using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rigidbody;
    private AudioSource audioSource;
    private Vector3 ballStartPosition;
    //public float launchSpeed;
    public Vector3 launchVelocity;
    public bool inPlay = false;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        ballStartPosition = transform.position;
        rigidbody.useGravity = false;
        //Launch(launchVelocity);

    }

    public void Launch(Vector3 velocity)
    {
        //rigidbody.velocity = new Vector3(0, 0, launchSpeed);
        inPlay = true;
        rigidbody.useGravity = true;
        rigidbody.velocity = velocity;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Reset()
    {
        Debug.Log("Ball reset!!!");
        inPlay = false;
        transform.position = ballStartPosition;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
        rigidbody.useGravity = false;
    }
}
