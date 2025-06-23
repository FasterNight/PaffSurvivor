using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed = 3f;
    private Transform target;

    void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.playerInstance != null)
        {
            Transform parent = GameManager.Instance.playerInstance.transform;

            Transform child = parent.Find("PlayerModel");
            if (child != null)
            {
                target = child;
            }
            else
            {
                Debug.LogError("PlayerModel non trouvé dans l'objet Player !");
            }
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = (target.position - transform.position).normalized;
        direction.y = 0; 

        transform.position += direction * speed * Time.deltaTime;

        if (direction != Vector3.zero)
            transform.forward = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
