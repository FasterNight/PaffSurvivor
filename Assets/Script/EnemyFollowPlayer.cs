using System.Linq;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed = 3f;
    public float attackRange = 1.5f;
    public float separationDistance = 1.2f;
    public float separationStrength = 1.5f;

    private Transform player;
    private Rigidbody rb;
    private Animator animator;
    private Vector3 velocity;
    private bool isDead = false;

    void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.playerInstance != null)
        {
            Transform playerTransform = GameManager.Instance.playerInstance.transform;
            Transform playerModel = playerTransform.Find("PlayerModel");
            player = playerModel != null ? playerModel : playerTransform;
        }
        else
        {
            Debug.LogError("Zombie : Joueur introuvable !");
        }

        rb = GetComponent<Rigidbody>();
        if (!rb) Debug.LogError("Zombie : Rigidbody manquant !");

        animator = GetComponentInChildren<Animator>();
        if (!animator) Debug.LogWarning("Zombie : Animator non trouvé dans les enfants !");
    }

    void FixedUpdate()
    {
        if (player == null || isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            Vector3 moveDirection = (player.position - transform.position).normalized;
            Vector3 separation = CalculateSeparation();

            Vector3 finalDirection = (moveDirection + separation * separationStrength).normalized;

            velocity = finalDirection * speed;
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);

            if (animator) animator.SetBool("Run", true);
        }
        else
        {
            rb.velocity = Vector3.zero;
            if (animator) animator.SetBool("Run", false);
        }

        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0f;
        if (lookDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * 10f);
        }
    }

    Vector3 CalculateSeparation()
    {
        Vector3 separation = Vector3.zero;
        Collider[] nearbyZombies = Physics.OverlapSphere(transform.position, separationDistance);

        foreach (Collider col in nearbyZombies)
        {
            if (col.gameObject != gameObject && col.CompareTag("Zombie"))
            {
                Vector3 away = transform.position - col.transform.position;
                float distance = away.magnitude;

                if (distance > 0f && distance < separationDistance)
                {
                    separation += away.normalized / distance;
                }
            }
        }

        return separation;
    }

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true; 
        if (animator) animator.SetBool("Dead", true);

        Destroy(gameObject, 3f);
    }
}
