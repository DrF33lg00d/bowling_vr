using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0,0,100.0f));
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0,0,-100.0f));
        }
        
    }
    
    
    
}
