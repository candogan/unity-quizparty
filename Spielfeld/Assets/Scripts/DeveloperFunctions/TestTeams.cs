using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTeams : MonoBehaviour
{
    private List<Team> Teamlist = new List<Team>();
    // Start is called before the first frame update

    void Start()
    {
        Team red = new Team(Color.red, 0, 1 );
        Team blue = new Team(Color.blue, 15, 1);
        Team yellow = new Team(Color.yellow, 5, 3 );
        Team green = new Team(Color.green, 20, 15);

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