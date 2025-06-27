using UnityEngine;

public class HitZombie: MonoBehaviour
{
    public float damage = 20f;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(" hit");
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Zombie hit");
            Zombie enemy = other.GetComponent<Zombie>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
            }
        }
    }
}
