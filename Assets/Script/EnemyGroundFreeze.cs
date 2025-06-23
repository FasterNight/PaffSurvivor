using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyGroundFreeze : MonoBehaviour
{
    private Rigidbody rb;
    public string groundTag = "Ground"; 

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(groundTag))
        {
            rb.constraints |= RigidbodyConstraints.FreezePositionY;
        }
    }
}
