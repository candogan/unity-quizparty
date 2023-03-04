using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOptionsHandler : MonoBehaviour
{
    
    private static float teamCount;
    private static float roundCount;

    private static bool difficulty1;
    private static bool difficulty2;
    private static bool difficulty3;

    private static List<int> difficulties;

    public static bool notNewGame = false;

    public Slider teamSlider;
    public Slider roundSlider;

    public Toggle diffOneToggle;
    public Toggle diffTwoToggle;
    public Toggle diffThreeToggle;

    // Initialisieren der Variablen bei Start des Spiels
    void Start()
    {
        initialiazeVariables();
    }

    public void PlayGame ()
    {
        //Variablen der ausgewählten Schwierigkeit anpassen
        updateDiffList();
        //Prüfen ob mindestens eine Schwierigkeit ausgewählt wurde
        //Spiel starten
        checkDiffSelection();
    }

    public void PlayOldGame(){
        teamCount = PlayerPrefs.GetInt("teamCount");
        roundCount = PlayerPrefs.GetInt("roundCount");
        difficulty1 = true;
        difficulty2 = true;
        difficulty3 = true;
        difficulties = new List<int>();
        notNewGame = true;
        if (roundCount < 5){
            SceneManager.LoadScene("QuizPartyMinimal");
        } else if (roundCount > 4 && roundCount < 9){
            SceneManager.LoadScene("QuizParty");
        }
    }

    //Initialisieren der Variablen
    private void initialiazeVariables()
    {
        teamCount = 2;
        roundCount = 3;
        difficulty1 = true;
        difficulty2 = true;
        difficulty3 = true;
        difficulties = new List<int>();
        if (difficulty1 == true)
        {
            difficulties.Add(1);
        }
        if (difficulty2 == true)
        {
            difficulties.Add(2);
        }
        if (difficulty3 == true)
        {
            difficulties.Add(3);
        }
    }

    //Variablen der ausgewählten Schwierigkeit anpassen
    private void updateDiffList()
    {
        difficulty1 = diffOneToggle.isOn;
        difficulty2 = diffTwoToggle.isOn;
        difficulty3 = diffThreeToggle.isOn;

        if (difficulty1 == true)
        {
            difficulties.Add(1);
        }
        if (difficulty2 == true)
        {
            difficulties.Add(2);
        }
        if (difficulty3 == true)
        {
            difficulties.Add(3);
        }
    }

    //Prüfen ob mindestens eine Schwierigkeit ausgewählt wurde
    //Spiel starten
    private void checkDiffSelection(){
        if (difficulty1 == true || difficulty2 == true || difficulty3 == true)
        {
            teamCount = teamSlider.value;
            roundCount  = roundSlider.value;
            if (roundCount < 5){
                SceneManager.LoadScene("QuizPartyMinimal");
            } else if (roundCount > 4 && roundCount < 9){
                SceneManager.LoadScene("QuizParty");
            }
        } 
        else
        {
            difficulties.Clear();
        }
    }

    public static float getTeamCount()
    {
        return teamCount;      
    }

    public static float getRoundCount()
    {
        return roundCount;      
    }

    public static bool getDiff1()
    {
        return difficulty1;      
    }

    public static bool getDiff2()
    {
        return difficulty2;      
    }

    public static bool getDiff3()
    {
        return difficulty3;      
    }

    public static List<int> getDifficulties()
    {
        return difficulties;      
    }
}
