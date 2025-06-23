using UnityEngine;

public class EnemyDeathNotifier : MonoBehaviour
{
    public EnemySpawner spawner;

    void OnDestroy()
    {
        if (spawner != null)
            spawner.OnEnemyDeath(gameObject);
    }
}