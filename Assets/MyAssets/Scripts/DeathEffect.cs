using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class DeathEffect : MonoBehaviour
{
    public Image blackScreen;
    public TMP_Text deathText; 
    public AudioSource deathSound; 

    public void Start()
    {
        StartCoroutine(FadeToBlack());
    }

    private IEnumerator FadeToBlack()
    {
        float elapsedTime = 0f;
        float fadeDuration = 4f;
        Color color = blackScreen.color;

        if (deathSound != null)
        {
            deathSound.Play();
        }

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            blackScreen.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        blackScreen.color = color;

        if (deathText != null)
        {
            deathText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(6f);

        SceneManager.LoadScene("MainMenu");
    }
}
