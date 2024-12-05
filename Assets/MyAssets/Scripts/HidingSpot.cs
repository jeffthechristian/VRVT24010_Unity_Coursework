using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Audio;

public class HidingSpot : MonoBehaviour
{
    public GameObject player;
    public AudioClip audioClip;
    public AudioSource audioSource;

    private bool isPlayerHiding = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PTrigger"))
        {
            audioSource.PlayOneShot(audioClip);


            isPlayerHiding = true;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PTrigger"))
        {
            isPlayerHiding = false;
        }
    }

    private void Update()
    {
        if (isPlayerHiding)
        {
            if (XROrigin.isCrouching && !Flashlight.isFlashlight && isPlayerHiding)
            {
                player.SetActive(false);
            }
            else
            {
                player.SetActive(true);
            }
        }
    }
}
