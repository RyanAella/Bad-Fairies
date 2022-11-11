using System.Collections;
using System.Collections.Generic;
using _Scripts.Buildings;
using UnityEngine;

public class Frame : MonoBehaviour
{
    public Material mat1;   // sichtbar
    public Material mat2;   // unsichtbar

    public bool tracked;

    public BuildableController bc;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (tracked)
        {
            this.GetComponent<Renderer>().material = mat1;
        }
        else
        {
            this.GetComponent<Renderer>().material = mat2;
        }

        tracked = false;
    }
}
