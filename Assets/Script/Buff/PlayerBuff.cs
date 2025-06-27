using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuff : MonoBehaviour
{
    [Header("Saw")]
    public GameObject Saw;
    private int Sawlevel;
    private int SawlevelMax = 10;


    [Header("Molotov")]
    public GameObject Molotov;
    private int MolotovLevel;
    private int MolotovLevelMax = 10;
    // Start is called before the first frame update
    void Start()
    {
        Sawlevel = 0;
        MolotovLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
