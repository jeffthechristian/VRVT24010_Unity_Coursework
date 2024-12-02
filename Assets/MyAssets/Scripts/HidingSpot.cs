using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class HidingSpot : MonoBehaviour
{
    public GameObject player;
    public AudioClip audioClip;
    public AudioSource audioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PTrigger"))
        {
            player.SetActive(false);
            audioSource.PlayOneShot(audioClip);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PTrigger"))
        {
            player.SetActive(true);

        }
    }
}
