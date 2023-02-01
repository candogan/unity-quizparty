using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestionManager : MonoBehaviour
{
    public TextMeshProUGUI questionTextField;

    private JsonGameEventFieldList eventFieldList;
    private Random rnd = new Random();
    private GameObject timerPauseButton;

    private JsonGameEventField currentEventField;

    private static int verfuegbar = 0;
    private static int nichtVerfuegbar = 1;

    private static int interaktionsfeld = 1;
    private static int wissensfeld = 2;
    private static int bildraten = 3;
    private static int schaetzfrage = 4;
    private static int actionfeld = 5;

    private static int actualEventFieldIndex;
    private static int actualFieldType;
    private static int actualTeamIndex;
    private static int allTeams = 444;

    void Start(){
        timerPauseButton = GameObject.Find("TimerPauseButton");
    }

    void Update(){
    }

    public int GetActualTeamIndex(){
        return actualTeamIndex;
    }

    public void setFieldtypeInteraction(int teamIndex){
        actualFieldType = interaktionsfeld;
        actualTeamIndex = teamIndex;
        questionTextField.text = "TEAM X: Macht euch bereit für eine Interaktionsaufgabe!";
        RandomQuestionPicker(actualFieldType);
    }
    
    public void setFieldtypeKnowledge(int teamIndex){
        actualFieldType = wissensfeld;
        questionTextField.text = "TEAM X: Macht euch bereit für eine Wissensaufgabe!";
        RandomQuestionPicker(actualFieldType);
    }
    
    public void setFieldtypePicture(int teamIndex){
        actualFieldType = bildraten;
        actualTeamIndex = teamIndex;
        questionTextField.text = "TEAM X: Macht euch bereit für ein Bildrätsel!";
        RandomQuestionPicker(actualFieldType);
    }
    
    public void setFieldtypeEstimation(){
        actualFieldType = schaetzfrage;
        actualTeamIndex = allTeams;
        questionTextField.text = "Alle Teams: Macht euch bereit für ein Schätzrätsel!";
        RandomQuestionPicker(actualFieldType);
    }
    
    public void setFieldtypeAction(int teamIndex){
        actualFieldType = actionfeld;
        actualTeamIndex = teamIndex;
        questionTextField.text = "TEAM X: Eure Figur Rückt 5 Felder weiter!";
        RandomQuestionPicker(actualFieldType);
    }

    public void LoadField(){
        //Debug.Log("Lade Aufgabentyp: " + actualFieldType);
        questionTextField.text =  eventFieldList.jsonGameEventFieldList[actualEventFieldIndex].content;
    }

    /*
    Diese Methode prueft, welche Fragen im Spiel noch nicht dran kamen und bestimmt eine zufaellige Frage zu der gegebenen Kategorie
    @param: fieldtype = Fragekategorie
    */
    private void RandomQuestionPicker(int fieldType){
        eventFieldList = FindObjectOfType<JsonReader>().getEventFieldList();
        List<int> availableIndexes = new List<int>();
        int i = 0;

        //Speicherung der Indexe von verfuegbaren Feldern (mit gewuenschter Kategtorie) in der Liste availableIndexes
        foreach (JsonGameEventField gameEventField in eventFieldList.jsonGameEventFieldList){
            if (gameEventField.state == verfuegbar && gameEventField.type == fieldType) {
                availableIndexes.Add(i);
            }
            i++;
        }

        int fieldCount = availableIndexes.Count;

        if (fieldCount < 1){
            availableIndexes = ResetStateAndGetAvailableFields(fieldType);
            fieldCount = availableIndexes.Count;
        }

        actualEventFieldIndex = availableIndexes[rnd.Next(fieldCount)];
        //Debug.Log("index: " + actualEventFieldIndex + ", state: " + eventFieldList.jsonGameEventFieldList[actualEventFieldIndex].state + ", fieldcount: " + fieldCount + ", fieldtype: " +  fieldType);

        currentEventField = eventFieldList.jsonGameEventFieldList[actualEventFieldIndex];
        eventFieldList.jsonGameEventFieldList[actualEventFieldIndex].state = nichtVerfuegbar;
        
        FindObjectOfType<PanelUiManager>().SetupTimer(eventFieldList.jsonGameEventFieldList[actualEventFieldIndex].time);
    }




    /*
    Falls alle Fragen einer Kategorie bereits dran kamen, werden mit dieser Methode alle Fragen wieder auf unbenutzt gesetzt.
    @return: neue Liste der moeglichen Eventfelder fuer den Random Question Picker
    */
    private List<int> ResetStateAndGetAvailableFields(int fieldType){
            int i = 0;
            List<int> availableIndexes = new List<int>();

            //Debug.Log("--------reset fieldtype: " + fieldType + "--------");

            foreach (JsonGameEventField gameEventField in eventFieldList.jsonGameEventFieldList){
                if (gameEventField.type == fieldType) {
                    eventFieldList.jsonGameEventFieldList[i].state = verfuegbar;
                    availableIndexes.Add(i);
                    //Debug.Log("i:" + i + ", state: "+ gameEventField.state);
                }
                i++;
            }
        return availableIndexes;
    }

    public void ShowCorrectAnswer(){
        questionTextField.text = "Lösung: " + currentEventField.solution;
    }

    public string GetAnswer(){
        return currentEventField.solution;
    }

    public void EmptyQuestiontext(){
        questionTextField.text = "";
    }

    public void DistributePoints(List<int> winnerTeams, int pointsToAdd){
        foreach (int winner in winnerTeams){
            //Debug.Log("Punkte Team " + winner +" vor korrekter Antwort: " + FindObjectOfType<TestTeams>().GetTeamList()[winner].GetScore());
            FindObjectOfType<TestTeams>().addOrTakePointsToScore(winner, pointsToAdd);
            //Debug.Log("Punkte Team " + winner +" nach korrekter Antwort: " + FindObjectOfType<TestTeams>().GetTeamList()[winner].GetScore());
        }
    }

    public bool IsPictureField(){
        return (currentEventField != default(JsonGameEventField) && currentEventField.type == bildraten);
    }

    public bool IsEstimationField(){
        return (currentEventField != default(JsonGameEventField) && currentEventField.type == schaetzfrage);
    }

    public Sprite LoadPictureFromDisk(){
        string filePath = Application.dataPath + "/Resources/" + currentEventField.file;
        Debug.Log("Lade: " + filePath);
        Sprite newImage = IMG2Sprite.instance.LoadNewSprite(filePath);
        return newImage;
    }
}