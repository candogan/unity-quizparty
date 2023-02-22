using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class GameField {
    Vector3 position;
    int type;
    string nextFieldMove;
    int turnDegree;

    public GameField (Vector3 position, int type, string nextFieldMove, int turnDegree) {
        this.position = position;
        this.type = type;
        this.nextFieldMove = nextFieldMove;
        this.turnDegree = turnDegree;
    }

    public Vector3 getPosition(){
        return position;
    }

    public int getTurnDegree(){
        return turnDegree;
    }

    public void setPosition(Vector3 newPosition){
        position = newPosition;
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

