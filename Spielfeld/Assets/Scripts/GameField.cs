using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

[Serializable]
public class GameField {
    int posX;
    int posY;
    int posZ;
    int type;
    string nextFieldMove;

    public GameField (int posX, int posY, int posZ, int type, string nextFieldMove) {
        this.posX = posX;
        this.posY = posY;
        this.posZ = posZ;
        this.type = type;
        this.nextFieldMove = nextFieldMove;
    }

    public int getPosX(){
        return posX;
    }

    public int getPosY(){
        return posY;
    }

    public int getPosZ(){
        return posZ;
    }

    public void setPosX(int newPosX){
        posX = newPosX;
    }

    public void setPosY(int newPosY){
        posY = newPosY;
    }

    public void setPosZ(int newPosZ){
        posZ = newPosZ;
    }

    public int[] getPos(){
        int[] posArray = {posX, posY, posZ};
        return posArray;
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

