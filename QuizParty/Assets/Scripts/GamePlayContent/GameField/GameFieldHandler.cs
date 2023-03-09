using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameFieldHandler : MonoBehaviour
{
    private List<GameField> gameFields = new List<GameField>();

    public int gameFieldLayout;

    private List<int> difficulty;

    private List<GameEventField> eventFieldList;

    private List<int> gameFieldTypes = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        if (GameOptionsHandler.getNotNewGame()){
            eventFieldList = FileHandler.ReadListFromJSON<GameEventField> ("saveGameQuestions.json");
        }else{
            eventFieldList = FileHandler.ReadListFromJSON<GameEventField> ("GameFieldQuestions.json");
        }
        difficulty = GameOptionsHandler.getDifficulties();
        GenerateFieldTypeList();
        if (gameFieldLayout == 1) {
            InitializeGameFields();
        } else {
            InitializeMinimalGameFields();
        }
        updateFieldColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GenerateFieldTypeList(){
        foreach (GameEventField gameEventField in eventFieldList){
            if (difficulty.Contains(gameEventField.GetDifficulty()) && gameEventField.GetUsed() == 0 && gameEventField.GetFieldType() != 4){
                gameFieldTypes.Add(gameEventField.GetFieldType());
            }
        }
    }

    private int RandomFieldPicker(){
        System.Random rnd = new System.Random();
        int fieldType = gameFieldTypes[rnd.Next(0, gameFieldTypes.Count - 1)];
        return fieldType;
    }

    private void updateFieldColor(){
            foreach (GameField gameField in gameFields){
                
                switch (gameField.getType()){
                    
                    case 1:
                    gameField.getGameObject().GetComponent<Renderer>().material.SetColor("_Color", new Color(0.509434f, 0.2944579f, 0.08891062f, 1f));
                    break;
                    case 2:
                    gameField.getGameObject().GetComponent<Renderer>().material.SetColor("_Color", new Color(0.6450694f, 0.8802369f, 0.9056604f, 1f));
                    break;
                    case 3:
                    gameField.getGameObject().GetComponent<Renderer>().material.SetColor("_Color", new Color(0.06482618f, 0.3396226f, 0.01762193f, 1f));
                    break;
                    default:
                    gameField.getGameObject().GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, 1f, 1f, 1f));
                    break;
                }
            }
    }

    public List<GameField> GetGameFieldList(){
        return gameFields;
    }

    public string GetNextFieldMoveOfFieldindex(int fieldindex){
        return gameFields[fieldindex].getNextFieldMove();
    }

    public int GetNextFieldTurnDegree(int fieldindex){
        return gameFields[fieldindex].getTurnDegree();
    }

    public Vector3 GetLocationOfFieldindex(int fieldindex){
        return gameFields[fieldindex].getPosition();
    }

    public int GetFieldType(int fieldindex)
    {
        return gameFields[fieldindex].getType();
    }

    public int GetFieldCount(){
        return gameFields.Count;
    }


    private void InitializeGameFields()
    {
        /*
        Todo:
        - Gamefieldtypes einpflegen (hier am besten warten bis actionfield auch implementiert ist)
        */

        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (82)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (114)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (95)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (97)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (77)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        //gameFields.Add(new GameField(GameObject.Find("bridge_small (5)"), GameFieldTypeEnum.NOTHING, NextFieldDirectionEnum.X_ACHSIS_DOWN)); // bridge
        //gameFields.Add(new GameField(GameObject.Find("bridge_small (1)"), GameFieldTypeEnum.NOTHING, NextFieldDirectionEnum.X_ACHSIS_DOWN)); // bridge
        //gameFields.Add(new GameField(GameObject.Find("bridge_small (2)"), GameFieldTypeEnum.NOTHING, NextFieldDirectionEnum.X_ACHSIS_DOWN)); // bridge
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (119)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (108)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (99)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (74)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (47)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (111)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (98)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (89)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (103)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (53)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (90)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (46)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (75)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (73)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (120)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (58)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (63)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        //gameFields.Add(new GameField(GameObject.Find("bridge_small (6)"), GameFieldTypeEnum.NOTHING, NextFieldDirectionEnum.X_ACHSIS_UP)); // bridge
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (65)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (85)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (87)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (106)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (59)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (109)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (118)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (93)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (48)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (57)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (94)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (51)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (91)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (56)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, 90));
    }

        private void InitializeMinimalGameFields()
    {
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (82)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (114)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (95)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (97)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (77)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (85)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (87)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (106)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (59)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (109)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_UP, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (118)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (93)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (48)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (57)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (94)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (51)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (91)"), RandomFieldPicker(), NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (56)"), RandomFieldPicker(), NextFieldDirectionEnum.X_ACHSIS_DOWN, 90));
    }
}
