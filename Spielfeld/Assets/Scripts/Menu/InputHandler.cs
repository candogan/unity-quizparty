using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InputHandler : MonoBehaviour {
    [SerializeField] InputField type;
    [SerializeField] InputField question;
    [SerializeField] InputField answer;
    [SerializeField] InputField time;
    [SerializeField] string filename;

    private List<GameEventField> entries = new List<GameEventField> ();

    private void Start () {
        entries = FileHandler.ReadListFromJSON<GameEventField> (filename);
    }

    public void AddFieldToList () {
        entries.Add (new GameEventField (Convert.ToInt32(type.text), question.text, answer.text, Convert.ToInt32(time.text), 0));
        type.text = "";
        question.text = "";
        answer.text = "";
        time.text = "";
        FileHandler.SaveToJSON<GameEventField> (entries, filename);
    }
}