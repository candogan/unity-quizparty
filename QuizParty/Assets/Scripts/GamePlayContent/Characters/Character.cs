using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameFieldHandler;
using static NextFieldDirectionEnum;

public class Character : MonoBehaviour
{
    private Animator character_Animator;
    private GameFieldHandler gameFieldHandler;

    private int actualFieldIndex = 0;
    private bool readyForNextMove = true;
    private string movingDirection;
    private float fieldDistance = 0;
    private int fieldsToMove = 0;
    private int fieldCount;
    private int turnDegree;
    private bool needsTurn = true;

    private Vector3 initFieldPosition;
    private Vector3 initCharacterPosition;
    private Vector3 currentLocation;
    private Vector3 nextFieldLocation;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartClass()
    {
        character_Animator = gameObject.GetComponent<Animator>();

        gameFieldHandler = FindObjectOfType<GameFieldHandler>();
        fieldCount = gameFieldHandler.GetFieldCount();
    }

    public int GetActualFieldIndex()
    {
        return this.actualFieldIndex;
    }

    // Update is called once per frame
    void Update()
    {
        currentLocation = transform.position;
        if(0 < fieldsToMove && readyForNextMove){
            SpecifyMovingParameters();

        } else if(0 < fieldsToMove && !readyForNextMove){

            if (movingDirection == NextFieldDirectionEnum.X_ACHSIS_DOWN){
                //Debug.Log("X-DOWN, Field-ID: " + actualFieldIndex);
                MoveCharacterXDown();
            } else if(movingDirection == NextFieldDirectionEnum.X_ACHSIS_UP){
                //Debug.Log("X-UP, Field-ID: " + actualFieldIndex);
                MoveCharacterXUp();
            } else if (movingDirection == NextFieldDirectionEnum.Z_ACHSIS_DOWN){
                //Debug.Log("Z-Down, Field-ID: " + actualFieldIndex);
                MoveCharacterZDown();
            } else if(movingDirection == NextFieldDirectionEnum.Z_ACHSIS_UP){
                //Debug.Log("Z-Up, Field-ID: " + actualFieldIndex);
                MoveCharacterZUp();
            }
        }
    }

    public void TransferDiceResult(int diceResult){
        fieldsToMove = diceResult;
    }

    public bool charcterIsOnTargetField(){
        return fieldsToMove == 0;
    }

    private void SpecifyMovingParameters(){
        initFieldPosition = gameFieldHandler.GetLocationOfFieldindex(actualFieldIndex);
        initCharacterPosition = transform.position;
        movingDirection = gameFieldHandler.GetNextFieldMoveOfFieldindex(actualFieldIndex);
        nextFieldLocation = gameFieldHandler.GetLocationOfFieldindex((actualFieldIndex + 1) % fieldCount);
        // Debug.Log("Richtung: " + movingDirection + ", Ziel: " + nextFieldLocation + ",actualFieldIndex " + actualFieldIndex + ", readyForNextMove" + readyForNextMove);
        readyForNextMove = false;
        turnDegree = gameFieldHandler.GetNextFieldTurnDegree(actualFieldIndex);



        if (movingDirection == NextFieldDirectionEnum.X_ACHSIS_DOWN){
            //Debug.Log("X-DOWN, Field-ID: " + actualFieldIndex);
            fieldDistance = initFieldPosition.x - nextFieldLocation.x;
        } else if(movingDirection == NextFieldDirectionEnum.X_ACHSIS_UP){
            //Debug.Log("X-UP, Field-ID: " + actualFieldIndex);
            fieldDistance = nextFieldLocation.x - initFieldPosition.x;
        } else if (movingDirection == NextFieldDirectionEnum.Z_ACHSIS_DOWN){
            //Debug.Log("Z-Down, Field-ID: " + actualFieldIndex);
            fieldDistance = initFieldPosition.z - nextFieldLocation.z;
        } else if(movingDirection == NextFieldDirectionEnum.Z_ACHSIS_UP){
            //Debug.Log("Z-Up, Field-ID: " + actualFieldIndex);
            fieldDistance = nextFieldLocation.z - initFieldPosition.z;
        }
        // Debug.Log("Distanz: " + fieldDistance);
    }


    private void MoveCharacterXDown(){
        if (currentLocation.x >= initCharacterPosition.x - fieldDistance) {
                //character_Animator.SetTrigger("Run In Place");
                //Temp Fix um Exception zu umgehen
                if(needsTurn){
                    transform.Rotate(0, turnDegree, 0);
                }
                needsTurn = false;  
                Vector3 destiny = new Vector3(-8, 0, 0) * Time.deltaTime;
                transform.Translate(destiny, Space.World);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed, Space.Self);
            if (!readyForNextMove){
                actualFieldIndex = (actualFieldIndex + 1) % fieldCount;
                fieldsToMove -= 1;
            }
            readyForNextMove = true;
            needsTurn = true;
        }
    }

    private void MoveCharacterXUp(){
        if (currentLocation.x <= initCharacterPosition.x + fieldDistance) {
                //character_Animator.SetTrigger("Run In Place");
                //Temp Fix um Exception zu umgehen
                if(needsTurn){
                    transform.Rotate(0, turnDegree, 0);
                }
                needsTurn = false;
                Vector3 destiny = new Vector3(8, 0, 0) * Time.deltaTime;
                transform.Translate(destiny, Space.World);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed, Space.Self);
            if (!readyForNextMove){
                actualFieldIndex = (actualFieldIndex + 1) % fieldCount;
                fieldsToMove -= 1;
            }
            readyForNextMove = true;
            needsTurn = true;
        }
    }

    private void MoveCharacterZDown(){
        if (currentLocation.z >= initCharacterPosition.z - fieldDistance) {
                //character_Animator.SetTrigger("Run In Place");
                //Temp Fix um Exception zu umgehen
                if(needsTurn){
                    transform.Rotate(0, turnDegree, 0);
                }
                needsTurn = false;
                Vector3 destiny = new Vector3(0, 0, -8) * Time.deltaTime;
                transform.Translate(destiny, Space.World);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed, Space.Self);
            if (!readyForNextMove){
                actualFieldIndex = (actualFieldIndex + 1) % fieldCount;
                fieldsToMove -= 1;
            }
            readyForNextMove = true;
            needsTurn = true;
        }
    }

    private void MoveCharacterZUp(){
        if (currentLocation.z <= initCharacterPosition.z + fieldDistance) {
                //character_Animator.SetTrigger("Run In Place");
                //Temp Fix um Exception zu umgehen
                if(needsTurn){
                    transform.Rotate(0, turnDegree, 0);
                }
                needsTurn = false;
                Vector3 destiny = new Vector3(0, 0, 8) * Time.deltaTime;
                transform.Translate(destiny, Space.World);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed, Space.Self);
            if (!readyForNextMove){
                actualFieldIndex = (actualFieldIndex + 1) % fieldCount;
                fieldsToMove -= 1;
            }
            readyForNextMove = true;
            needsTurn = true;
        }
    }
}
