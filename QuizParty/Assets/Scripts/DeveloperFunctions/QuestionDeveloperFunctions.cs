using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionDeveloperFunctions : MonoBehaviour
{
    public QuestionManager questionManager;
    
    private int actualTeam;


    public void SetTeamIndex(int teamIndex){
        actualTeam = teamIndex;
    }

    public void SetFieldTypeAndLoadTeaser(int fieldtype){
        questionManager.StartNewQuestion(actualTeam, fieldtype);
    }
}
