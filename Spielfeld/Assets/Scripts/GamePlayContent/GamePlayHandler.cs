using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameFieldTypeEnum;
using static NextFieldDirectionEnum;

public class GamePlayHandler : MonoBehaviour
{
    //Objecttyp zu liste gewechselt -> bietet einfacheren zugriff auf die values
    public CameraManager camera;
    public TeamHandler teamHandler;
    public GameOptionsHandler gameOptionsHandler;
    public GameObject dice;
    public DiceScript diceSc;
    public int diceValue;
    public GameObject characterOne;
    public Character characterOneSc;
    private bool oneRoundFinished;
    private bool oneTeamFinished;
    private int roundCount;
    private int actualRoundCount = 1;
    private int teamCount;
    private int actualTeamCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitializeClasses();
        StartCoroutine(startGameWithDelay());
    }

    // Wait for other Scripts to load
    IEnumerator startGameWithDelay()
    {
        // Wait for x seconds
        yield return new WaitForSecondsRealtime(5);

        Debug.Log("Erste Runde!");
        Debug.Log(teamCount);
        Debug.Log(roundCount);
        StartRoundForTeam();
    }

    // Update is called once per frame
    void Update()
    {
        if (oneTeamFinished) {
            oneTeamFinished = false;
            if (actualTeamCount < teamCount-1) {
                actualTeamCount += 1;
            } else {
                oneRoundFinished = true;
                actualTeamCount = 0;
            }

            if (oneRoundFinished) {
                oneRoundFinished = false;
                if (actualRoundCount < roundCount) {
                    actualRoundCount += 1;
                } else {
                    Debug.Log("KEINE RUNDE MEHR!");
                }
            }
            Debug.Log("Neue Runde!");
            StartRoundForTeam();
        }
    }

    public void StartRoundForTeam()
    {
        characterOne = teamHandler.getCharacterOfTeamindex(actualTeamCount);
        characterOneSc = (Character) characterOne.GetComponent<Character>();
        characterOneSc.StartClass();
        RollDice();
    }

    public void RollDice()
    {
        camera.FocusDiceCamera();
        diceSc.TriggerDice();
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
        oneTeamFinished = true;
    }

    private void InitializeClasses()
    {
        dice = GameObject.Find("Dice (1)");
        diceSc = (DiceScript) dice.GetComponent<DiceScript>();
        diceSc.StartClass();

        teamCount = (int) GameOptionsHandler.getTeamCount();
        roundCount = (int) GameOptionsHandler.getRoundCount();
    }
}
