using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pin : MonoBehaviour
{
    public float standingTreshold = 3f;
    public float standingTreshold360 = 357f;

    
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(name + IsStanding());
    }

    public bool IsStanding()
    {
        Vector3 rotationInEuler = transform.rotation.eulerAngles;
        float tilltX = Mathf.Abs(270 - rotationInEuler.x);
        float tilltZ = Mathf.Abs(rotationInEuler.z);

        if ((tilltX < standingTreshold || tilltX > standingTreshold360) && (tilltZ < standingTreshold || tilltZ > standingTreshold360))
        {
            return true;
        }
        else {
            return false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(name + IsStanding());
    }    
}
