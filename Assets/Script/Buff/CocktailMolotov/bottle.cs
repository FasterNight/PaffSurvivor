using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class bottle : MonoBehaviour
{
    public string tagActive = "Ground";
    public GameObject area;

    public float TimeActive = 2f;
    private float Time = 0f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagActive))
        {
            Activation();
        }
    }

    private void Activation()
    {
        GameObject molotov = Instantiate(area, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
