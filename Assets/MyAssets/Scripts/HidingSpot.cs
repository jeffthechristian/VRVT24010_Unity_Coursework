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

            if (!XROrigin.isCrouching && !Flashlight.isFlashlight)
            {
                player.SetActive(false);
                isPlayerHiding = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PTrigger"))
        {
            player.SetActive(true);
            isPlayerHiding = false;
        }
    }

    private void Update()
    {
        if (isPlayerHiding)
        {
            if (XROrigin.isCrouching || Flashlight.isFlashlight)
            {
                player.SetActive(true);
                isPlayerHiding = false;
            }
        }
    }
}
