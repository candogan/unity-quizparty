using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    public GameObject diceCamera;
    public GameObject mainCamera;
    public GameObject sideCamera;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartClass()
    {
        diceCamera = GameObject.Find("DiceCamera");
        mainCamera = GameObject.Find("MainCamera");
        sideCamera = GameObject.Find("SideCamera");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void FocusDiceCamera()
    {
        mainCamera.SetActive(false);
        sideCamera.SetActive(false);
        diceCamera.SetActive(true);
    }

    public void FocusMainCamera()
    {
        sideCamera.SetActive(false);
        diceCamera.SetActive(false);
        mainCamera.SetActive(true);
    }

    public void FocusSideCamera()
    {
        Debug.Log("Inside");
        diceCamera.SetActive(false);
        mainCamera.SetActive(false);
        sideCamera.SetActive(true);
    }
}