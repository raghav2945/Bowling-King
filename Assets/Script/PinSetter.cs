using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PinSetter : MonoBehaviour
{
    public Text standingDisplay;
    public int lastStandingCount = -1;
    public GameObject pinSet;
    public bool ballOutOfPlay = false;

    private float lastChangeTime;
    private Ball ball;
    private int lastSettledCount = 10;
    private ActionMaster actionMaster = new ActionMaster();
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.FindObjectOfType<Ball>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        standingDisplay.text = CountStanding().ToString();
        if (ballOutOfPlay) {
            CheckStanding();
            standingDisplay.color = Color.red;
        }
    }

    public void RaisePins() {
        //Debug.Log("Raising pins!!");
        foreach (Pin pin in GameObject.FindObjectsOfType<Pin>())
        {
            pin.Raise();
            pin.transform.rotation = Quaternion.Euler(270f, 0, 0);
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
        //Debug.Log("Renewing pins!!");
        //Instantiate(pinSet, new Vector3(0, 5, 1829), Quaternion.identity);
        GameObject newPins = Instantiate(pinSet);
        newPins.transform.position += new Vector3(0, 50, 0);
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

    /*public void OnTriggerEnter(Collider other)
    {
        GameObject thingHit = other.gameObject;

        //Ball enter play arena
        if (thingHit.GetComponent<Ball>())
        {
            ballOutOfPlay = true;
            standingDisplay.color = Color.red;
        }
    }*/

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
        int standingPin = CountStanding();
        int pinFall = lastSettledCount - standingPin;
        lastSettledCount = standingPin;

        ActionMaster.Action action = actionMaster.Bowl(pinFall);
        Debug.Log("PinFall : " + pinFall + " " + action);

        if (action == ActionMaster.Action.Tidy)
        {
            animator.SetTrigger("tidyTrigger");
        }
        else if (action == ActionMaster.Action.EndTurn)
        {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }
        else if (action == ActionMaster.Action.Reset)
        {
            animator.SetTrigger("resetTrigger");
            lastSettledCount = 10;
        }
        else if (action == ActionMaster.Action.EndGame) {
            throw new UnityException("Don't know how to handle end game yet");
        }
        ball.Reset();
        lastStandingCount = -1;
        ballOutOfPlay = false;
        standingDisplay.color = Color.green;
    }
}
