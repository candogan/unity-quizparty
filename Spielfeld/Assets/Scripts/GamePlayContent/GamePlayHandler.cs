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
    private int roundCount;
    private int actualRoundCount = 1;
    private int teamCount;
    private int actualTeamCount = 0;
    private bool lastMoveThisRound = false;
    private bool waiting = false;

    private int gameState = GameStateEnum.INITIALIZING;

    // Start is called before the first frame update
    void Start()
    {
        InitializeClasses();
    }

    // Update is called once per frame
    void Update()
    {
        ManageState();
    }

    // State Manager für die Rundenlogik -> generische wartezeiten, je nachdem wie lange der Würfe / Character für die aktive Handlung benötigt
    private void ManageState(){
        if(gameState == GameStateEnum.SWITCHING_ACTIVE_TEAM){
            //Debug.Log("Roundchange");
            if (actualTeamCount < teamCount && actualRoundCount < roundCount +1){
                InitzializeRoundForTeam();
                gameState = GameStateEnum.ROLLING_DICE;
            } else {
                gameState = GameStateEnum.GAME_FINISHED;
            }
        } else if(gameState == GameStateEnum.ROLLING_DICE){
            //Debug.Log("Waiting for Dice");
            RollDice();
            gameState = GameStateEnum.WAITING_FOR_DICE;
        } else if (gameState == GameStateEnum.WAITING_FOR_DICE && diceSc.DiceIsDone() && waiting == false){
            waiting = true;
            StartCoroutine(WaitASecond());
            diceValue = diceSc.getDiceValue();
            camera.FocusSideCamera();
            characterOneSc.TransferDiceResult(diceValue);
            //Debug.Log("Moving Character");
            gameState = GameStateEnum.WAITING_FOR_MOVING_CHARACTER;
        } else if (gameState == GameStateEnum.WAITING_FOR_MOVING_CHARACTER && characterOneSc.charcterIsOnTargetField() && waiting == false){
            waiting = true;
            StartCoroutine(WaitASecond());
            //Debug.Log("Question Mode");
            StartQuestion();
            gameState = GameStateEnum.QUESTION_MODE;
        } else if (gameState == GameStateEnum.QUESTION_MODE && !panelUiManager.UiIsActive() && waiting == false){
            waiting = true;
            StartCoroutine(WaitASecond());
            //Debug.Log("Preparing next Round");
            gameState = GameStateEnum.PREPARING_NEXT_ROUND;
        } else if(gameState == GameStateEnum.PREPARING_NEXT_ROUND){
            //Debug.Log("Changing Team");
            if (actualTeamCount < teamCount -1 ){
                actualTeamCount += 1;
                lastMoveThisRound = false;
            } else {
                actualTeamCount = 0;
                actualRoundCount += 1;
                lastMoveThisRound = true;
            }
            gameState = GameStateEnum.SWITCHING_ACTIVE_TEAM;
        }
    }

    private void StartQuestion()
    {
        int fieldIndex = characterOneSc.GetActualFieldIndex();
        int fieldType = gameFieldHandler.GetFieldType(fieldIndex);
        //Debug.Log("FIELDTYPE: " + fieldType);
        if (fieldType != GameFieldTypeEnum.NOTHING) {
            questionManager.StartNewQuestion(actualTeamCount, fieldType);
        }
    }

    public void InitzializeRoundForTeam()
    {
        Debug.Log("Team: " +  actualTeamCount + ", Runde: " + actualRoundCount);
        characterOne = teamHandler.getCharacterOfTeamindex(actualTeamCount);
        characterOneSc = (Character) characterOne.GetComponent<Character>();
        characterOneSc.StartClass();
    }

    public void RollDice()
    {
        camera.FocusDiceCamera();
        diceSc.TriggerDice();
    }


    private void InitializeClasses()
    {
        dice = GameObject.Find("Dice (1)");
        diceSc = (DiceScript) dice.GetComponent<DiceScript>();
        diceSc.StartClass();

        teamCount = (int) GameOptionsHandler.getTeamCount();
        roundCount = (int) GameOptionsHandler.getRoundCount();
        gameState = GameStateEnum.SWITCHING_ACTIVE_TEAM;
    }

    public bool isLastMoveThisRound(){
        return lastMoveThisRound;
    }

    public int GetActualRound(){
        return actualRoundCount;
    }

    IEnumerator WaitASecond()
    {
        // Wait for x seconds
        yield return new WaitForSecondsRealtime(3);
        waiting = false;
    }
}
