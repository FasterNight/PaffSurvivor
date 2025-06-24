using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    public Camera targetCamera;
    public Vector2 mapSize = new Vector2(200f, 200f);
    public float cameraHeight = 10f;
    public float offsetDistance = 4f; // Intensité du recul
    public float smoothTime = 0.2f;

    private float cameraHalfWidth;
    private float cameraHalfHeight;

    private Vector3 lastPlayerPosition;
    private Vector3 currentVelocity;
    private float orthographicSize;
    private float aspect;
    internal static Camera main;
    internal bool orthographic;

    void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.playerInstance != null)
        {
            Transform model = GameManager.Instance.playerInstance.transform.Find("PlayerModel");
            if (model != null)
            {
                player = model;
            }
            else
            {
                Debug.LogError("PlayerModel non trouvé dans l'objet Player !");
            }
        }
        else
        {
            Debug.LogError("GameManager ou playerInstance est nul !");
        }

        // Initialisation de la caméra
        cameraHalfHeight = targetCamera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * targetCamera.aspect;
        lastPlayerPosition = player != null ? player.position : Vector3.zero;
    }

    void LateUpdate()
    {
        // Direction du mouvement
        Vector3 playerDelta = player.position - lastPlayerPosition;
        Vector3 moveDir = playerDelta.normalized;

        // Inverser le décalage pour que la caméra se place en arrière
        Vector3 offset = -moveDir * offsetDistance;

        // Position cible avant clamp
        Vector3 targetPos = player.position + offset;
        targetPos.y = cameraHeight;

        // Limites de la map
        float minX = -mapSize.x / 2f + cameraHalfWidth;
        float maxX = mapSize.x / 2f - cameraHalfWidth;
        float minZ = -mapSize.y / 2f + cameraHalfHeight;
        float maxZ = mapSize.y / 2f - cameraHalfHeight;

        // Clamp sur X et Z
        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.z = Mathf.Clamp(targetPos.z, minZ, maxZ);

        // Mouvement fluide vers la position
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);

        // Met à jour la dernière position
        lastPlayerPosition = player.position;
    }
}
