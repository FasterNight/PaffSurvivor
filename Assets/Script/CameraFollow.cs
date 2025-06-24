using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Camera targetCamera; // Assigne Main Camera ici
    public Vector2 mapSize = new Vector2(200f, 200f);
    public float cameraHeight = 10f;
    public float offsetDistance = 4f;
    public float smoothTime = 0.2f;

    private Transform player;
    private float cameraHalfWidth;
    private float cameraHalfHeight;
    private Vector3 lastPlayerPosition;
    private Vector3 currentVelocity;

    void Start()
    {
        if (targetCamera == null)
            targetCamera = Camera.main;

        if (targetCamera != null)
        {
            cameraHalfHeight = targetCamera.orthographicSize;
            cameraHalfWidth = cameraHalfHeight * targetCamera.aspect;
        }
    }

    void LateUpdate()
    {
        // Vérifie que le joueur est bien assigné, sinon essaie de le récupérer
        if (player == null)
        {
            if (GameManager.Instance != null && GameManager.Instance.playerInstance != null)
            {
                Transform model = GameManager.Instance.playerInstance.transform.Find("PlayerModel");
                if (model != null)
                {
                    player = model;
                    lastPlayerPosition = player.position;
                }
                else
                {
                    Debug.LogError("PlayerModel non trouvé dans playerInstance.");
                    return;
                }
            }
            else
            {
                // Attente que GameManager ou playerInstance soit prêt
                return;
            }
        }

        // Logique de suivi normale
        Vector3 playerDelta = player.position - lastPlayerPosition;
        Vector3 moveDir = playerDelta.normalized;
        Vector3 offset = -moveDir * offsetDistance;

        Vector3 targetPos = player.position + offset;
        targetPos.y = cameraHeight;

        float minX = -mapSize.x / 2f + cameraHalfWidth;
        float maxX = mapSize.x / 2f - cameraHalfWidth;
        float minZ = -mapSize.y / 2f + cameraHalfHeight;
        float maxZ = mapSize.y / 2f - cameraHalfHeight;

        targetPos.x = Mathf.Clamp(targetPos.x, minX, maxX);
        targetPos.z = Mathf.Clamp(targetPos.z, minZ, maxZ);

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref currentVelocity, smoothTime);
        lastPlayerPosition = player.position;
    }
}
