using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class EstimationCheck : MonoBehaviour
{
    public GameObject estimationPopup;
    public GameObject inputFieldSample;
    public GameObject checkEstimationButton;

    public TeamHandler teamHandler;
    public QuestionManager questionManager;
    public PanelUiManager panelUiManager;

    private List<Team> teamList = new List<Team>();
    private List<GameObject> InputFields = new List<GameObject>();
    private GameObject winnersPopup;
    private int estimationFieldHeight = 95;

    Regex regexNumbers = new Regex(@"^\d+$");
    // Start is called before the first frame update
    void Start()
    {
        InitializeEstimationPopup();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitializeEstimationPopup()
    {
        teamList = teamHandler.GetTeamList();

        RectTransform popupRectTrans = estimationPopup.GetComponent<RectTransform>();
        Vector2 popupSize = popupRectTrans.sizeDelta;

        if(InputFields.Count < 1 && teamHandler.GetTeamList().Count > 0) {

            //Debug.Log("-------- Start Initialization Estimation-Popup --------");
            GameObject newInputfield;
            GameObject newInputTextArea;
            GameObject newInputDefaultText;

            int i = 1;
            int yPosInputField = -235;

            foreach (Team thisTeam in teamList){

                //Setup Pupup Size
                popupSize.y += estimationFieldHeight;
                popupRectTrans.sizeDelta = popupSize;
                checkEstimationButton.transform.position += new Vector3(0, -43.5f, 0); 

                //Instantiate Inputfield for each Team
                newInputfield = Instantiate(inputFieldSample, new Vector3(400 , yPosInputField , 0), Quaternion.identity);
                newInputfield.transform.SetParent(estimationPopup.transform, false);

                //Set Inputfield Symbol to Teamcolor + default Text
                newInputfield.transform.GetChild(1).GetComponent<Image>().color = thisTeam.GetColor();
                newInputTextArea = newInputfield.transform.GetChild(2).gameObject;
                newInputDefaultText= newInputTextArea.transform.GetChild(1).gameObject;
                newInputDefaultText.GetComponent<TextMeshProUGUI>().text = "Antwort Team " + i;

                InputFields.Add(newInputfield);
                yPosInputField -= 110;
                i++;
            }

        }
    }

    private void ResetInputFields(){
        foreach(GameObject inputField in InputFields){
            inputField.GetComponent<TMP_InputField>().text = "";
        }
    }


    public void CheckEstimations(){
        if(Validate()){
            Debug.Log("-------- Eingaben valide --------");
            List<int> winnerTeams = IdentifyEstimationWinner();
            ResetInputFields();


            panelUiManager.DisableEstimationPopup();
            questionManager.DistributePoints(winnerTeams, 2);
            panelUiManager.ShowDistributedPoints(winnerTeams, 2);
            //panelUiManager.ShowRoundState(); Deprecated
        }
    
    }

    private bool Validate(){
        bool isValid = true;
        
        foreach (GameObject thisInputfield in InputFields){
            GameObject thisErrorIcon = thisInputfield.transform.GetChild(3).gameObject;
            GameObject thisErrorBorder = thisInputfield.transform.GetChild(0).gameObject;

            string inputValue = thisInputfield.GetComponent<TMP_InputField>().text.Replace("\u200B", "");
                
            if(!int.TryParse(inputValue, out int result) || inputValue.Length < 1)
            {
                thisErrorIcon.SetActive(true);
                thisErrorBorder.SetActive(true);
                isValid = false;
            } else {
                thisErrorIcon.SetActive(false);
                thisErrorBorder.SetActive(false);
            }
        
        }
        return isValid;
    }


    private List<int> IdentifyEstimationWinner(){
        List<int> differencesToSolution = new List<int>();
        List<int> teamsWithRightAnswer = new List<int>();
        int.TryParse(questionManager.GetActualAnswer(), out int solution);
        int i = 0;

        foreach (GameObject thisInputfield in InputFields){
            string inputValue = thisInputfield.GetComponent<TMP_InputField>().text.Replace("\u200B", "");
            int.TryParse(inputValue, out int estimation);

            if(estimation == solution){
                teamsWithRightAnswer.Add(i);
            } else if (estimation < solution){
                differencesToSolution.Add(solution-estimation);
            } else if (estimation > solution){
                differencesToSolution.Add(estimation-solution);
            }
            i++;
        }

        if(teamsWithRightAnswer.Count > 0){
            return teamsWithRightAnswer;
        }

        return GetSmallestDifferencesToSolution(differencesToSolution);
    }


    private List<int> GetSmallestDifferencesToSolution(List<int> differencesToSolution){
        int i = 0;
        int smallestDiffernce = differencesToSolution.AsQueryable().Min();
        List<int> bestEstimations = new List<int>();

        foreach (int thisDifference in differencesToSolution){
            if (thisDifference == smallestDiffernce){
                bestEstimations.Add(i);
            }
            i++;
        }

        return bestEstimations;
    }
}
