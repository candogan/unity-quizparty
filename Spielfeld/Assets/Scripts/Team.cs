using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    private Color color;
    private int score;
    private int positionOnMap;

    public Team(Color thisColor, int thisScore, int thisPositionOnMap){
        color = thisColor;
        score = thisScore;
        positionOnMap = thisPositionOnMap;
    }


    public Color GetColor(){
        return color;
    }

    public void SetColor(Color newColor){
        color = newColor;
    }

    public int GetScore(){
        return score;
    }

    public void SetScore(int newScore){
        score = newScore;
    }

    public void addOrTakePointsToScore(int pointsToAddOrTake){
        score += pointsToAddOrTake;
    }

    public int GetPositionOnMap(){
        return positionOnMap;
    }

    public void SetPositionOnMap(int newPositionOnMap){
        positionOnMap = newPositionOnMap;
    }

    public void MovePositionOnMap(int fieldsToMove){
        positionOnMap += fieldsToMove;
    }

}
