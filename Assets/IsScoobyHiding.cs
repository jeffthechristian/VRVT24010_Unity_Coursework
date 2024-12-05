using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class IsScoobyHiding : MonoBehaviour
{
    public GameObject player;

    private void Update()
    {
        if (HidingSpot.isPlayerHiding && XROrigin.isCrouching && !Flashlight.isFlashlight)
        {
            player.SetActive(false);
        }
        else
        {
            player.SetActive(true);
        }
    }
}
