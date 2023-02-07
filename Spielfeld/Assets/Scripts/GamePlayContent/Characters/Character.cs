using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GamePlayHandler;
using static NextFieldDirectionEnum;

public class Character : MonoBehaviour
{
    private Animator character_Animator;
    private GamePlayHandler gamePlayHandler;

    private int actualFieldIndex = 0;
    private bool readyForNextMove = true;
    private string movingDirection;
    private float fieldDistance = 0;
    private int fieldsToMove = 0;
    private int fieldCount;

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

        gamePlayHandler = FindObjectOfType<GamePlayHandler>();
        fieldCount = gamePlayHandler.GetFieldCount();
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

    private void SpecifyMovingParameters(){
        initFieldPosition = gamePlayHandler.GetLocationOfFieldindex(actualFieldIndex);
        initCharacterPosition = transform.position;
        movingDirection = gamePlayHandler.GetNextFieldMoveOfFieldindex(actualFieldIndex);
        nextFieldLocation = gamePlayHandler.GetLocationOfFieldindex((actualFieldIndex + 1) % fieldCount);
        // Debug.Log("Richtung: " + movingDirection + ", Ziel: " + nextFieldLocation + ",actualFieldIndex " + actualFieldIndex + ", readyForNextMove" + readyForNextMove);
        readyForNextMove = false;


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
                Vector3 destiny = new Vector3(0, 0, 8) * Time.deltaTime;
                transform.Translate(destiny);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed);
            if (!readyForNextMove){
                actualFieldIndex = (actualFieldIndex + 1) % fieldCount;
                fieldsToMove -= 1;
            }
            readyForNextMove = true;
        }
    }

    private void MoveCharacterXUp(){
        if (currentLocation.x <= initCharacterPosition.x + fieldDistance) {
                //character_Animator.SetTrigger("Run In Place");
                //Temp Fix um Exception zu umgehen
                Vector3 destiny = new Vector3(0, 0, -8) * Time.deltaTime;
                transform.Translate(destiny);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed);
            if (!readyForNextMove){
                actualFieldIndex = (actualFieldIndex + 1) % fieldCount;
                fieldsToMove -= 1;
            }
            readyForNextMove = true;
        }
    }

    private void MoveCharacterZDown(){
        if (currentLocation.z >= initCharacterPosition.z - fieldDistance) {
                //character_Animator.SetTrigger("Run In Place");
                //Temp Fix um Exception zu umgehen
                Vector3 destiny = new Vector3(-8, 0, 0) * Time.deltaTime;
                transform.Translate(destiny);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed);
            if (!readyForNextMove){
                actualFieldIndex = (actualFieldIndex + 1) % fieldCount;
                fieldsToMove -= 1;
            }
            readyForNextMove = true;
        }
    }

    private void MoveCharacterZUp(){
        if (currentLocation.z <= initCharacterPosition.z + fieldDistance) {
                //character_Animator.SetTrigger("Run In Place");
                //Temp Fix um Exception zu umgehen
                Vector3 destiny = new Vector3(8, 0, 0) * Time.deltaTime;
                transform.Translate(destiny);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed);
            if (!readyForNextMove){
                actualFieldIndex = (actualFieldIndex + 1) % fieldCount;
                fieldsToMove -= 1;
            }
            readyForNextMove = true;
        }
    }
}
