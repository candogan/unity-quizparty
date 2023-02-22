using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public GameObject diceCamera;
    public GameObject mainCamera;
    public GameObject sideCamera;
    private GameObject playerCamera;


    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FocusDiceCamera()
    {
        mainCamera.SetActive(false);
        sideCamera.SetActive(false);
        playerCamera.SetActive(false);
        diceCamera.SetActive(true);
    }

    public void FocusMainCamera()
    {
        sideCamera.SetActive(false);
        diceCamera.SetActive(false);
        playerCamera.SetActive(false);
        mainCamera.SetActive(true);
    }

    public void FocusSideCamera()
    {
        //Debug.Log("Inside");
        diceCamera.SetActive(false);
        mainCamera.SetActive(false);
        playerCamera.SetActive(false);
        sideCamera.SetActive(true);
    }

    public void FocusPlayerCamera()
    {
        diceCamera.SetActive(false);
        mainCamera.SetActive(false);
        sideCamera.SetActive(false);
        playerCamera.SetActive(true);

    }
        //activeTeam.transform.GetChild (5).gameObject.SetActive(false);

    public void setPlayerCamera(Character activeTeam){
        playerCamera = activeTeam.transform.GetChild (5).gameObject;   
    }
}