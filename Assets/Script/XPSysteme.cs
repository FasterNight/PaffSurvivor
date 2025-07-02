using UnityEngine;

public class XPSysteme : MonoBehaviour
{
    public float moveSpeed = 5f;
    public int xpValue = 1;

    private Transform player;
    private PlayerStats playerStats;
    private Vector3 randomVelocity;

    void Start()
    {
        // Recherche du joueur
        if (GameManager.Instance != null && GameManager.Instance.playerInstance != null)
        {
            Transform playerTransform = GameManager.Instance.playerInstance.transform;
            Transform playerModel = playerTransform.Find("PlayerModel");
            player = playerModel != null ? playerModel : playerTransform;
            playerStats = player.GetComponent<PlayerStats>();
        }
        else
        {
            Debug.LogError("XP : Joueur introuvable !");
        }

        // XP aléatoire
        xpValue = Random.Range(1, 6); 

        // Direction initiale
        randomVelocity = new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized * Random.Range(1f, 3f);

        // Appliquer la couleur
        SetColorBasedOnXP();
    }

    void Update()
    {
        if (player == null || playerStats == null)
            return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance < playerStats.attractRange)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position += randomVelocity * Time.deltaTime;
            randomVelocity = Vector3.Lerp(randomVelocity, Vector3.zero, Time.deltaTime * 2f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats pxp = other.GetComponent<PlayerStats>();
            if (pxp != null)
            {
                pxp.GainXP(xpValue);
            }

            Destroy(gameObject);
        }
    }

    void SetColorBasedOnXP()
    {
        Renderer renderer = GetComponentInChildren<Renderer>();
        if (renderer != null)
        {
            float intensity = Mathf.InverseLerp(1f, 5f, xpValue);
            Color brightGreen = Color.Lerp(Color.green, new Color(0.5f, 1f, 0.5f), intensity);
            renderer.material.color = brightGreen;
        }
    }
}
