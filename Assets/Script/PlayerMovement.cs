using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 mapSize = new Vector2(200f, 200f);
    public float dragThreshold = 0.1f; // Pour ignorer les petits mouvements involontaires

    private Vector3 dragStartScreen;
    private Vector3 currentDragDirection;
    private bool isDragging = false;

    void Update()
    {
        HandleMouseDrag();
        MovePlayer();
        ClampToMapBounds();
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Enregistre la position initiale du drag (écran)
            dragStartScreen = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            // Relâchement du clic
            isDragging = false;
            currentDragDirection = Vector3.zero;
        }

        if (isDragging)
        {
            Vector3 dragCurrentScreen = Input.mousePosition;
            Vector3 dragVector = dragCurrentScreen - dragStartScreen;

            // Ignore les petits mouvements
            if (dragVector.magnitude > dragThreshold * Screen.dpi)
            {
                // Direction du mouvement dans le monde
                Vector3 worldDir = new Vector3(dragVector.x, 0f, dragVector.y).normalized;
                currentDragDirection = worldDir;
            }
            else
            {
                currentDragDirection = Vector3.zero;
            }
        }
    }

    void MovePlayer()
    {
        if (currentDragDirection == Vector3.zero) return;

        transform.position += currentDragDirection * moveSpeed * Time.deltaTime;
    }

    void ClampToMapBounds()
    {
        float halfWidth = mapSize.x / 2f;
        float halfDepth = mapSize.y / 2f;

        float clampedX = Mathf.Clamp(transform.position.x, -halfWidth, halfWidth);
        float clampedZ = Mathf.Clamp(transform.position.z, -halfDepth, halfDepth);

        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}
