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
    private int goalFieldIndex = 30;
    private bool readyForNextMove = true;
    string movingDirection;
    private float fieldDistance = 0;

    private Vector3 initPosition;
    private Vector3 currentLocation;
    private Vector3 goalLocation;

    // Start is called before the first frame update
    void Start()
    {
        character_Animator = gameObject.GetComponent<Animator>();

        gamePlayHandler = FindObjectOfType<GamePlayHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        currentLocation = transform.position;
        if(actualFieldIndex < goalFieldIndex && readyForNextMove){
            SpecifyMovingParameters();

        } else if(actualFieldIndex < goalFieldIndex && !readyForNextMove){

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
        goalFieldIndex += diceResult;
    }

    private void SpecifyMovingParameters(){
        initPosition = transform.position;
        movingDirection = gamePlayHandler.GetNextFieldMoveOfFieldindex(actualFieldIndex);
        goalLocation = gamePlayHandler.GetLocationOfFieldindex(actualFieldIndex+1);
        Debug.Log("Richtung: " + movingDirection + ", Ziel: " + goalLocation + ",actualFieldIndex " + actualFieldIndex + ", readyForNextMove" + readyForNextMove);
        readyForNextMove = false;


        if (movingDirection == NextFieldDirectionEnum.X_ACHSIS_DOWN){
            //Debug.Log("X-DOWN, Field-ID: " + actualFieldIndex);
            fieldDistance = initPosition.x - goalLocation.x;
        } else if(movingDirection == NextFieldDirectionEnum.X_ACHSIS_UP){
            //Debug.Log("X-UP, Field-ID: " + actualFieldIndex);
            fieldDistance = goalLocation.x - initPosition.x;
        } else if (movingDirection == NextFieldDirectionEnum.Z_ACHSIS_DOWN){
            //Debug.Log("Z-Down, Field-ID: " + actualFieldIndex);
            fieldDistance = initPosition.z - goalLocation.z;
        } else if(movingDirection == NextFieldDirectionEnum.Z_ACHSIS_UP){
            //Debug.Log("Z-Up, Field-ID: " + actualFieldIndex);
            fieldDistance = goalLocation.z - initPosition.z;
        }
        Debug.Log("Distanz: " + fieldDistance);
    }


    private void MoveCharacterXDown(){
        Debug.Log("currentLocation: " + currentLocation + ", initPosition");
        if (currentLocation.x >= initPosition.x - fieldDistance) {
                //character_Animator.SetTrigger("Run In Place");
                //Temp Fix um Exception zu umgehen
                Vector3 destiny = new Vector3(0, 0, 8) * Time.deltaTime;
                transform.Translate(destiny);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed);
            if (!readyForNextMove){
                actualFieldIndex += 1;
            }
            readyForNextMove = true;
        }
    }

    private void MoveCharacterXUp(){
        if (currentLocation.x <= initPosition.x + fieldDistance) {
                //character_Animator.SetTrigger("Run In Place");
                //Temp Fix um Exception zu umgehen
                Vector3 destiny = new Vector3(0, 0, -8) * Time.deltaTime;
                transform.Translate(destiny);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed);
            if (!readyForNextMove){
                actualFieldIndex += 1;
            }
            readyForNextMove = true;
        }
    }

    private void MoveCharacterZDown(){
        if (currentLocation.z >= initPosition.z - fieldDistance) {
                //character_Animator.SetTrigger("Run In Place");
                //Temp Fix um Exception zu umgehen
                Vector3 destiny = new Vector3(-8, 0, 0) * Time.deltaTime;
                transform.Translate(destiny);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed);
            if (!readyForNextMove){
                actualFieldIndex += 1;
            }
            readyForNextMove = true;
        }
    }

    private void MoveCharacterZUp(){
        if (currentLocation.z <= initPosition.z + fieldDistance) {
                //character_Animator.SetTrigger("Run In Place");
                //Temp Fix um Exception zu umgehen
                Vector3 destiny = new Vector3(8, 0, 0) * Time.deltaTime;
                transform.Translate(destiny);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed);
            if (!readyForNextMove){
                actualFieldIndex += 1;
            }
            readyForNextMove = true;
        }
    }
}
