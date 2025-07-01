using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lanceur : MonoBehaviour
{
    [Range(0, 1000)] public int power = 0;
    public GameObject ElecPrefab;
    public Transform player;
    public float elecRate = 2f;

    private float elecTimer = 0f;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    // Update is called once per frame
    void Update()
    {
        elecTimer -= Time.deltaTime;
        if (elecTimer <= 0f && power > 0)
        {
            elecTimer = elecRate;
            LaunchLightning();
        }
    }

    private void LaunchLightning()
    {
        Camera cam = Camera.main;
        if (cam == null) return;

        for (int i = 0; i < power; i++)
        {
            // Coordonnées écran aléatoires
            float randX = Random.Range(0f, 1f);
            float randY = Random.Range(0f, 1f);

            Vector3 screenPoint = new Vector3(randX * Screen.width, randY * Screen.height, cam.transform.position.y);
            Ray ray = cam.ScreenPointToRay(screenPoint);

            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                Vector3 spawnPos = new Vector3(hit.point.x, 1f, hit.point.z);
                Quaternion randomYRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
                Instantiate(ElecPrefab, spawnPos, randomYRotation);
            }
        }
    }


}
