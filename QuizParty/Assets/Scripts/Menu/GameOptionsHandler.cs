using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;

public class GameOptionsHandler : MonoBehaviour
{
    
    private static float teamCount = 2;
    private static float roundCount = 3;

    private static bool difficulty1 = false;
    private static bool difficulty2 = false;
    private static bool difficulty3 = false;

    private static List<int> difficulties = new List<int>();

    public static bool notNewGame = false;

    public Slider teamSlider;
    public Slider roundSlider;

    public Toggle diffOneToggle;
    public Toggle diffTwoToggle;
    public Toggle diffThreeToggle;

    public GameObject textUI;
    public TMP_Text text;
    public TMP_Text mapText;
    public GameObject backgroundUI;


    // Initialisieren der Variablen bei Start des Spiels
    void Start()
    {
   
    }

    void Update(){
        if (roundSlider.value < 5){
            mapText.GetComponent <TMP_Text> ().text = "Kleine Karte";
        }else{
            mapText.GetComponent <TMP_Text> ().text = "Große Karte";
        }
    }

    public void PlayGame ()
    {
        //Variablen der ausgewählten Schwierigkeit anpassen
        updateToggle();
        updateDiffList();
        //Prüfen ob mindestens eine Schwierigkeit ausgewählt wurde
        //Spiel starten
        checkDiffSelection();
    }

    public void PlayOldGame(){
        teamCount = PlayerPrefs.GetInt("teamCount");
        roundCount = PlayerPrefs.GetInt("roundCount");
        getSavedDifficulties();
        updateDiffList();
        notNewGame = true;
        LoadScene();
    }

    //Variablen der ausgewählten Schwierigkeit anpassen
    private void updateToggle()
    {
        difficulty1 = diffOneToggle.isOn;
        difficulty2 = diffTwoToggle.isOn;
        difficulty3 = diffThreeToggle.isOn;
    }

    private void updateDiffList()
    {

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
            if(getNumberOfQuestions() < roundCount * teamCount || getNumberOfGuesses() < roundCount){
                text.GetComponent <TMP_Text> ().text = "Sie haben aktuell " + getNumberOfQuestions() + " normale Fragen und " + getNumberOfGuesses() + " Schätzfragen eingepflegt. Für ein Spiel mit den aktuellen Einstellungen sollten midenstens " + roundCount * teamCount + " normale Fragen und " + roundCount + " Schätzfragen eingepflegt sein.";
                textUI.SetActive(true);
                backgroundUI.SetActive(false);
            }else{
                LoadScene();
            } 
        } 
        else
        {
            difficulties.Clear();
        }
    }

    public void BackButton(){
        textUI.SetActive(false);
        backgroundUI.SetActive(true);
    }

    private void LoadScene(){
        if (roundCount < 5){
            SceneManager.LoadScene("QuizPartyMinimal");
        } else if (roundCount > 4 && roundCount < 9){
                SceneManager.LoadScene("QuizParty");
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

    public static bool getNotNewGame(){
        return notNewGame;
    }

    public static void saveDifficulties(){
        if (difficulty1 == true)
        {
            PlayerPrefs.SetInt("diff1", 1);
        }else{
            PlayerPrefs.SetInt("diff1", 0);
        }
        if (difficulty2 == true)
        {
            PlayerPrefs.SetInt("diff2", 1);
        }else{
            PlayerPrefs.SetInt("diff2", 0);
        }
        if (difficulty3 == true)
        {
            PlayerPrefs.SetInt("diff3", 1);
        }else{
            PlayerPrefs.SetInt("diff3", 0);
        }
    }

    private void getSavedDifficulties(){
        if(PlayerPrefs.GetInt("diff1") == 1){
            difficulty1 = true;
        }
        if(PlayerPrefs.GetInt("diff2") == 1){
            difficulty2 = true;
        }
        if(PlayerPrefs.GetInt("diff3") == 1){
            difficulty3 = true;
        }
    }

    private int getNumberOfQuestions(){
        int number = 0;
        List<GameEventField> fragenKatalog = FileHandler.ReadListFromJSON<GameEventField> ("GameFieldQuestions.json");
        foreach (GameEventField question in fragenKatalog){
            if (difficulties.Contains(question.GetDifficulty()) && question.GetFieldType() != 4){
                number += 1;
            }
        }
        return number;
    }

    private int getNumberOfGuesses(){
        int number = 0;
        List<GameEventField> fragenKatalog = FileHandler.ReadListFromJSON<GameEventField> ("GameFieldQuestions.json");
        foreach (GameEventField question in fragenKatalog){
            if (difficulties.Contains(question.GetDifficulty()) && question.GetFieldType() == 4){
                number += 1;
            }
        }
        return number;
    }

}
