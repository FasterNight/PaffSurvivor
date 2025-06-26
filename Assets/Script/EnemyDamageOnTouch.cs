using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyDamageOnTouch : MonoBehaviour
{
    public int damage = 2;
    public float damageInterval = 1f;

    private bool isTouchingPlayer = false;
    private PlayerStats playerStats;
    private float damageTimer = 0f;

    void Update()
    {
        if (isTouchingPlayer && playerStats != null)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                playerStats.TakeDamage(damage);
                damageTimer = 0f;
            }
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerStats = collision.gameObject.GetComponent<PlayerStats>();
            isTouchingPlayer = true;
            damageTimer = damageInterval; // tape instantanément au contact
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTouchingPlayer = false;
            playerStats = null;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerStats = other.GetComponent<PlayerStats>();
            isTouchingPlayer = true;
            damageTimer = damageInterval;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isTouchingPlayer = false;
            playerStats = null;
        }
    }
}
