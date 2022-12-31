using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.IO;

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

                g.transform.GetChild (2).GetComponent <Button>().onClick.AddListener (delegate() {
                    DeleteItem(field, fragenKatalog);
                });

                g.SetActive(true);
            } 
        }

        //Destroy (fragenTemplate);  
    }

    void DeleteItem(GameEventField field, List<GameEventField> fragenKatalog ){
        fragenKatalog.Remove(field);
        File.Delete(FileHandler.GetPath("testjson.json"));
        FileHandler.SaveToJSON<GameEventField> (fragenKatalog, "testjson.json");
        RenewList(); 
    }

    public void RenewList(){
        for (int i = 1; i < transform.childCount; i++){
            Destroy (transform.GetChild (i).gameObject);
        }
        LoadList();
    }

}
