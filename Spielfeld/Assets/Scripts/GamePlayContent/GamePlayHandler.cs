using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameFieldTypeEnum;
using static NextFieldDirectionEnum;

public class GamePlayHandler : MonoBehaviour
{
    //Objecttyp zu liste gewechselt -> bietet einfacheren zugriff auf die values
    public CameraManager camera;
    public GameObject dice;
    public DiceScript diceSc;
    public int diceValue;
    public GameObject characterOne;
    public Character characterOneSc;
    private bool oneRoundFinished;

    private float teamCount;
    private float roundCount;

    // Start is called before the first frame update
    void Start()
    {
        InitializeClasses();
        RollDice();
    }

    // Update is called once per frame
    void Update()
    {
        if (oneRoundFinished) {
            oneRoundFinished = false;
            RollDice();
        }
    }

    public void RollDice()
    {
        camera.FocusDiceCamera();
        diceSc.TriggerDice();
        // You dont get the result. Implementation required!
        StartCoroutine(waitForResult());
    }

    IEnumerator waitForResult()
    {
        //Wait for x seconds
        yield return new WaitForSecondsRealtime(5);
        diceValue = diceSc.getDiceValue();
        //Debug.Log("Folgende Zahl wurde gewürfelt: " + diceValue);
        camera.FocusSideCamera();
        characterOneSc.TransferDiceResult(diceValue);
        StartCoroutine(waitCharacterToMove());
    }

    IEnumerator waitCharacterToMove()
    {
        // Wait for x seconds
        yield return new WaitForSecondsRealtime(7);
        oneRoundFinished = true;
    }

    private void InitializeClasses()
    {
        camera = new CameraManager();
        camera.StartClass();

        dice = GameObject.Find("Dice (1)");
        diceSc = (DiceScript) dice.GetComponent<DiceScript>();
        diceSc.StartClass();

        characterOne = GameObject.Find("Toon Chicken Team 1");
        characterOneSc = (Character) characterOne.GetComponent<Character>();
        characterOneSc.StartClass();

        teamCount = GameOptionsHandler.getTeamCount();
        roundCount = GameOptionsHandler.getRoundCount();
    }
}
