using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InputHandler : MonoBehaviour {
    [SerializeField] TMP_InputField question;
    [SerializeField] TMP_InputField answer;
    [SerializeField] TMP_Dropdown difficulty;
    [SerializeField] string filename;
    public GameObject addButton;

    private List<GameEventField> entries = new List<GameEventField> ();

    private void Start () {
        entries = FileHandler.ReadListFromJSON<GameEventField> (filename);
        addButton.GetComponent <Button>().interactable = false;
    }

    private void Update (){
        if (question.text == "" || answer.text == "" ){
            addButton.GetComponent <Button>().interactable = false;
        } else {
            addButton.GetComponent <Button>().interactable = true;
        }
    }

    public void AddFieldToList (int type) {
        entries = FileHandler.ReadListFromJSON<GameEventField> (filename);
        Debug.Log(difficulty.value);

        if (type == 1)
        {
            entries.Add (new GameEventField (type, question.text, "", Convert.ToInt32(answer.text), difficulty.value + 1, 0));
        }
        else{
            entries.Add (new GameEventField (type, question.text, answer.text, 60, difficulty.value + 1, 0));
        }
        question.text = "";
        answer.text = "";
        FileHandler.SaveToJSON<GameEventField> (entries, filename);
    }
}