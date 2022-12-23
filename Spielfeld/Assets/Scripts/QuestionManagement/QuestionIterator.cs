using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestionIterator : MonoBehaviour
{
    private int i = 0;
    private JsonGameEventFieldList eventFieldList;
    private TextMeshProUGUI text;



    public void OnMouseDown(){
        text = GameObject.Find("Aufgabentext").GetComponent<TextMeshProUGUI>();
        text.text = "zzz";

        eventFieldList = FindObjectOfType<JsonReader>().getEventFieldList();
        
        text.text = eventFieldList.jsonGameEventFieldList[i].content;
        i = (i+1)%3;
    }
}