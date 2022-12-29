using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using TMPro;

public class QuestionIterator : MonoBehaviour
{
    public TextMeshProUGUI textField;
    public TextMeshProUGUI showAnswerField;
    public GameObject showAnswerObject;

    private JsonGameEventFieldList eventFieldList;
    private Random rnd = new Random();

    private JsonGameEventField currentEventField;

    private static int verfuegbar = 0;
    private static int nichtVerfuegbar = 1;

    private static int interaktionsfeld = 1;
    private static int wissensfeld = 2;
    private static int bildraten = 3;
    private static int schaetzfrage = 4;
    private static int actionfeld = 5;

    void Start(){
    }

    void Update(){
    }

    public void LoadKnowledgeField(){
        RandomQuestionPicker(wissensfeld);
    }

    public void LoadActionField(){
        RandomQuestionPicker(actionfeld);
    }

    public void LoadPictureField(){
        RandomQuestionPicker(bildraten);
    }

    public void LoadEstimationField(){
        RandomQuestionPicker(schaetzfrage);
    }

    public void LoadInteractionField(){
        RandomQuestionPicker(interaktionsfeld);
    }



    /*
    Diese Methode prüft, welche Fragen im Spiel noch nicht dran kamen und bestimmt eine zufällige Frage zu der gegebenen Kategorie
    @param: fieldtype = Fragekategorie
    */
    private void RandomQuestionPicker(int fieldType){
        eventFieldList = FindObjectOfType<JsonReader>().getEventFieldList();
        List<int> availableIndexes = new List<int>();
        int i = 0;

        //Speicherung der Indexe von verfuegbaren Feldern (mit gewünschter Kategtorie) in der Liste availableIndexes
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

        int randomQuestionIndex = availableIndexes[rnd.Next(fieldCount)];
        //Debug.Log("index: " + randomQuestionIndex + ", state: " + eventFieldList.jsonGameEventFieldList[randomQuestionIndex].state + ", fieldcount: " + fieldCount + ", fieldtype: " +  fieldType);

        currentEventField = eventFieldList.jsonGameEventFieldList[randomQuestionIndex];
        eventFieldList.jsonGameEventFieldList[randomQuestionIndex].state = nichtVerfuegbar;
        textField.text =  eventFieldList.jsonGameEventFieldList[randomQuestionIndex].content;
        
        FindObjectOfType<CountdownManager>().SetupTimer(eventFieldList.jsonGameEventFieldList[randomQuestionIndex].time);
    }




    /*
    Falls alle Fragen einer Kategorie bereits dran kamen, werden mit dieser Methode alle Fragen wieder auf unbenutzt gesetzt.
    @return: neue Liste der möglichen Eventfelder für den Random Question Picker
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

    public void ShowAnswer(){
        showAnswerObject.SetActive(true);
        showAnswerField.text = "Lösung: " + currentEventField.solution;
    }
}