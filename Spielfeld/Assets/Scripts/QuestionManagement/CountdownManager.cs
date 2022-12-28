using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    private Timer timer;

    public GameObject timerStartButton;
    public GameObject timerPauseButton;
    public GameObject timerContinueButton;
    public GameObject showAnswerButton;

    private int eventFieldTime;

    // Start is called before the first frame update
    void Start()
    {     
        timer = FindObjectOfType<Timer>();
    }

    void update(){
        //Pruefen ob noch zeit vhd. 
        //falls abgelaufen: Moderator bekommt meldung und muss richtig/falsch angeben
    }

    public void StartTimer(){
        timer.StartTimer();
        timerStartButton.SetActive(false);
        timerPauseButton.SetActive(true);
    }

    public void SetupTimer(int secs){
        eventFieldTime = secs;
        TimeSpan t = TimeSpan.FromSeconds(secs);
        timer.textMeshProText.text = ConvertToDisplayFormat(t);
        timer.minutes = t.Minutes;
        timer.seconds = t.Seconds;
    }

    public void PauseTimer(){
        timerPauseButton.SetActive(false);
        timerContinueButton.SetActive(true);
        showAnswerButton.SetActive(true);
        timer.PauseTimer();

    }

    public void ContinueTimer(){
        timerPauseButton.SetActive(true);
        timerContinueButton.SetActive(false);
        timer.ContinueTimer();
    }

    public void ShowAnswer(){
        timerContinueButton.SetActive(false);
        showAnswerButton.SetActive(false);
        //Debug.Log("verbleibend: " + timer.GetRemainingSeconds + ", Fragezeit: " + eventFieldTime);
    }

    public void GetTimePoints(){

    }

    private string ConvertToDisplayFormat(TimeSpan t){
        return t.ToString(@"mm\:ss");
    }


}
