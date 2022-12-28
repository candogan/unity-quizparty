using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using TMPro;

public class QuestionIterator : MonoBehaviour
{
    private JsonGameEventFieldList eventFieldList;
    private TextMeshProUGUI textField;
    private Random rnd = new Random();

    private static int verfügbar = 0;
    private static int nichtVerfügbar = 1;

    private static int interaktionsfeld = 1;
    private static int wissensfeld = 2;
    private static int bildraten = 3;
    private static int schaetzfrage = 4;
    private static int actionfeld = 5;

    void Start(){
        textField = GameObject.Find("QuestionTextfield").GetComponent<TextMeshProUGUI>();
    }

    public void loadKnowledgeField(){
        randomQuestionPicker(wissensfeld);
    }

    public void loadActionField(){
        randomQuestionPicker(actionfeld);
    }

    public void loadPictureField(){
        randomQuestionPicker(bildraten);
    }

    public void loadEstimationField(){
        randomQuestionPicker(schaetzfrage);
    }

    public void loadInteractionField(){
        randomQuestionPicker(interaktionsfeld);
    }



    /*
    Diese Methode prüft, welche Fragen im Spiel noch nicht dran kamen und bestimmt eine zufällige Frage zu der gegebenen Kategorie
    @param: fieldtype = Fragekategorie
    */
    private void randomQuestionPicker(int fieldType){
        eventFieldList = FindObjectOfType<JsonReader>().getEventFieldList();
        List<int> availableIndexes = new List<int>();
        int i = 0;

        //Speicherung der Indexe von verfügbaren Feldern (mit gewünschter Kategtorie) in der Liste availableIndexes
        foreach (JsonGameEventField gameEventField in eventFieldList.jsonGameEventFieldList){
            if (gameEventField.state == verfügbar && gameEventField.type == fieldType) {
                availableIndexes.Add(i);
            }
            i++;
        }

        int fieldCount = availableIndexes.Count;

        if (fieldCount < 1){
            availableIndexes = resetStateAndGetAvailableFields(fieldType);
            fieldCount = availableIndexes.Count;
        }

        int randomQuestionIndex = availableIndexes[rnd.Next(fieldCount)];
        //Debug.Log("index: " + randomQuestionIndex + ", state: " + eventFieldList.jsonGameEventFieldList[randomQuestionIndex].state + ", fieldcount: " + fieldCount + ", fieldtype: " +  fieldType);

        eventFieldList.jsonGameEventFieldList[randomQuestionIndex].state = nichtVerfügbar;
        textField.text =  eventFieldList.jsonGameEventFieldList[randomQuestionIndex].content;
        
        //todo: Set Countdown Time
        //setCountdown(eventFieldList.jsonGameEventFieldList[randomQuestionIndex]);
    }




    /*
    Falls alle Fragen einer Kategorie bereits dran kamen, werden mit dieser Methode alle Fragen wieder auf unbenutzt gesetzt.
    @return: neue Liste der möglichen Eventfelder für den Random Question Picker
    */
    private List<int> resetStateAndGetAvailableFields(int fieldType){
            int i = 0;
            List<int> availableIndexes = new List<int>();

            //Debug.Log("--------reset fieldtype: " + fieldType + "--------");

            foreach (JsonGameEventField gameEventField in eventFieldList.jsonGameEventFieldList){
                if (gameEventField.type == fieldType) {
                    eventFieldList.jsonGameEventFieldList[i].state = verfügbar;
                    availableIndexes.Add(i);
                    //Debug.Log("i:" + i + ", state: "+ gameEventField.state);
                }
                i++;
            }
        return availableIndexes;
    }
}