using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Estimation_Check : MonoBehaviour
{
    public GameObject estimationPopup;
    public GameObject inputFieldSample;

    private List<Team> teamList = new List<Team>();
    private List<GameObject> InputFields = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeEstimationPopup()
    {
        if(InputFields.Count < 1 && FindObjectOfType<TestTeams>().GetTeamList().Count > 0) {

            Debug.Log("-------- Start Initialization Estimation-Popup --------");
            teamList = FindObjectOfType<TestTeams>().GetTeamList();
            GameObject newInputfield;
            GameObject newInputTextArea;
            GameObject newInputDefaultText;

            int i = 1;
            int yPosInputField = -235;

            foreach (Team thisTeam in teamList){
                newInputfield = Instantiate(inputFieldSample, new Vector3(400 , yPosInputField , 0), Quaternion.identity);
                newInputfield.transform.SetParent(estimationPopup.transform, false);

                //Set Inputfield Symbol to Teamcolor
                newInputfield.transform.GetChild(0).GetComponent<Image>().color = thisTeam.GetColor();

                newInputTextArea = newInputfield.transform.GetChild(1).gameObject;
                newInputDefaultText= newInputTextArea.transform.GetChild(1).gameObject;
                newInputDefaultText.GetComponent<TextMeshProUGUI>().text = "Antwort Team " + i;

                InputFields.Add(newInputfield);
                yPosInputField -= 110;
                i++;
            }

        }
        
    }

    public void Validate(){
        //Todo: make sure that every Team gave an Answer
    }
}
