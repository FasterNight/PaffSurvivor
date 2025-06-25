using UnityEngine;

public class Zombie : MonoBehaviour
{
    public float speed = 3f;
    public float pushForce = 5f;
    public float attackRange = 1.5f;
    public float separationDistance = 1f;
    public float separationStrength = 2f;

    private Transform player;
    private Rigidbody rb;

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
            Debug.LogError("ZombieFollow : joueur introuvable !");
        }
        rb = GetComponentInChildren<Rigidbody>();
    }


    void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > attackRange)
        {
            FollowPlayer();
        }
    }

    void FollowPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 move = direction * speed * Time.deltaTime;
        rb.MovePosition(rb.position + move);
    }
}
