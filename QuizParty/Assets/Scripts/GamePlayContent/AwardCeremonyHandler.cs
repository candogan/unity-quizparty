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


    List<Team> orderedTeamlist;

    private List<Vector3> awardPositions = new List<Vector3>{
        new Vector3(0f,4.38f,49f),
        new Vector3(1f,3.86f,49f),
        new Vector3(-1f,3.86f,49f),
        new Vector3(-2.45f,2.5f,49f)
    };

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IntizializeAwardCeremony(){
        CheckTeamRanking();

        pauseButton.SetActive(false);
        awardCeremonyCanvas.SetActive(true);

        for(int i = 0; i < orderedTeamlist.Count ; i +=1 ){
            orderedTeamlist[i].GetCharacter().transform.position = awardPositions[i];

            if (i != 3){
                orderedTeamlist[i].GetCharacter().transform.eulerAngles  = new Vector3(0f,0f,0f);
            } else {
                orderedTeamlist[i].GetCharacter().transform.eulerAngles  = new Vector3(0f,27f,0f);
            }
        }

        cameraManager.FocusAwardCeremonyCamera();
    }

    public void CheckTeamRanking(){
        orderedTeamlist = teamHandler.GetTeamList();
        orderedTeamlist.Sort(SortByScore);
    }

    public void LoadMainMenu(){
        SceneManager.LoadScene("MainMenu");
    }

    private int SortByScore(Team t1, Team t2){
        return t2.GetScore().CompareTo(t1.GetScore());
     }
}
