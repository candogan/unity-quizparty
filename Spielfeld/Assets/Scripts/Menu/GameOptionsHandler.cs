using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOptionsHandler : MonoBehaviour
{
    
    public static float teamCount;
    public static float roundCount;

    public static bool difficulty1;
    public static bool difficulty2;
    public static bool difficulty3;

    public Slider teamSlider;
    public Slider roundSlider;

    public Toggle diffOneToggle;
    public Toggle diffTwoToggle;
    public Toggle diffThreeToggle;

    // Start is called before the first frame update
    void Start()
    {
        teamCount = 2;
        roundCount = 3;
        difficulty1 = true;
        difficulty2 = true;
        difficulty3 = true;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PlayGame ()
    {
        difficulty1 = diffOneToggle.isOn;
        difficulty2 = diffTwoToggle.isOn;
        difficulty3 = diffThreeToggle.isOn;

        if (difficulty1 == true || difficulty2 == true || difficulty3 == true)
        {
            teamCount = teamSlider.value;
            roundCount  = roundSlider.value;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }
}
