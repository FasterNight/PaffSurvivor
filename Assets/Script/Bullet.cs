using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 10f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Zombie enemy = other.GetComponent<Zombie>();
            if (enemy != null)
            {
                enemy.Die();
            }
            Destroy(gameObject);
        }
    }

}
