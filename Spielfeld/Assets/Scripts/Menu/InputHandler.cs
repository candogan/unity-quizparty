using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class InputHandler : MonoBehaviour {
    [SerializeField] TMP_InputField question;
    [SerializeField] TMP_InputField answer;
    [SerializeField] TMP_InputField time;
    [SerializeField] string filename;

    private List<GameEventField> entries = new List<GameEventField> ();

    private void Start () {
        entries = FileHandler.ReadListFromJSON<GameEventField> (filename);
    }

    public void AddFieldToList () {
        entries.Add (new GameEventField (2, question.text, answer.text, Convert.ToInt32(time.text), 0));
        question.text = "";
        answer.text = "";
        time.text = "";
        FileHandler.SaveToJSON<GameEventField> (entries, filename);
    }
}