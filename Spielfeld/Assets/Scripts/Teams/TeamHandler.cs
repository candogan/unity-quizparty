using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamHandler : MonoBehaviour
{


    public GameObject characterPrefab;
    public GameObject characterArea;

    public Material materialRed;
    public Material materialBlue;
    public Material materialYellow;
    public Material materialGreen;

    public static float teamCount;

    private List<Team> teamlist = new List<Team>();

    private List<Material> characterMaterials = new List<Material>();

    private List<Color> colorlist = new List<Color>{                    // Colors for the Teams
        Color.red,
        Color.blue,
        Color.yellow,
        Color.green
    };
    // Start is called before the first frame update
    private List<Vector3> initCharacterPositions = new List<Vector3>{   // Startpositionen der Charactere
        new Vector3(66f,150f,-142.75f),
        new Vector3(66f,150f,-140.75f),
        new Vector3(68f,150f,-142.75f),
        new Vector3(68f,150f,-140.75f)
    };


    void Start()
    {
        teamCount = GameOptionsHandler.getTeamCount();
        InitializeTeams();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public List<Team> GetTeamList(){
        return teamlist;
    }

    public void addOrTakePointsToScore(int teamIndex, int pointsToAdd){
        teamlist[teamIndex].addOrTakePointsToScore(pointsToAdd);
    }

    public GameObject getCharacterOfTeamindex(int teamIndex){
        return teamlist[teamIndex].GetCharacter();
    }

    private void InitializeTeams(){
        //ToDo: Charactere in Laufrichtung drehen

        characterMaterials.Add(materialRed);
        characterMaterials.Add(materialBlue);
        characterMaterials.Add(materialYellow);
        characterMaterials.Add(materialGreen);


        for (int i = 0; i < teamCount; i++){
            GameObject newCharacter = Instantiate(characterPrefab, initCharacterPositions[i], Quaternion.identity);
            newCharacter.transform.SetParent(characterArea.transform, false);

            newCharacter.transform.GetChild (3).GetComponent<Renderer>().material = characterMaterials[i];

            Team newTeam = new Team(colorlist[i], newCharacter);
            teamlist.Add(newTeam);
        }
    }
}
