using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamManager : MonoBehaviour
{
    private List<Team> Teamlist = new List<Team>();
    // Start is called before the first frame update

    public TeamManager(int teamCount){
        //ToDo:
        //for i=0; i++; i<teamCount:
        //  create new Team
        //  set Team Variables
        //  Teamlist.Add(newTeam)
    }

    void Start()
    {
        
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
