using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class Pin : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    private Rigidbody rigidBody;

    void Start ()
    {
        Transform currentTransform = transform;
        startPosition = currentTransform.position;
        startRotation = currentTransform.rotation;
        rigidBody = GetComponent<Rigidbody> ();
    }

    public bool IsStanding()
    {
        return startRotation.Compare(transform.rotation, 10);
    }

    public void RaiseIfStanding()
    {
        if (IsStanding()) {
            MoveToStart();
        }
    }

    public void SetLower()
    {
        rigidBody.useGravity = true;
    }

    public void MoveToStart()
    {
        rigidBody.useGravity = false;
        rigidBody.velocity = Vector3.zero;
        rigidBody.angularVelocity = Vector3.zero;
        Transform currentTransform = transform;
        currentTransform.position = startPosition;
        currentTransform.rotation = startRotation;
       ;
    }



    // void OnCollisionEnter(Collision other)
    // {
    //     if (other.gameObject.name == "ball" && isSettled)
    //     {
    //         isSettled = false;
    //     }
    // }
}
