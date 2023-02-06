using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameFieldTypeEnum;
using static NextFieldDirectionEnum;

public class GamePlayHandler : MonoBehaviour
{
    //Objecttyp zu liste gewechselt -> bietet einfacheren zugriff auf die values
    private List<GameField> gameFields = new List<GameField>();
    public CameraManager camera;
    public GameObject dice;
    public DiceScript diceSc;

    // Start is called before the first frame update
    void Start()
    {
        InitializeGameFields();
        InitializeClasses();
        RollDice();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RollDice()
    {
        camera.FocusDiceCamera();
        diceSc.TriggerDice();
        // You dont get the result. Implementation required!
        StartCoroutine(waiterForResult());
    }

    IEnumerator waiterForResult()
    {
        //Wait for 4 seconds
        yield return new WaitForSecondsRealtime(6);
        int diceValue = diceSc.getDiceValue();
        Debug.Log("Folgende Zahl wurde gewürfelt: " + diceValue);
    }

    public List<GameField> GetGameFieldList(){
        return gameFields;
    }

    public string GetNextFieldMoveOfFieldindex(int fieldindex){
        return gameFields[fieldindex].getNextFieldMove();
    }

    public Vector3 GetLocationOfFieldindex(int fieldindex){
        return gameFields[fieldindex].getPosition();
    }

    public int GetFieldCount(){
        return gameFields.Count;
    }

    private void InitializeClasses()
    {
        dice = GameObject.Find("Dice (1)");
        diceSc = (DiceScript) dice.GetComponent<DiceScript>();
        diceSc.StartClass();

        camera = new CameraManager();
        camera.StartClass();
    }

    private void InitializeGameFields()
    {
        /*
        Todo:
        - Gamefieldtypes einpflegen (hier am besten warten bis actionfield auch implementiert ist)
        */

        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (82)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (114)").transform.position, GameFieldTypeEnum.NOTHING, NextFieldDirectionEnum.Z_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (95)").transform.position, GameFieldTypeEnum.GUESSQUESTION, NextFieldDirectionEnum.X_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (97)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (77)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("bridge_small (5)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN)); // bridge
        gameFields.Add(new GameField(GameObject.Find("bridge_small (1)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN)); // bridge
        gameFields.Add(new GameField(GameObject.Find("bridge_small (2)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN)); // bridge
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (119)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (108)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (99)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (74)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (47)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (111)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (98)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (89)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (103)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (53)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (90)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (46)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (75)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (73)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (120)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (58)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (63)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("bridge_small (6)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP)); // bridge
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (65)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (85)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (87)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (106)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (59)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (109)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (118)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (93)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (48)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (57)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (94)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (51)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (91)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (56)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN));
    }
}
