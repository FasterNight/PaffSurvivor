using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaC : MonoBehaviour
{

    public float TimeBetweenHit = 0f;
    private float Timer = 0f;
    private bool Domage = false;

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0f)
        {
            Domage = true;
            Timer = TimeBetweenHit;
        }
        else
        {
            Domage = false;
        }
    }

    public void Resize(int n)
    {
        Vector3 Scale = new Vector3(3+n, 0.1f, 3+n); ;
        gameObject.transform.localScale = Scale;
    }

    private void OnTriggerStay(Collider other)
    {
        if (Domage)
        {
            
        }
    }
}
