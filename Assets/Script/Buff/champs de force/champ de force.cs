using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class champdeforce : MonoBehaviour
{
    [Range(0, 10)] public int power = 0;
    public bool Active = false;
    public GameObject ChampPrefab;
    public GameObject Player;

    private GameObject area;

    void Update()
    {
        if (Active && area == null)
        {
            Active = false;
            area = Instantiate(ChampPrefab);
        }
        if (area != null) {
            area.GetComponent<areaC>().Resize(power);
            Vector3 targetPos = Player.transform.position;
            area.transform.position = targetPos;
        }
    }
}
