using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class Candle : MonoBehaviour
{
    public AudioSource audiosource;
    public AudioClip audioClip;
    private bool hasPlayed;

    void Start()
    {
        hasPlayed = false;

        var grabInteractable = GetComponent<XRGrabInteractable>();
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrab);
        }
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        if (!hasPlayed)
        {
            audiosource.PlayOneShot(audioClip);
            hasPlayed = true;
        }
    }

}
