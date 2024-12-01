using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    public GameObject candleLight;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Candle"))
        {
            candleLight.SetActive(true);
        }
    }
}
