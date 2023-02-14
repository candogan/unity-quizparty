using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PanelUiManager : MonoBehaviour
{
    public GameObject questionUi;
    public GameObject questionPanel;
    public GameObject timerStartButton;
    public GameObject timerPauseButton;
    public GameObject timerContinueButton;
    public GameObject showAnswerButton;
    public GameObject rightAnswerButton;
    public GameObject wrongAnswerButton;
    public GameObject pictureTask;
    public GameObject estimationPopup;
    public GameObject winnerPopupSample;
    public GameObject winnerFieldSample;
    public TextMeshProUGUI quizText;
    public QuestionManager questionManager;

    public Image pictureContent;

    public TeamHandler teamHandler;
    public Timer timer;

    private int eventFieldTime;
    private GameObject winnersPopup;
    private List<Team> teamList;

    // Start is called before the first frame update
    void Start()
    {
        teamList = teamHandler.GetTeamList();  
    }

    void Update(){
        //Pruefen ob noch zeit vhd. 
        //falls abgelaufen: Moderator bekommt meldung und muss richtig/falsch angeben
        if (timer.GetRemainingSeconds() < 1){
            timer.StopTimer();
            WaitForAnswer();
        }

        if(questionManager.IsPictureField() && timer.isRunning()){
            pictureContent.fillAmount += 1.1f / (float)eventFieldTime * Time.deltaTime;
        }
    }

    public void ShowQuestionUi(){
        questionUi.SetActive(true);
    }

    public void ResetQuestionUi(){
        questionUi.SetActive(false);

        Destroy(winnersPopup);
        estimationPopup.SetActive(false);
        pictureTask.SetActive(false);
        wrongAnswerButton.SetActive(false);
        rightAnswerButton.SetActive(false);
        showAnswerButton.SetActive(false);
        timerContinueButton.SetActive(false);
        timerPauseButton.SetActive(false);
        timerStartButton.SetActive(true);
        timer.StopTimer();
        timerPauseButton.transform.localPosition = new Vector3(0,0,0);
        quizText.text = "Lade neues Rätsel...";
    }

    public bool UiIsActive(){
        return questionUi.activeSelf;
    }

    public void StartTimer(){
        timer.StartTimer();
        timerStartButton.SetActive(false);
        timerPauseButton.SetActive(true);

        if (questionManager.IsPictureField()){
            timerPauseButton.transform.Translate(0,-365,0);
            pictureTask.SetActive(true);
            pictureContent.sprite = questionManager.LoadPictureFromDisk();
            pictureContent.fillAmount = 0;
        }
    }

    public void SetupTimer(int secs){
        eventFieldTime = secs;
        TimeSpan t = TimeSpan.FromSeconds(secs);
        timer.textMeshProText.text = t.ToString(@"mm\:ss");
        timer.minutes = t.Minutes;
        timer.seconds = t.Seconds;
    }

    public void PauseTimer(){
        timerPauseButton.SetActive(false);
        timerContinueButton.SetActive(true);
        showAnswerButton.SetActive(true);
        timer.PauseTimer();

        if (questionManager.IsPictureField()){
            pictureTask.SetActive(false);
        }
    }

    public void ContinueTimer(){
        timerPauseButton.SetActive(true);
        timerContinueButton.SetActive(false);
        showAnswerButton.SetActive(false);
        timer.ContinueTimer();

        if (questionManager.IsPictureField()){
            pictureTask.SetActive(true);
        }
    }

    public void WaitForAnswer(){
        timerContinueButton.SetActive(false);
        showAnswerButton.SetActive(false);
        
        if(questionManager.IsEstimationField()){
            estimationPopup.SetActive(true);
            questionManager.EmptyQuestiontext();
        } else {
            rightAnswerButton.SetActive(true);
            wrongAnswerButton.SetActive(true);
            questionManager.ShowCorrectAnswer();
        }

        //versetzen des Pause Buttons falls mehr Platz fuer ein Bildraetsel benoetigt wird
        if (questionManager.IsPictureField()){
            timerPauseButton.transform.Translate(0,365,0);
        }
    }

    public void DisableEstimationPopup(){
        estimationPopup.SetActive(false);
        questionManager.ShowCorrectAnswer();
    }

    public void HandleCorrectAnswer(){
        int points = GetTimePointsAndRemoveAnswerButtons();

        List<int> winnerTeams = new List<int>();
        int winner = questionManager.GetActualTeamIndex();

        winnerTeams.Add(winner-1);
        questionManager.DistributePoints(winnerTeams, points);
        ShowDistributedPoints(winnerTeams, points);

        timer.StopTimer();
    }

    public void HandleWrongAnswer(){

    }

    private int GetTimePointsAndRemoveAnswerButtons(){
        double t = timer.GetRemainingSeconds() / eventFieldTime;

        rightAnswerButton.SetActive(false);
        wrongAnswerButton.SetActive(false);

        if (t > 0.75){
            return 4;
        } else if (t > 0.5 && t < 0.75){
            return 3;
        } else if (t > 0.25 && t < 0.5){
            return 2;
        } else {
            return 1;
        }
        
    }

    public void ShowDistributedPoints(List<int> winnerTeams, int distributedPoints){
        int yPosInputField = -190;

        winnersPopup = Instantiate(winnerPopupSample, new Vector3(0 , 0 , 0), Quaternion.identity);
        winnersPopup.transform.SetParent(questionPanel.transform, false);
        RectTransform popupRectTrans = GameObject.Find("WinnersPopup(Clone)").GetComponent<RectTransform>();
        Vector2 popupSize = popupRectTrans.sizeDelta;

        GameObject questionDoneButton = winnersPopup.transform.GetChild(1).gameObject;
        questionDoneButton.GetComponent<Button>().onClick.AddListener(() => ResetQuestionUi());


        foreach (int winner in winnerTeams){
            questionDoneButton.transform.position += new Vector3(0, -42f, 0);

            popupSize.y += 85;
            popupRectTrans.sizeDelta = popupSize;
            int winnerReadable = winner + 1; 
            
            GameObject newWinnerField = Instantiate(winnerFieldSample, new Vector3(250 , yPosInputField , 0), Quaternion.identity);
            newWinnerField.transform.SetParent(winnersPopup.transform, false);

            newWinnerField.transform.GetChild(0).GetComponent<Image>().color = teamList[winner].GetColor();
            newWinnerField.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Team " + winnerReadable + ": +"+ distributedPoints + " Punkte";

            yPosInputField -= 85;
        }
    }

}
