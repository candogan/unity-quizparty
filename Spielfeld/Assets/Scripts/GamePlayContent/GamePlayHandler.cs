using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using static GameFieldTypeEnum;
using static NextFieldDirectionEnum;

public class GamePlayHandler : MonoBehaviour
{

    public GameField[] gameFields;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameField[] prepareGameFields()
    {
        // TODO

        GameField[] gameFieldArray = {
            new GameField(new Vector3((float)-123.8314,(float) 71.35801,(float) -6.518653), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-131.8314,(float) 71.35801,(float) -6.518653), GameFieldTypeEnum.NOTHING, NextFieldDirectionEnum.RIGHT),
            new GameField(new Vector3((float)-131.8314,(float) 71.35801,(float) 1.481347), GameFieldTypeEnum.GUESSQUESTION, NextFieldDirectionEnum.LEFT),
            new GameField(new Vector3((float)-139.8314,(float) 71.35801,(float) 1.481347), GameFieldTypeEnum.KNOWLEDGE, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-147.8314,(float) 71.35801,(float) 1.481347), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-155.8314,(float) 71.35801,(float) 1.481347), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT), // bridge
            new GameField(new Vector3((float)-163.8314,(float) 71.35801,(float) 1.481347), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT), // bridge
            new GameField(new Vector3((float)-171.8314,(float) 71.35801,(float) 1.481347), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT), // bridge
            new GameField(new Vector3((float)-179.8314,(float) 71.35801,(float) 1.481347), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-187.8314,(float) 71.35801,(float) 1.481347), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.LEFT),
            new GameField(new Vector3((float)-187.8314,(float) 71.35801,(float) -6.518653), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-187.8314,(float) 71.35801,(float) -14.51865), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.RIGHT),
            new GameField(new Vector3((float)-195.8314,(float) 71.35801,(float) -14.51865), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-203.8314,(float) 71.35801,(float) -14.51865), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-211.8314,(float) 71.35801,(float) -14.51865), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.RIGHT),
            new GameField(new Vector3((float)-211.8314,(float) 71.35801,(float) -6.518653), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-211.8314,(float) 71.35801,(float) 1.481347), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-211.8314,(float) 71.35801,(float) 9.481347), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-211.8314,(float) 71.35801,(float) 17.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-211.8314,(float) 71.35801,(float) 25.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-211.8314,(float) 71.35801,(float) 33.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.RIGHT),
            new GameField(new Vector3((float)-203.8314,(float) 71.35801,(float) 33.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-195.8314,(float) 71.35801,(float) 33.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-187.8314,(float) 71.35801,(float) 33.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-179.8314,(float) 71.35801,(float) 33.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-171.8314,(float) 71.35801,(float) 33.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT), // bridge
            new GameField(new Vector3((float)-163.8314,(float) 71.35801,(float) 33.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-155.8314,(float) 71.35801,(float) 33.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.RIGHT),
            new GameField(new Vector3((float)-155.8314,(float) 71.35801,(float) 25.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.LEFT),
            new GameField(new Vector3((float)-147.8314,(float) 71.35801,(float) 25.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-139.8314,(float) 71.35801,(float) 25.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-131.8314,(float) 71.35801,(float) 25.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.LEFT),
            new GameField(new Vector3((float)-131.8314,(float) 71.35801,(float) 33.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.RIGHT),
            new GameField(new Vector3((float)-123.8314,(float) 71.35801,(float) 33.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-115.8314,(float) 71.35801,(float) 33.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.RIGHT),
            new GameField(new Vector3((float)-115.8314,(float) 71.35801,(float) 25.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-115.8314,(float) 71.35801,(float) 17.48135), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-115.8314,(float) 71.35801,(float) 9.481347), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-115.8314,(float) 71.35801,(float) 1.481347), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.STRAIGHT),
            new GameField(new Vector3((float)-115.8314,(float) 71.35801,(float) -6.518653), GameFieldTypeEnum.INTERACTION, NextFieldDirectionEnum.RIGHT),
        };

        return gameFieldArray;
    }
}
