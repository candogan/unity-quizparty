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

        if (Input.GetKeyDown(KeyCode.Escape)) {
            Application.Quit();
        }
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
            StartCoroutine(WaitASecond(3));
            diceValue = diceSc.getDiceValue();
            camera.FocusPlayerCamera();
            characterOneSc.TransferDiceResult(diceValue);
            
            //Debug.Log("Moving Character");
            gameState = GameStateEnum.WAITING_FOR_MOVING_CHARACTER;
        } else if (gameState == GameStateEnum.WAITING_FOR_MOVING_CHARACTER && characterOneSc.charcterIsOnTargetField() && waiting == false){
            waiting = true;
            ManageRoundState();
            StartCoroutine(WaitASecond(3));
            //Debug.Log("Question Mode");
            camera.FocusSideCamera();
            StartQuestion();
            gameState = GameStateEnum.QUESTION_MODE;
        } else if (gameState == GameStateEnum.QUESTION_MODE && !panelUiManager.UiIsActive() && waiting == false){
            //Debug.Log("Preparing next Round");
            if(lastMoveThisRound){
                questionManager.StartNewQuestion(actualTeamCount, GameFieldTypeEnum.GUESSQUESTION);
                gameState = GameStateEnum.ROUND_ENDED_AND_ESTIMATION_TASK;
            } else {
                gameState = GameStateEnum.PREPARING_NEXT_ROUND;
            }

        //Wenn alle Teams in einer Runde dran waren, wird eine Schaetzfrage getriggert
        } else if(gameState == GameStateEnum.ROUND_ENDED_AND_ESTIMATION_TASK && !panelUiManager.UiIsActive() && waiting == false){
            gameState = GameStateEnum.PREPARING_NEXT_ROUND;


        } else if(gameState == GameStateEnum.PREPARING_NEXT_ROUND && waiting == false){
            //Debug.Log("Changing Team");
            if (actualTeamCount < teamCount - 1 ){
                actualTeamCount += 1;
            } else {
                actualTeamCount = 0;
                actualRoundCount += 1;
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
        } else {
            gameState = GameStateEnum.PREPARING_NEXT_ROUND;
        }
    }

    public void InitzializeRoundForTeam()
    {
        Debug.Log("Team: " +  actualTeamCount + ", Runde: " + actualRoundCount);
        characterOne = teamHandler.getCharacterOfTeamindex(actualTeamCount);
        characterOneSc = (Character) characterOne.GetComponent<Character>();
        camera.setPlayerCamera(characterOneSc);
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

    //Prueft ob die Runde grade beim letzten Teamzug
    private void ManageRoundState(){
        if(actualTeamCount >= teamCount - 1){
            lastMoveThisRound = true;
        } else {
            lastMoveThisRound = false;
        }
    }

    IEnumerator WaitASecond(int secondsToWait)
    {
        // Wait for x seconds
        yield return new WaitForSecondsRealtime(secondsToWait);
        waiting = false;
    }
}
