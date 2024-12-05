using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.Audio;

public class HidingSpot : MonoBehaviour
{
    public AudioClip audioClip;
    public AudioSource audioSource;

    public static bool isPlayerHiding = false;

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


}
