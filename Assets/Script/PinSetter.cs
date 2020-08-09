using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public Text standingDisplay;
    public int lastStandingCount = -1;

    private bool ballEnteredBox = false;
    private float lastChangeTime;
    private Ball ball;
    
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        standingDisplay.text = CountStanding().ToString();
        if (ballEnteredBox) {
            CheckStanding();
        }
    }

    public void RaisePins() {
        Debug.Log("Raising pins!!");
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.Raise();
        }
    }

    public void LowerPins() {
        //Debug.Log("Lowering pins!!");
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.Lower();
        }
    }

    public void RenewPins()
    {
        Debug.Log("Renewing pins!!");
    }

    public int CountStanding() {
        int standing = 0;

        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
            {
                standing++;
            }                       
        }
        return standing;
    }

    public void OnTriggerEnter(Collider other)
    {
        GameObject thingHit = other.gameObject;

        //Ball enter play arena
        if (thingHit.GetComponent<Ball>())
        {
            ballEnteredBox = true;
            standingDisplay.color = Color.red;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        GameObject thingLeft = other.gameObject;

        if (thingLeft.GetComponent<Pin>()) {
            //Debug.Log("Pin left");
            Destroy(thingLeft);
        }
    }

    void CheckStanding() {
        int currentStanding = CountStanding();

        if (currentStanding != lastStandingCount) {
            lastChangeTime = Time.time;
            lastStandingCount = currentStanding;
            return;
        }
        float settleTime = 3f; // how long to wait to consider pin settled
        if ((Time.time - lastChangeTime) > settleTime) {
            PinsHaveSettled();
        }
    }

    void PinsHaveSettled() {
        ball.Reset();
        lastStandingCount = -1;
        ballEnteredBox = false;
        standingDisplay.color = Color.green;
    }
}
