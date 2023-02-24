using System.Collections;
using System.Collections.Generic;
using Random = System.Random;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static GameFieldTypeEnum;

public class QuestionManager : MonoBehaviour
{
    public GamePlayHandler gameplayHandler;
    public TextMeshProUGUI questionTextField;
    public GameObject timerPauseButton;
    public PanelUiManager panelUiManager;
    public TeamHandler teamHandler;

    private List<GameEventField> eventFieldList;
    private List<int> availableFieldTypes = new List<int>(); 
    private List<int> difficulty;
    private bool basicMode = false;
    private Random rnd = new Random();

    private GameEventField currentEventField;

    private static int verfuegbar = 0;
    private static int nichtVerfuegbar = 1;

    private static int actualEventFieldIndex;
    private static int actualFieldType;
    private static int actualTeamIndex;

    private static string filename = "GameFieldQuestions.json";

    private static List<string> teaserTextList = new List<string>{
        "TEAM {0}: Macht euch bereit für eine Interaktionsaufgabe!",
        "TEAM {0}: Macht euch bereit für eine Wissensaufgabe!",
        "TEAM {0}: Macht euch bereit für ein Bildrätsel!",
        "Alle Teams: Runde {0} ist vorbei. Macht euch bereit für ein Schätzrätsel!",
        "TEAM {0}: Eure Figur Rückt 5 Felder weiter!"
        };

    void Start(){
        difficulty = GameOptionsHandler.getDifficulties();
        eventFieldList = FileHandler.ReadListFromJSON<GameEventField> (filename);
        SearchAvailableTypes();
    }

    public int GetActualTeamIndex(){
        return actualTeamIndex;
    }

    public void StartNewQuestion(int teamIndex, int fieldtype){
        actualTeamIndex = teamIndex;

        actualFieldType = fieldtype;
        if(fieldtype != GameFieldTypeEnum.GUESSQUESTION){
            questionTextField.text = string.Format(teaserTextList[actualFieldType-1], actualTeamIndex + 1);
        } else {
            questionTextField.text = string.Format(teaserTextList[actualFieldType-1], gameplayHandler.GetActualRound());
        }
        RandomQuestionPicker(actualFieldType);

        panelUiManager.SetupTimer(eventFieldList[actualEventFieldIndex].GetTime());
        panelUiManager.ShowQuestionUi();
    }


    public void LoadQuestionText(){
        //Debug.Log("Lade Aufgabentyp: " + actualFieldType);
        if (IsPictureField()){
            questionTextField.text = "Wer / Was ist auf dem Bild abgebildet?";
        } else {

            questionTextField.text = eventFieldList[actualEventFieldIndex].GetContent();
        }
    }


    private void SearchAvailableTypes(){
        foreach(GameEventField gameEventField in eventFieldList){
            if(!availableFieldTypes.Contains(gameEventField.GetFieldType())){
                availableFieldTypes.Add(gameEventField.GetFieldType());
            }
        }
    }


    /*
    Diese Methode prueft, welche Fragen im Spiel noch nicht dran kamen und bestimmt eine zufaellige Frage zu der gegebenen Kategorie
    @param: fieldtype = Fragekategorie
    */
    private void RandomQuestionPicker(int fieldType){
        List<int> availableFields = GetAvailbleFields(fieldType);

        if (availableFields.Count > 0){
            int fieldCount = availableFields.Count;

            actualEventFieldIndex = availableFields[rnd.Next(fieldCount)];

            currentEventField = eventFieldList[actualEventFieldIndex];
            eventFieldList[actualEventFieldIndex].SetUsed(nichtVerfuegbar);
        }
    }


    /*
    Gibt alle verfügbaren Eventfelder für den fieldtype zurück und beinhaltet Auswahllogik, falls zuwenige Fragen eingepflegt wurden 
    */
    private List<int> GetAvailbleFields(int fieldtype){

        if(!availableFieldTypes.Contains(fieldtype)){
            Debug.Log("WARNING: NOT ENOUGH QUESTIONS. PICKED RANDOM QUESTIONTYPE");
            fieldtype = availableFieldTypes[rnd.Next(availableFieldTypes.Count)];
            StartNewQuestion(actualTeamIndex, fieldtype);
        }

        List<int> thisAvailableFields = new List<int>();
        int i = 0;

        foreach(GameEventField gameEventField in eventFieldList){
            if(gameEventField.GetUsed() == verfuegbar && gameEventField.GetFieldType() == fieldtype && difficulty.Contains(gameEventField.GetDifficulty())){
                thisAvailableFields.Add(i);
            }
            i++;
        }

        if (!basicMode && thisAvailableFields.Count < 1){
            //Falls nicht genug Fragen zur Schwierigkeitstufe vhd wird der schwierigkeitsfilter aufgehoben => Basic Mode des Spiels
            Debug.Log("WARNING: NOT ENOUGH QUESTIONS. SWITCHED TO BASICMODE");

            difficulty = new List<int>{1,2,3};
            basicMode = true;
            return GetAvailbleFields(fieldtype);

        }else if (basicMode && thisAvailableFields.Count < 1){
            //Letzter Ausweg falls zu wenig Fragen da sind: Wiederholung von Fragen
            Debug.Log("WARNING: NOT ENOUGH QUESTIONS. REPEATING QUESTIONS...");

            return ResetStateAndGetAvailableFields(fieldtype);
        }

        return thisAvailableFields;
    }

        /*
    Falls alle Fragen einer Kategorie bereits dran kamen, werden mit dieser Methode alle Fragen wieder auf unbenutzt gesetzt.
    @return: neue Liste der moeglichen Eventfelder fuer den Random Question Picker
    */
    private List<int> ResetStateAndGetAvailableFields(int fieldType){
            int i = 0;
            List<int> availableIndexes = new List<int>();

            //Debug.Log("--------reset fieldtype: " + fieldType + "--------");

            foreach (GameEventField gameEventField in eventFieldList){
                if (gameEventField.GetFieldType() == fieldType) {
                    eventFieldList[i].SetUsed(verfuegbar);
                    availableIndexes.Add(i);
                    //Debug.Log("i:" + i + ", state: "+ gameEventField.state);
                }
                i++;
            }
        return availableIndexes;
    }


    public void ShowCorrectAnswer(){
        if (currentEventField.GetFieldType() == GameFieldTypeEnum.KNOWLEDGE || currentEventField.GetFieldType() == GameFieldTypeEnum.GUESSQUESTION || currentEventField.GetFieldType() == GameFieldTypeEnum.GUESSPICTURE){
            questionTextField.text = "Lösung: " + currentEventField.GetAnswer();
        } else if (currentEventField.GetFieldType() == GameFieldTypeEnum.INTERACTION){
            questionTextField.text = "Hat das Team die Aufgabe gemeistert?";
        }
    }

    public string GetActualAnswer(){
        return currentEventField.GetAnswer();
    }

    public void EmptyQuestiontext(){
        questionTextField.text = "";
    }

    public void DistributePoints(List<int> winnerTeams, int pointsToAdd){
        foreach (int winner in winnerTeams){
            Debug.Log("Winner index: " + winner);
            //Debug.Log("Punkte Team " + winner +" vor korrekter Antwort: " + teamHandler.GetTeamList()[winner].GetScore());
            teamHandler.addOrTakePointsToScore(winner, pointsToAdd);
            //Debug.Log("Punkte Team " + winner +" nach korrekter Antwort: " + teamHandler.GetTeamList()[winner].GetScore());
        }
    }

    public bool IsPictureField(){
        return (currentEventField != default(GameEventField) && currentEventField.GetFieldType() == GameFieldTypeEnum.GUESSPICTURE);
    }

    public bool IsEstimationField(){
        return (currentEventField != default(GameEventField) && currentEventField.GetFieldType() == GameFieldTypeEnum.GUESSQUESTION);
    }

    public Sprite LoadPictureFromDisk(){
        string filePath = Application.dataPath + "/Resources/" + currentEventField.GetContent();
        //Debug.Log("Lade: " + filePath);
        Sprite newImage = IMG2Sprite.instance.LoadNewSprite(filePath);
        return newImage;
    }
}