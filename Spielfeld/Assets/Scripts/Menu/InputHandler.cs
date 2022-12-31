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

    public void AddFieldToList () {
        entries = FileHandler.ReadListFromJSON<GameEventField> (filename);
        entries.Add (new GameEventField (2, question.text, answer.text, 60, 0));
        question.text = "";
        answer.text = "";
        FileHandler.SaveToJSON<GameEventField> (entries, filename);
    }
}