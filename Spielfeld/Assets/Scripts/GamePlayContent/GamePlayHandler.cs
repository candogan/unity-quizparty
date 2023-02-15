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
    public GameFieldHandler gameFieldHandler;
    public QuestionManager questionManager;
    public PanelUiManager panelUiManager;
    public GameObject dice;
    public DiceScript diceSc;
    public int diceValue;
    public GameObject characterOne;
    public Character characterOneSc;
    private bool oneTeamFinished = true;
    private bool gameFinished;
    private bool triggerQuestion = false;
    private bool finishedQuestion = true;
    private bool lastMoveThisRound = false;
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
        yield return new WaitForSecondsRealtime(2);

        /*
        Debug.Log("Erste Runde!");
        Debug.Log("Teamcount geladen: " + teamCount);
        Debug.Log("Roundcount geladen: " + roundCount);
        */
    }

    // Update is called once per frame
    void Update()
    {
        if (triggerQuestion) {
            // Debug.Log("Starte Frage für Team: " + actualTeamCount);
            StartQuestion();
        }

        if (gameFinished == false){
            ManageRoundLogic();
        }
    }


    private void ManageRoundLogic(){
        finishedQuestion = !panelUiManager.UiIsActive();

        if (finishedQuestion && oneTeamFinished && actualRoundCount <= roundCount) {
            oneTeamFinished = false;
            StartRoundForTeam();
            if (actualTeamCount < teamCount-1) {
                actualTeamCount += 1;
                lastMoveThisRound = false;
            } else {
                actualTeamCount = 0;
                actualRoundCount += 1;
                lastMoveThisRound = true;
            }
        } else if (actualRoundCount > roundCount){
            Debug.Log("Finished");
            gameFinished = true;
        }
    }

    private void StartQuestion()
    {
        triggerQuestion = false;
        int fieldIndex = characterOneSc.GetActualFieldIndex();
        int fieldType = gameFieldHandler.GetFieldType(fieldIndex);

        Debug.Log("FIELDTYPE: " + fieldType);
        if (fieldType == GameFieldTypeEnum.NOTHING) {
            finishedQuestion = true;
            return;
        } else {
            questionManager.StartNewQuestion(actualTeamCount, fieldType);
        }
    }

    public void StartRoundForTeam()
    {
        // Debug.Log("Team: " +  actualTeamCount + ", Runde: " + actualRoundCount);
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
        triggerQuestion = true;
    }

    private void InitializeClasses()
    {
        dice = GameObject.Find("Dice (1)");
        diceSc = (DiceScript) dice.GetComponent<DiceScript>();
        diceSc.StartClass();

        teamCount = (int) GameOptionsHandler.getTeamCount();
        roundCount = (int) GameOptionsHandler.getRoundCount();
    }

    public bool isLastMoveThisRound(){
        return lastMoveThisRound;
    }

    public int GetActualRound(){
        return actualRoundCount;
    }
}
