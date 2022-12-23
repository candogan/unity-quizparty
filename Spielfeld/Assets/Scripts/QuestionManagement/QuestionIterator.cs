using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using TMPro;

public class QuestionIterator : MonoBehaviour
{
    private JsonGameEventFieldList eventFieldList;
    private TextMeshProUGUI text;
    private Random rnd = new Random();

    //private static int interaktionsfeld = 1;
    private static int wissensfeld = 2;
    //private static int Bildraten = 3;
    //private static int schaetzfrage = 4;
    //private static int actionfeld = 5;

    void Start(){
        text = GameObject.Find("QuestionTextfield").GetComponent<TextMeshProUGUI>();
    }

    public void loadKnowledgeField(){
        eventFieldList = FindObjectOfType<JsonReader>().getEventFieldList();
        int i = randomQuestionPicker(wissensfeld);

        eventFieldList.jsonGameEventFieldList[i].state = 1;

        text.text =  eventFieldList.jsonGameEventFieldList[i].content;
    }

    /*
    Diese Methode prüft, welche Fragen im Spiel noch nicht dran kamen und bestimmt eine zufällige Frage zu der gegebenen Kategorie
    @param: fieldtype = Fragekategorie
    */
    private int randomQuestionPicker(int fieldType){
        List<int> availableIndexes = new List<int>();
        int i = 0;

        foreach (JsonGameEventField gameEventField in eventFieldList.jsonGameEventFieldList){
            if (gameEventField.state == 0 && gameEventField.type == fieldType) {
                //Debug.Log("i:" + i + ", state: "+ gameEventField.state);
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
        Debug.Log("index: " + randomQuestionIndex + ", state: " + eventFieldList.jsonGameEventFieldList[randomQuestionIndex].state + ", fieldcount: " + fieldCount);
        return randomQuestionIndex;
    }

    /*
    Falls alle Fragen einer Kategorie bereits dran kamen, werden mit dieser Methode alle Fragen wieder auf unbenutzt gesetzt.
    @return: neue Liste der möglichen Eventfelder für den Random Question Picker
    */
    private List<int> resetStateAndGetAvailableFields(int fieldType){
            int i = 0;
            List<int> availableIndexes = new List<int>();

            Debug.Log("reset");

            foreach (JsonGameEventField gameEventField in eventFieldList.jsonGameEventFieldList){
                if (gameEventField.type == fieldType) {
                    eventFieldList.jsonGameEventFieldList[i].state = 0;
                    availableIndexes.Add(i);
                    Debug.Log("i:" + i + ", state: "+ gameEventField.state);
                    i++;
                }
            }
        return availableIndexes;
    }
}