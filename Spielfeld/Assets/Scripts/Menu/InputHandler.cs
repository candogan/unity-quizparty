using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InputHandler : MonoBehaviour {
    [SerializeField] TMP_InputField question;
    [SerializeField] TMP_InputField answer;
    [SerializeField] string filename;

    private List<GameEventField> entries = new List<GameEventField> ();

    private void Start () {
        entries = FileHandler.ReadListFromJSON<GameEventField> (filename);
    }

    public void AddFieldToList (int type) {
        entries = FileHandler.ReadListFromJSON<GameEventField> (filename);
        if (type == 1)
        {
            entries.Add (new GameEventField (type, question.text, "", Convert.ToInt32(answer.text), 0));
        }
        else{
            entries.Add (new GameEventField (type, question.text, answer.text, 60, 0));
        }
        question.text = "";
        answer.text = "";
        FileHandler.SaveToJSON<GameEventField> (entries, filename);
    }
}