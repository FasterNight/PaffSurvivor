using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Vector2 mapSize = new Vector2(200f, 200f);

    void Start()
    {
        transform.position = new Vector3(0f, 1.1f, 0f);
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        Vector3 move = new Vector3(moveX, 0f, moveZ).normalized;
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);

        // Limiter la position dans la map
        float halfWidth = mapSize.x / 2f;
        float halfHeight = mapSize.y / 2f;

        float clampedX = Mathf.Clamp(transform.position.x, -halfWidth, halfWidth);
        float clampedZ = Mathf.Clamp(transform.position.z, -halfHeight, halfHeight);

        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);
    }
}
