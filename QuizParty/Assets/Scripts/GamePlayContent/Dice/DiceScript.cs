using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceScript : MonoBehaviour
{
    Rigidbody rb;
    bool classInitialized;
    bool hasLanded;
    bool thrown;
    Vector3 initPosition;
    public int diceValue;
    public DiceSide[] diceSides;

    public void Start()
    {
    }

    public void StartClass()
    {
        rb = GetComponent<Rigidbody>();
        initPosition = transform.position;
        rb.useGravity = false;
    }

    void Update()
    {
        if (rb.IsSleeping() && !hasLanded && thrown) {
            hasLanded = true;
            rb.useGravity = false;
            rb.isKinematic = true;

            SideValueCheck();
        } else if (DiceStandsOnEdge()) {
            RollAgain();
        }
    }

    private bool DiceStandsOnEdge(){
        return rb.IsSleeping() && hasLanded && diceValue == 0;
    }

    public bool DiceIsDone (){
        return transform.position != initPosition && thrown == true && hasLanded == true && !DiceStandsOnEdge();
    }

    public void TriggerDice()
    {
        Reset();
        RollDice();
    }

    public int getDiceValue()
    {
        return diceValue;
    }

    private void RollDice()
    {
        if (!thrown && !hasLanded) {
            thrown = true;
            rb.useGravity = true;
            rb.AddTorque(Random.Range(0, 100000000), Random.Range(0, 100000000), Random.Range(0, 100000000));
        }
        else if (thrown && hasLanded) {
            Reset();
        }
    }

    private void Reset()
    {
        transform.position = initPosition;
        thrown = false;
        hasLanded = false;
        rb.useGravity = false;
        rb.isKinematic = false;
    }

    private void RollAgain()
    {
        Reset();
        thrown = true;
        rb.useGravity = true;
        rb.AddTorque(Random.Range(0, 100000000), Random.Range(0, 100000000), Random.Range(0, 100000000));
    }

    private void SideValueCheck()
    {
        diceValue = 0;
        foreach (DiceSide side in diceSides) {
            if (side.OnGround()) {
                diceValue = side.sideValue;
                // Debug.Log(diceValue + " has been rolled!");
            }
        }
    }
}
