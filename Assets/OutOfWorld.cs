using UnityEngine;

public class OutOfWorld : MonoBehaviour
{
    public Vector3 respawnPosition;  

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPosition;
        }
    }
}
