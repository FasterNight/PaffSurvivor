using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LanceurMolotov : MonoBehaviour
{
    [Range(0, 10)] public int power = 0;
    public GameObject MolotovPrefab;
    public Transform player;
    public float launchForce = 10f;
    public float launchUpward = 5f;
    public float fireRate = 2f;

    private float fireTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        fireTimer -= Time.deltaTime;
        if (fireTimer <= 0f && power > 0)
        {
            fireTimer = fireRate;
            LaunchMolotovs();
        }
    }

    void LaunchMolotovs()
    {
        float angleStep = 360f / power;
        float randomOffset = Random.Range(0f, 360f); // Pour changer l'orientation globale à chaque tir

        for (int i = 0; i < power; i++)
        {
            float angle = (angleStep * i + randomOffset) * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle));

            Vector3 launchDir = direction.normalized * launchForce + Vector3.up * launchUpward;

            GameObject molotov = Instantiate(MolotovPrefab, player.position, Quaternion.identity);
            Rigidbody rb = molotov.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddForce(launchDir, ForceMode.Impulse);
            }
        }
    }
}
