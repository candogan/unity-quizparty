using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class GameField {
    Vector3 position;
    int type;
    string nextFieldMove;

    public GameField (Vector3 position, int type, string nextFieldMove) {
        this.position = position;
        this.type = type;
        this.nextFieldMove = nextFieldMove;
    }

    public Vector3 getPosition(){
        return position;
    }

    public void setPosition(Vector3 newPosition){
        posX = newPosition;
    }

    public void setNextFieldMove(string newNextFieldMove){
        nextFieldMove = newNextFieldMove;
    }

    public string getNextFieldMove(){
        return nextFieldMove;
    }

    public int getType(){
        return type;
    }

    public void setType(int newType){
        type = newType;
    }

}

