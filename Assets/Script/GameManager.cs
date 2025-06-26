using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Slider xpBar;

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

            // Injecte les références aux barres directement si le Player a un PlayerStats
            PlayerStats stats = playerInstance.GetComponent<PlayerStats>();
            if (stats != null)
            {
                stats.InitializeBars(healthBar, xpBar);
            }
        }
        else
        {
            Debug.LogError("Player prefab non assigné dans le GameManager.");
        }
    }
}
