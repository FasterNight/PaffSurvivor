using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player")]
    public GameObject playerPrefab;
    public Vector3 spawnPosition = new Vector3(0f, 0f, 0f);
    [HideInInspector]
    public GameObject playerInstance;

    [Header("UI")]
    public Slider healthBar;

    public Image xpCircle;                // Image circulaire XP
    public TextMeshProUGUI levelText;    // Texte niveau

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (playerPrefab != null)
        {
            // Cherche le GameObject "Player" dans la hiérarchie
            Transform parent = GameObject.Find("Player")?.transform;
            if (parent == null)
                Debug.LogWarning("GameObject 'Player' non trouvé dans la hiérarchie.");

            // Instancie le joueur comme enfant de "Player" (ou sans parent si introuvable)
            playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity, parent);

            // Initialisation des stats et UI
            PlayerStats stats = playerInstance.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.InitializeBars(healthBar, null);  // Pas de xpBar ici
                stats.InitializeXPUI(xpCircle, levelText);
            }
        }
        else
        {
            Debug.LogError("Player prefab non assigné dans le GameManager.");
        }
    }

}
