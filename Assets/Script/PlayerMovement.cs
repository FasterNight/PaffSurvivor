using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Déplacement")]
    public float moveSpeed = 5f;
    public Vector2 mapSize = new Vector2(200f, 200f);
    public float dragThreshold = 0.1f;

    [Header("Ciblage automatique")]
    public float detectionRadius = 10f;
    public float rotationSpeed = 5f;
    public string targetTag = "Enemy";

    [Header("Animation")]
    public Animator animator;

    private Vector3 dragStartScreen;
    private Vector3 currentDragDirection;
    private bool isDragging = false;

    private Transform currentTarget;

    void Update()
    {
        HandleMouseDrag();
        MovePlayer();
        ClampToMapBounds();

        FindClosestTarget();
        LookAtTarget();

        UpdateAnimation();
    }

    void HandleMouseDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragStartScreen = Input.mousePosition;
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            currentDragDirection = Vector3.zero;
        }

        if (isDragging)
        {
            Vector3 dragCurrentScreen = Input.mousePosition;
            Vector3 dragVector = dragCurrentScreen - dragStartScreen;

            if (dragVector.magnitude > dragThreshold * Screen.dpi)
            {
                currentDragDirection = new Vector3(dragVector.x, 0f, dragVector.y).normalized;
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

    void FindClosestTarget()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
        float shortestDistance = Mathf.Infinity;
        Transform nearestTarget = null;

        foreach (GameObject target in targets)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance < shortestDistance && distance <= detectionRadius)
            {
                shortestDistance = distance;
                nearestTarget = target.transform;
            }
        }

        currentTarget = nearestTarget;
    }

    void LookAtTarget()
    {
        if (currentTarget != null)
        {
            Vector3 direction = currentTarget.position - transform.position;
            direction.y = 0f;

            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 100f);
            }
        }
    }

    void UpdateAnimation()
    {
        if (animator == null) return;

        if (currentDragDirection != Vector3.zero)
        {
            float dot = Vector3.Dot(transform.forward, currentDragDirection.normalized);
            bool isRunningForward = dot > 0.5f;
            bool isRunningBack = dot < -0.5f;

            animator.SetBool("Run", isRunningForward);
            animator.SetBool("RunBack", isRunningBack);
        }
        else
        {
            animator.SetBool("Run", false);
            animator.SetBool("RunBack", false);
        }
    }
}
