using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Détection & Ciblage")]
    public float detectionRadius = 10f;
    public string targetTag = "Enemy";

    [Header("Tir")]
    public GameObject bulletPrefab;
    public Transform shootPoint;
    public float fireRate = 1f;
    public float bulletSpeed = 10f;

    private float fireCooldown = 0f;
    private Transform currentTarget;

    void Update()
    {
        FindClosestTarget();
        ShootAtTarget();
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

    void ShootAtTarget()
    {
        if (currentTarget == null) return;

        fireCooldown -= Time.deltaTime;

        if (fireCooldown <= 0f)
        {
            fireCooldown = 1f / fireRate;

            if (bulletPrefab != null && shootPoint != null)
            {
                Transform parent = GameObject.Find("Player")?.transform;
                if (parent == null)
                    Debug.LogWarning("GameObject 'Player' non trouvé pour y placer la balle.");

                GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity, parent);

                Vector3 direction = (currentTarget.position - shootPoint.position).normalized;
                bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
            }
        }
    }
}
