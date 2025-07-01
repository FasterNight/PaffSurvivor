using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanceurLazer : MonoBehaviour
{
    public bool power = false;
    public GameObject LazerPrefab;
    public Transform player;
    public float lazRate = 2f;

    private float lazTimer = 0f;

    public float angle = 20f;
    public int LifeRebon = 6;
    public float spawnOffset = 1.5f; // distance du joueur pour spawner les lasers
    public float moveDistance = 5f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        lazTimer -= Time.deltaTime;
        if (lazTimer <= 0f && power == true)
        {
            lazTimer = lazRate;
            LaunchLazer();
        }
    }

    public void LaunchLazer()
    {
        // Chaque paire : position de départ et les 2 directions (rebond A ↔ B)
        var lazerConfigs = new (Vector3 offset, Vector3 dirA, Vector3 dirB)[]
        {
        // Haut gauche : va bas droit ↔ bas gauche
        (new Vector3(-1, 0, 1), new Vector3(1, 0, -1), new Vector3(-1, 0, -1)),

        // Haut droit : va bas gauche ↔ bas droit
        (new Vector3(1, 0, 1), new Vector3(-1, 0, -1), new Vector3(1, 0, -1)),

        // Bas gauche : va haut droit ↔ haut gauche
        (new Vector3(-1, 0, -1), new Vector3(1, 0, 1), new Vector3(-1, 0, 1)),

        // Bas droit : va haut gauche ↔ haut droit
        (new Vector3(1, 0, -1), new Vector3(-1, 0, 1), new Vector3(1, 0, 1))
        };

        foreach (var (offset, dirA, dirB) in lazerConfigs)
        {
            Vector3 spawnPos = player.position + offset.normalized * spawnOffset;
            Quaternion rot = Quaternion.LookRotation(dirA);
            GameObject lazer = Instantiate(LazerPrefab, spawnPos, rot);

            var lazerScript = lazer.GetComponent<LazerMoveFixed>();
            if (lazerScript != null)
                lazerScript.Init(dirA.normalized, dirB.normalized, moveDistance, LifeRebon);
        }
    }

}

// distance que chaque laser parcourt avant rebond


