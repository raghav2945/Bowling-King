using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneBox : MonoBehaviour
{
    private PinSetter pinSetter;
    // Start is called before the first frame update
    void Start()
    {
        pinSetter = GameObject.FindObjectOfType<PinSetter>();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Ball") {
            pinSetter.ballOutOfPlay = true; 
        }
    }
}
