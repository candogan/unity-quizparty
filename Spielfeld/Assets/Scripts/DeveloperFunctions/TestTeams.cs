using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTeams : MonoBehaviour
{
    private List<Team> Teamlist = new List<Team>();
    // Start is called before the first frame update
    public GameObject emptyGameobject;

    void Start()
    {
        Team red = new Team(Color.red, emptyGameobject );
        Team blue = new Team(Color.blue, emptyGameobject);
        Team yellow = new Team(Color.yellow, emptyGameobject );
        Team green = new Team(Color.green, emptyGameobject);

        Teamlist.Add(red);
        Teamlist.Add(blue);
        Teamlist.Add(yellow);
        Teamlist.Add(green);

        FindObjectOfType<EstimationCheck>().InitializeEstimationPopup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Team> GetTeamList(){
        return Teamlist;
    }

    public void addOrTakePointsToScore(int teamIndex, int pointsToAdd){
        Teamlist[teamIndex].addOrTakePointsToScore(pointsToAdd);
    }
}