using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Team
{
    private Color color;
    private int score;
    private GameObject character;

    public Team(Color thisColor, GameObject thisCharacter){
        color = thisColor;
        score = 0;
        character = thisCharacter;
    }

    public GameObject GetCharacter(){
        return character;
    }

    public void SetCharacter(GameObject thisCharacter){
        character = thisCharacter;
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
}
