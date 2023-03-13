using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class MainMenu : MonoBehaviour
{

    public GameObject playOldGameButton;

    void Start(){
        if (!File.Exists(Application.dataPath + "/Resources/saveGameQuestions.json")){
            
        playOldGameButton.GetComponent <Button>().interactable = false;
        }
    }

    public void QuitGame ()
    {
        Debug.Log ("QUIT");
        Application.Quit();
    }

}
