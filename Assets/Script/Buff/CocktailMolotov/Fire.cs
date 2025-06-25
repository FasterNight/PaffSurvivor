using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public float Timer = 3f;

    void Update()
    {
        if (Timer <= 0)
        {
            Destroy(gameObject);
        }
        Timer -= Time.deltaTime;
    }
}