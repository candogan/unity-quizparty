using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AwardCeremonyHandler : MonoBehaviour
{   
    public CameraManager cameraManager;
    public TeamHandler teamHandler;
    public GameObject awardCeremonyCanvas;
    public GameObject pauseButton;
    public List<GameObject> awardSteps = new List<GameObject>();

    List<Team> teamList;

    private List<Vector3> awardPositions = new List<Vector3>{
        new Vector3(-36.89f,2.85f,63.35f),
        new Vector3(-36.24f,2.85f,62.16f),
        new Vector3(-35.64f,2.85f,60.85f),
        new Vector3(-35.27f,2.85f,59.62f)
    };

    private List<Vector3> awardRotations = new List<Vector3>{
        new Vector3(0f,-125.383f,0f),
        new Vector3(0f,-114.777f,0f),
        new Vector3(0f,-106.76f,0f),
        new Vector3(0f,-86.782f,0f)
    };

    private float maxStepSize = 0.8f; // Max Additional Height of Award Stairs
    private int roundCound;
    private int maxPoints;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IntizializeAwardCeremony(){
        teamList = teamHandler.GetTeamList();

        pauseButton.SetActive(false);
        awardCeremonyCanvas.SetActive(true);

        roundCound = (int) GameOptionsHandler.getRoundCount();
        maxPoints = roundCound * 4;

        for(int i = 0; i < teamList.Count ; i +=1 ){
            teamList[i].GetCharacter().transform.position = awardPositions[i];
            teamList[i].GetCharacter().transform.eulerAngles  = awardRotations[i];

            awardSteps[i].transform.position += CalculateStepHeight(teamList[i]);
            teamList[i].GetCharacter().transform.position += CalculateStepHeight(teamList[i]);
        }

        cameraManager.FocusAwardCeremonyCamera();
    }


    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }


    private Vector3 CalculateStepHeight(Team team){
        int score = team.GetScore();
        double relativeScore = (double)team.GetScore() / (double)( maxPoints + 2 );
        Debug.Log("Score " + score + "   ( maxPoints + 2 ) " + ( maxPoints + 2 ) +  "  relativeScore " + relativeScore);
        return new Vector3(0f, (float)( maxStepSize * relativeScore ) , 0f);
     }
}
