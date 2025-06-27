using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    [Range(0, 10)] public int power = 0;
    public GameObject SawModel;
    public float cooldownTime = 2f;
    private float TimeCooldown = 0f;

    public float ActiveTime = 2f;
    private float TimerActive = 0f;

    private bool cooldown = false;

    public float radius = 2f;
    public float orbitSpeed = 100f;
    public float selfRotationSpeed = 300f;

    private List<GameObject> saws = new List<GameObject>();
    private Transform player;

    private void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.playerInstance != null)
        {
            Transform playerTransform = GameManager.Instance.playerInstance.transform;
            Transform playerModel = playerTransform.Find("PlayerModel");
            player = playerModel != null ? playerModel : playerTransform;
        }
        TimerActive = ActiveTime;
    }

    void Update()
    {
        if (cooldown)
        {
            if (TimeCooldown > 0)
            {
                TimeCooldown -= Time.deltaTime;
            }
            else
            {
                TimerActive = ActiveTime;
                cooldown = false;
                Activation();
            }
        }
        else
        {
            if (TimerActive > 0)
            {
                TimerActive -= Time.deltaTime;
            }
            else
            {
                TimeCooldown = cooldownTime;
                cooldown = true;
                Desactivation();
            }
        }

        if (!cooldown && power > 0)
        {
            UpdateSaws();
        }
    }

    private void Activation()
    {
        if (power <= 0) return;

        // Supprimer les anciennes scies si elles existent
        foreach (var saw in saws)
        {
            Destroy(saw);
        }
        saws.Clear();

        // Créer les nouvelles scies
        for (int i = 0; i < power; i++)
        {
            GameObject saw = Instantiate(SawModel);
            saws.Add(saw);
        }

        // Activer toutes les scies
        foreach (var saw in saws)
        {
            saw.SetActive(true);
        }
    }

    private void Desactivation()
    {
        foreach (var saw in saws)
        {
            saw.SetActive(false);
        }
    }

    private void UpdateSaws()
    {
        float angleStep = 360f / power;
        float time = Time.time;

        for (int i = 0; i < saws.Count; i++)
        {
            if (!saws[i].activeSelf) continue;

            float angle = angleStep * i + time * orbitSpeed;
            Vector3 offset = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0f, Mathf.Sin(angle * Mathf.Deg2Rad)) * radius;

            Vector3 targetPos = player.position + offset;
            saws[i].transform.position = targetPos;

            // Faire en sorte que la scie regarde vers l'extérieur du joueur
            Vector3 direction = (targetPos - player.position).normalized;
            float angleZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //saws[i].transform.rotation = Quaternion.Euler(90f, 0f, angleZ);

            // Puis faire tourner la scie sur elle-même
            saws[i].transform.Rotate(Vector3.forward * selfRotationSpeed * Time.deltaTime, Space.Self);
        }
    }
}
