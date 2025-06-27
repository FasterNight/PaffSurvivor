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
            playerInstance = Instantiate(playerPrefab, spawnPosition, Quaternion.identity);

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
