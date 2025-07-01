// LazerMoveFixed.cs
using System.Collections;
using UnityEngine;

public class LazerMoveFixed : MonoBehaviour
{
    private Vector3 directionA;
    private Vector3 directionB;
    private int currentRebon = 0;
    private int maxRebon;
    private float segmentDistance;
    private float speed = 10f;

    private Vector3 targetPosition;
    private bool movingToA = true;

    public void Init(Vector3 dirA, Vector3 dirB, float distance, int rebond)
    {
        directionA = dirA.normalized;
        directionB = dirB.normalized;
        segmentDistance = distance;
        maxRebon = rebond;

        targetPosition = transform.position + directionA * segmentDistance;
    }

    void Update()
    {
        Vector3 moveDir = (targetPosition - transform.position).normalized;
        float step = speed * Time.deltaTime;

        transform.position += moveDir * step;

        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentRebon++;
            if (currentRebon >= maxRebon)
            {
                Destroy(gameObject);
                return;
            }

            movingToA = !movingToA;
            Vector3 nextDir = movingToA ? directionA : directionB;
            targetPosition = transform.position + nextDir * segmentDistance;
        }
    }
}