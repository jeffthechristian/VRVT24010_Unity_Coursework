using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using UnityEngine.Audio;

public class Ghost : MonoBehaviour
{
    public static int ghostAnger = 0;
    private static int currentGhostAnger;
    public float moveSpeed = 2f;
    public float patrolRadius = 20f;
    public float reachDistance = 1f;
    public AudioClip beforeHuntSound;
    public AudioSource audioSource;
    public AudioClip huntSound;
    public AudioClip idleSound;
    public AudioClip[] randomSounds;

    [SerializeField] private SkinnedMeshRenderer[] ghostRenderers;
    private NavMeshAgent navMeshAgent;
    private Transform player;

    private Animator animator;

    private bool isHunting = false;
    private bool isAppearancePlaying = false; 
    private bool isRandomSoundPlaying = false; 
    public float huntInterval = 20f;
    public float huntChance = 0.25f;
    public float eventChance = 0.15f;
    public float eventInterval = 20f;
    public float soundChance = 0.15f;
    public float soundInterval = 20f;
    private Coroutine huntCoroutine;
    public GameObject deathEffect;

    void Start()
    {
        currentGhostAnger = ghostAnger;
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;

        UpdatePlayerReference();

        SetGhostVisibility(false);
        SetRandomPatrolPoint();
        StartCoroutine(CheckHuntChance());
        StartCoroutine(CheckIdleAppearanceChance()); 
        StartCoroutine(CheckRandomSoundChance()); 
    }

    void Update()
    {
        if (currentGhostAnger < ghostAnger)
        {
            currentGhostAnger = ghostAnger;

            huntChance += 0.08f;
        }

        if (isHunting)
        {
            if (player != null && IsPlayerReachable())
            {
                HuntPlayer();
                CheckIfPlayerReached();
            }
            else
            {
                Patrol();
            }
        }
        else if (!isAppearancePlaying)
        {
            Patrol();
        }
    }

    IEnumerator CheckHuntChance()
    {
        while (true)
        {
            if (isHunting || isAppearancePlaying || isRandomSoundPlaying)
            {
                yield return new WaitForSeconds(3f); 
                continue;
            }

            yield return new WaitForSeconds(huntInterval);

            if (!isHunting && !isAppearancePlaying && !isRandomSoundPlaying && Random.value < huntChance)
            {
                StartCoroutine(PrepareForHunt());
            }
        }
    }



    IEnumerator CheckIdleAppearanceChance()
    {
        while (true)
        {
            if (isHunting || isAppearancePlaying || isRandomSoundPlaying)
            {
                yield return new WaitForSeconds(3f);
                continue;
            }

            yield return new WaitForSeconds(eventInterval);

            if (!isAppearancePlaying && !isHunting && !isRandomSoundPlaying && Random.value < eventChance)
            {
                StartCoroutine(Appearance());
            }
        }
    }

    IEnumerator CheckRandomSoundChance()
    {
        while (true)
        {
            if (isHunting || isAppearancePlaying || isRandomSoundPlaying)
            {
                yield return new WaitForSeconds(3f);
                continue;
            }

            yield return new WaitForSeconds(soundInterval);

            if (!isAppearancePlaying && !isHunting && !isRandomSoundPlaying && Random.value < soundChance)
            {
                RandomSound();
            }
        }
    }

    IEnumerator Appearance()
    {
        isAppearancePlaying = true;
        SetGhostVisibility(true);
        navMeshAgent.isStopped = true;
        audioSource.PlayOneShot(idleSound);
        animator.SetTrigger("isIdle");
        float idleDuration = Random.Range(3f, 10f);

        yield return new WaitForSeconds(idleDuration);

        SetGhostVisibility(false);
        animator.SetTrigger("isWalk");
        navMeshAgent.isStopped = false;
        audioSource.Stop();
        isAppearancePlaying = false;
    }


    private void RandomSound()
    {
        isRandomSoundPlaying = true;

        int randomIndex = Random.Range(0, randomSounds.Length);
        AudioClip selectedClip = randomSounds[randomIndex];

        audioSource.PlayOneShot(selectedClip);

        isRandomSoundPlaying = false;
    }

    IEnumerator PrepareForHunt()
    {
        navMeshAgent.isStopped = true;
        audioSource.PlayOneShot(beforeHuntSound);

        yield return new WaitForSeconds(7f);

        StartHunting();
    }

    void StartHunting()
    {
        isHunting = true;
        navMeshAgent.isStopped = false;

        UpdatePlayerReference();

        if (player != null && IsPlayerReachable())
        {
            navMeshAgent.SetDestination(player.position);
        }
        else
        {
            SetRandomPatrolPoint();
        }

        if (audioSource != null && huntSound != null)
        {
            audioSource.PlayOneShot(huntSound);
        }

        float huntDuration = Random.Range(20f, 40f);
        huntCoroutine = StartCoroutine(HuntDurationTimer(huntDuration));
        StartCoroutine(FlickerEffect());
    }

    IEnumerator HuntDurationTimer(float duration)
    {
        yield return new WaitForSeconds(duration);
        StopHunting();
    }

    void HuntPlayer()
    {
        navMeshAgent.SetDestination(player.position);
    }

    void StopHunting()
    {
        isHunting = false;
        audioSource.Stop();

        if (huntCoroutine != null)
        {
            StopCoroutine(huntCoroutine);
        }

        SetGhostVisibility(false);
        SetRandomPatrolPoint();
    }

    void CheckIfPlayerReached()
    {
        if (Vector3.Distance(transform.position, player.position) <= reachDistance)
        {
            StopHunting();
            Debug.Log("Ghost: YUM YUM!");
            deathEffect.SetActive(true);
        }
    }

    void Patrol()
    {
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            SetRandomPatrolPoint();
        }
    }

    void SetRandomPatrolPoint()
    {
        Vector3 randomPoint = GetRandomNavMeshPoint(transform.position, patrolRadius);
        if (randomPoint != Vector3.zero)
        {
            navMeshAgent.SetDestination(randomPoint);
        }
    }

    Vector3 GetRandomNavMeshPoint(Vector3 origin, float radius)
    {
        for (int i = 0; i < 10; i++)
        {
            Vector3 randomDirection = Random.insideUnitSphere * radius;
            randomDirection += origin;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return Vector3.zero;
    }

    void SetGhostVisibility(bool isVisible)
    {
        foreach (SkinnedMeshRenderer renderer in ghostRenderers)
        {
            if (renderer != null)
            {
                renderer.enabled = isVisible;
            }
        }
    }

    IEnumerator FlickerEffect()
    {
        while (isHunting)
        {
            SetGhostVisibility(!ghostRenderers[0].enabled);
            yield return new WaitForSeconds(0.2f);
        }

        SetGhostVisibility(false);
    }

    bool IsPlayerReachable()
    {
        if (player == null) return false;

        NavMeshPath path = new NavMeshPath();
        if (navMeshAgent.CalculatePath(player.position, path))
        {
            return path.status == NavMeshPathStatus.PathComplete;
        }
        return false;
    }

    void UpdatePlayerReference()
    {
        player = GameObject.FindGameObjectWithTag("GhostTarget")?.transform;
    }
}
