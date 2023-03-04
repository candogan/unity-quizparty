using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFieldHandler : MonoBehaviour
{
    private List<GameField> gameFields = new List<GameField>();

    public int gameFieldLayout;

    // Start is called before the first frame update
    void Start()
    {
        if (gameFieldLayout == 1) {
            InitializeGameFields();
        } else {
            InitializeMinimalGameFields();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
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

        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (82)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (114)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (95)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.X_ACHSIS_DOWN, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (97)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (77)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        //gameFields.Add(new GameField(GameObject.Find("bridge_small (5)").transform.position, GameFieldTypeEnum.NOTHING, NextFieldDirectionEnum.X_ACHSIS_DOWN)); // bridge
        //gameFields.Add(new GameField(GameObject.Find("bridge_small (1)").transform.position, GameFieldTypeEnum.NOTHING, NextFieldDirectionEnum.X_ACHSIS_DOWN)); // bridge
        //gameFields.Add(new GameField(GameObject.Find("bridge_small (2)").transform.position, GameFieldTypeEnum.NOTHING, NextFieldDirectionEnum.X_ACHSIS_DOWN)); // bridge
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (119)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (108)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.Z_ACHSIS_DOWN, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (99)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (74)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_DOWN, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (47)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (111)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (98)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.Z_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (89)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (103)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (53)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (90)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (46)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (75)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (73)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (120)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (58)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (63)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        //gameFields.Add(new GameField(GameObject.Find("bridge_small (6)").transform.position, GameFieldTypeEnum.NOTHING, NextFieldDirectionEnum.X_ACHSIS_UP)); // bridge
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (65)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (85)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (87)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.X_ACHSIS_UP, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (106)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (59)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (109)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.Z_ACHSIS_UP, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (118)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.X_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (93)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (48)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (57)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (94)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (51)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (91)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (56)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_DOWN, 90));
    }

    private void InitializeMinimalGameFields()
    {
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (82)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (114)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (95)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.X_ACHSIS_DOWN, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (97)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.X_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (77)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.Z_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (85)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (87)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.Z_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (106)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.X_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (59)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (109)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.Z_ACHSIS_UP, -90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (118)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.X_ACHSIS_UP, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (93)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_UP, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (48)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 90));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (57)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (94)").transform.position, GameFieldTypeEnum.GUESSPICTURE, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (51)").transform.position, GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (91)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.Z_ACHSIS_DOWN, 0));
        gameFields.Add(new GameField(GameObject.Find("grass_with_soil_area (56)").transform.position, GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.X_ACHSIS_DOWN, 90));
    }
}
