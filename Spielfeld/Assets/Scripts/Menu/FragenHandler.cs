using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FragenHandler : MonoBehaviour
{

    void Start()
    {
        LoadList();
    }

    public void LoadList(){
        GameObject fragenTemplate = transform.GetChild (0).gameObject;
        GameObject g;

        List<GameEventField> fragenKatalog = FileHandler.ReadListFromJSON<GameEventField> ("testjson.json");
 
        foreach (GameEventField field in fragenKatalog) {
            if (field.GetFieldType() == 2){
                g = Instantiate (fragenTemplate, transform);
                g.transform.GetChild (0).GetComponent <TMP_Text> ().text = field.GetQuestion();
                g.transform.GetChild (1).GetComponent <TMP_Text> ().text = field.GetAnswer();
            } 
        }

        Destroy (fragenTemplate);  
    }

    public void RenewList(){
        for (int i = 1; i < transform.childCount; i++){
            Destroy (transform.GetChild (i).gameObject);
        }
        LoadList();
    }
}
