using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountdownUiManager : MonoBehaviour
{
    private Timer timer;

    public GameObject timerStartButton;
    public GameObject timerPauseButton;
    public GameObject timerContinueButton;
    public GameObject showAnswerButton;
    public GameObject rightAnswerButton;
    public GameObject wrongAnswerButton;
    public GameObject pictureTask;

    public Image pictureContent;

    private int eventFieldTime;

    // Start is called before the first frame update
    void Start()
    {     
        timer = FindObjectOfType<Timer>();
    }

    void Update(){
        //Pruefen ob noch zeit vhd. 
        //falls abgelaufen: Moderator bekommt meldung und muss richtig/falsch angeben
        if (timer.GetRemainingSeconds() < 1){
            PauseTimer();
            ShowAnswer();
        }

        if(FindObjectOfType<QuestionManager>().IsPictureField() && timer.isRunning()){
            pictureContent.fillAmount += 1.0f / (float)eventFieldTime * Time.deltaTime;
        }
    }

    public void StartTimer(){
        timer.StartTimer();
        timerStartButton.SetActive(false);
        timerPauseButton.SetActive(true);

        if (FindObjectOfType<QuestionManager>().IsPictureField()){
            timerPauseButton.transform.Translate(0,-365,0);
            pictureTask.SetActive(true);
            pictureContent.sprite = FindObjectOfType<QuestionManager>().LoadPictureFromDisk();
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

        if (FindObjectOfType<QuestionManager>().IsPictureField()){
            pictureTask.SetActive(false);
        }
    }

    public void ContinueTimer(){
        timerPauseButton.SetActive(true);
        timerContinueButton.SetActive(false);
        showAnswerButton.SetActive(false);
        timer.ContinueTimer();

        if (FindObjectOfType<QuestionManager>().IsPictureField()){
            pictureTask.SetActive(true);
        }
    }

    public void ShowAnswer(){
        timerContinueButton.SetActive(false);
        showAnswerButton.SetActive(false);
        rightAnswerButton.SetActive(true);
        wrongAnswerButton.SetActive(true);

        FindObjectOfType<QuestionManager>().ShowAnswer();

        if (FindObjectOfType<QuestionManager>().IsPictureField()){
            timerPauseButton.transform.Translate(0,365,0);
        }
    }

    public int GetTimePointsAndReset(){
        double t = timer.GetRemainingSeconds() / eventFieldTime;

        timer.StopTimer();
        rightAnswerButton.SetActive(false);
        wrongAnswerButton.SetActive(false);
        timerStartButton.SetActive(true);

    //Todo: Punktelogik ueberarbeiten
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

}
