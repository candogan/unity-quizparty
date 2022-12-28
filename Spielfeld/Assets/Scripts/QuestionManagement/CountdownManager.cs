using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using TMPro;

public class CountdownManager : MonoBehaviour
{
    private Timer timer;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Countdown Manager gestartet");
        
        
        timer = FindObjectOfType<Timer>();
        timer.textMeshProText.text = "huhu";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartTimer(){

    }

    public void setTime(int seconds){
        
    }

    public void pauseTimer(){

    }

    public void continueTimer(){

    }

    public void stopTimer(){

    }

    public void getTimePoints(){

    }

    private string convertSecondsToMinutes(int secs){
        TimeSpan t = TimeSpan.FromSeconds( secs );
        return t.ToString(@"mm\:ss");
    }


}
