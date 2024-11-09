using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class GhostEnemy : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float patrolRadius = 20f;
    public float detectionRange = 15f;
    public float reachDistance = 1f;  
    public AudioClip beforeHuntSound;         
    public AudioSource audioSource;    
    public AudioClip huntSound;         


    [SerializeField] private SkinnedMeshRenderer[] ghostRenderers; 
    private NavMeshAgent navMeshAgent;
    private Transform player;

    private bool isHunting = false;
    private float huntInterval = 20f;   
    private float huntChance = 0.25f;   
    private Coroutine huntCoroutine;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = moveSpeed;
        player = GameObject.FindGameObjectWithTag("GhostTarget").transform;

        SetGhostVisibility(false); 
        SetRandomPatrolPoint();
        StartCoroutine(CheckHuntChance());
    }

    void Update()
    {
        if (isHunting)
        {
            HuntPlayer();
            CheckIfPlayerReached();
        }
        else
        {
            Patrol();
        }
    }

    IEnumerator CheckHuntChance()
    {
        while (true)
        {
            yield return new WaitForSeconds(huntInterval);

            if (!isHunting && Random.value < huntChance)
            {
                StartCoroutine(PrepareForHunt());
            }
        }
    }

    IEnumerator PrepareForHunt()
    {
        navMeshAgent.isStopped = true;

        if (audioSource != null && beforeHuntSound != null)
        {
            audioSource.PlayOneShot(beforeHuntSound);
        }

        yield return new WaitForSeconds(3f);

        StartHunting();
    }

    void StartHunting()
    {
        isHunting = true;
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.position);
        if (audioSource != null && beforeHuntSound != null)
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
}
