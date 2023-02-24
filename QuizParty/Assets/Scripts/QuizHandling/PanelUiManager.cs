using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static HudHandler;

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
    public GameObject teamPointsFieldSample;
    public GameObject winnerPointsFieldSample;
    public TextMeshProUGUI quizText;
    public QuestionManager questionManager;
    public GamePlayHandler gameplayHandler;
    public GameObject notificationGameObject;
    public HudHandler hudHandler;

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
        hudHandler.SetHudInQuestionMode();
    }

    public void ResetQuestionUi(){
        questionUi.SetActive(false);
        hudHandler.SetHudInFieldMode();
        hudHandler.TransferTeamStatsToHud();

        Destroy(winnersPopup);
        estimationPopup.SetActive(false);
        pictureTask.SetActive(false);
        wrongAnswerButton.SetActive(false);
        rightAnswerButton.SetActive(false);
        showAnswerButton.SetActive(false);
        timerContinueButton.SetActive(false);
        timerPauseButton.SetActive(false);
        timerStartButton.SetActive(true);
        notificationGameObject.SetActive(false);
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
            timerPauseButton.transform.Translate(0,-395,0);
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
            timerPauseButton.transform.Translate(0,395,0);
            rightAnswerButton.transform.Translate(230,0,0);
            wrongAnswerButton.transform.Translate(230,0,0);
            pictureTask.SetActive(true);
            pictureContent.fillAmount = 1;
            pictureTask.transform.Translate(-320,-55,0);
        }
    }


    public void DisableEstimationPopup(){
        estimationPopup.SetActive(false);
        questionManager.ShowCorrectAnswer();
    }

    public void HandleCorrectAnswer(){
        int points = GetTimePoints();

        List<int> winnerTeams = new List<int>();
        int winner = questionManager.GetActualTeamIndex();

        rightAnswerButton.SetActive(false);
        wrongAnswerButton.SetActive(false);
        ResetAfterPictureField();

        winnerTeams.Add(winner);
        questionManager.DistributePoints(winnerTeams, points);
        ShowDistributedPoints(winnerTeams, points);
        ShowRoundState();

        timer.StopTimer();
    }

    public void HandleWrongAnswer(){
        rightAnswerButton.SetActive(false);
        wrongAnswerButton.SetActive(false);
        ResetAfterPictureField();

        ShowDistributedPoints(new List<int>(), 0);
        ShowRoundState();

        timer.StopTimer();
    }

    private void ResetAfterPictureField(){
        if (questionManager.IsPictureField()){
            rightAnswerButton.transform.Translate(-230,0,0);
            wrongAnswerButton.transform.Translate(-230,0,0);
            pictureTask.transform.Translate(320,55,0);
            pictureTask.SetActive(false);
        }
    }

    private int GetTimePoints(){
        double t = timer.GetRemainingSeconds() / eventFieldTime;

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

        int teamIndex = 0;


        foreach (Team team in teamList){

            questionDoneButton.transform.position += new Vector3(0, -42f, 0);

            popupSize.y += 85;
            popupRectTrans.sizeDelta = popupSize;
            int teamReadable = teamIndex + 1; 
            
            if (winnerTeams.Contains(teamIndex)){
                GameObject newWinnerField = Instantiate(winnerPointsFieldSample, new Vector3(250 , yPosInputField , 0), Quaternion.identity);
                newWinnerField.transform.SetParent(winnersPopup.transform, false);

                TextMeshProUGUI distributedPointsText = newWinnerField.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();

                newWinnerField.transform.GetChild(0).GetComponent<Image>().color = team.GetColor();

                newWinnerField.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Team " + teamReadable + ": "+ team.GetScore() + " Punkte";
                distributedPointsText.text = "+" + distributedPoints;
            } else {
                GameObject newPointsField = Instantiate(teamPointsFieldSample, new Vector3(250 , yPosInputField , 0), Quaternion.identity);
                newPointsField.transform.SetParent(winnersPopup.transform, false);

                newPointsField.transform.GetChild(0).GetComponent<Image>().color = team.GetColor();
                newPointsField.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Team " + teamReadable + ": "+ team.GetScore() + " Punkte";
            }

            yPosInputField -= 85;
            teamIndex += 1;
        }
    }

    public void ShowRoundState(){
        if (gameplayHandler.isLastMoveThisRound()){
            Debug.Log("Runde: " + gameplayHandler.GetActualRound());
            notificationGameObject.SetActive(true);
            notificationGameObject.GetComponent<TextMeshProUGUI>().text = "Runde " + (gameplayHandler.GetActualRound()) + " vorbei";
        }
    }
}
