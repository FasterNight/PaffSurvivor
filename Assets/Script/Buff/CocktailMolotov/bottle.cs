using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class bottle : MonoBehaviour
{
    public string tagActive = "Ground";
    public GameObject area;

    public float TimeActive = 2f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(tagActive))
        {
            Activation();
        }
    }

    private void Activation()
    {
        Quaternion rota = Quaternion.Euler(0f, 0f, 0f); // -90° sur X
        GameObject molotov = Instantiate(area, gameObject.transform.position, rota);
        Destroy(gameObject);
    }

}
