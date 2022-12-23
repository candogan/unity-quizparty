using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
 
// give Test Runner access to private variables and methods
[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("JsonReaderTest")]
 
[Serializable]
public class JsonGameEventField
{
    //these variables are case sensitive and must match the strings "firstName" and "lastName" in the JSON.
    public int id;
    public int type;
    public string content;
    public int time;
    public string solution;
    public int state;
}
 
[System.Serializable]
public class JsonGameEventFieldList
{
    // GameEventFieldList is case sensitive and must match the string "GameEventFieldList" in the JSON.
    public JsonGameEventField[] jsonGameEventFieldList;
}
 
 
 
public class JsonReader : MonoBehaviour
{
    //=================== Set from Unity editor =======================
    // file to read gameEventFields from
    public TextAsset jsonFile;
    //file where the Data is saved
    [SerializeField] JsonGameEventFieldList eventFieldList;
 
    //=================== MonoBehavior interface =======================
    void Start()
    { 
        eventFieldList = LoadGameEventsFromFile();
    }
 
    //======================= public API =================================
 
 
    // create one instance of the TrialController for the app
    public static JsonReader jsonReader;
    public static JsonReader Instance()
    {
        if (!jsonReader)
        {
            jsonReader = FindObjectOfType(typeof(JsonReader)) as JsonReader;
 
            if (!jsonReader)
            {
                Debug.LogError("JsonReader inactive or missing from unity scene.");
            }
        }
 
        return jsonReader;
    }

    public JsonGameEventFieldList getEventFieldList(){
        return eventFieldList;
    }

    public void setState(int i, int newState){
        eventFieldList.jsonGameEventFieldList[i].state = newState;
    }
 
    //============= internal structures and methods ======================
 
    // Make result of json read available to test runner
     
    internal JsonGameEventFieldList LoadGameEventsFromFile()
    {
        Assert.IsNotNull(jsonFile);
 
        JsonGameEventFieldList testEventFieldList = JsonUtility.FromJson<JsonGameEventFieldList>(jsonFile.text);
 
        foreach (JsonGameEventField gameEventField in testEventFieldList.jsonGameEventFieldList)
        {
            Debug.Log("Found gameEventField: " + gameEventField.content);
        }
 
        return testEventFieldList;
    }
}