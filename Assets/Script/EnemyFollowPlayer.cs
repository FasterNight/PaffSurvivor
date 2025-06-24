using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed = 3f;

    private Transform target;

    void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.playerInstance != null)
        {
            Transform player = GameManager.Instance.playerInstance.transform;
            Transform playerModel = player.Find("PlayerModel");

            // Si PlayerModel existe, on le cible, sinon le parent
            target = playerModel != null ? playerModel : player;
        }

        if (target == null)
        {
            Debug.LogError("Target du joueur non trouvée pour l’ennemi !");
        }
    }

    void Update()
    {
        if (target == null) return;

        Vector3 direction = target.position - transform.position;
        direction.y = 0; // on reste au sol
        direction.Normalize();

        transform.position += direction * speed * Time.deltaTime;

        if (direction.sqrMagnitude > 0.001f)
        {
            transform.forward = direction;
        }
    }
}
