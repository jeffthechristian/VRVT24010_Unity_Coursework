using UnityEngine;

public class OutOfWorld : MonoBehaviour
{
    public Vector3 respawnPosition;
    [SerializeField] private float upwardForce = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = respawnPosition;
        } 
        else
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
            }
        }
    }
}
