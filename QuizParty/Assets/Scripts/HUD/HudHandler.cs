using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

using static TeamHandler;

public class HudHandler : MonoBehaviour
{
    public GameObject teamHudPrefab;
    public GameObject hudCanvas;
    public GameObject pauseButton;
    public GameObject questionPanel;
    public GameObject diceActualTeam;
    public TeamHandler teamHandler;

    private List<GameObject> teamHudList = new List<GameObject>();
    private List<Vector3> initHudPositions = new List<Vector3>{   // Startpositionen der Charactere
        new Vector3(-760f,470f,0),
        new Vector3(-390f,470f,0),
        new Vector3(-20f,470f,0),
        new Vector3(350f,470f,0)
    };


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TransferTeamStatsToHud(){
        int i = 0;

        foreach(GameObject hudElement in teamHudList){
            hudElement.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = teamHandler.GetRankingOfTeam(i) + ". Platz: " + teamHandler.GetScoreOfTeamindex(i);

            i += 1;
        }
    }

    public void SetHudInQuestionMode(){
        foreach(GameObject hudElement in teamHudList){
            hudElement.SetActive(false);
        }

        pauseButton.SetActive(false);
    }

    public void SetHudInFieldMode(){
        foreach(GameObject hudElement in teamHudList){
            hudElement.SetActive(true);
        }

        pauseButton.SetActive(true);
    }

    public void ShowDiceActualTeam(int actualTeam){
        diceActualTeam.SetActive(true);
        diceActualTeam.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Team " + (actualTeam + 1) + " würfelt";
    }

    public void HideDiceActualTeam(){
        diceActualTeam.SetActive(false);
    }


    public void InitializeHudForTheTeams(List<Team> teamlist){
        int i = 0;

        foreach(Team thisTeam in teamlist){
            GameObject newHudTeamElement = Instantiate(teamHudPrefab, initHudPositions[i], Quaternion.identity);
            newHudTeamElement.transform.SetParent(hudCanvas.transform, false);

            newHudTeamElement.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Team: " + ( i + 1);
            newHudTeamElement.transform.GetChild(1).GetComponent<Image>().color = thisTeam.GetColor();
            newHudTeamElement.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "1. Platz: 0";

            teamHudList.Add(newHudTeamElement);
            i += 1; 
        }
    }
}
