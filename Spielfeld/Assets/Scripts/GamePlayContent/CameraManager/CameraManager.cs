using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    GameObject diceCamera;
    GameObject mainCamera;
    GameObject sideCamera;

    // Start is called before the first frame update
    void Start()
    {
        diceCamera = GameObject.Find("DiceCamera");
        mainCamera = GameObject.Find("MainCamera");
        sideCamera = GameObject.Find("SideCamera");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow)) {
            FocusMainCamera();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow)) {
            FocusDiceCamera();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            FocusSideCamera();
        }
    }

    void FocusDiceCamera()
    {
        mainCamera.SetActive(false);
        sideCamera.SetActive(false);
        diceCamera.SetActive(true);
    }

    void FocusMainCamera()
    {
        sideCamera.SetActive(false);
        diceCamera.SetActive(false);
        mainCamera.SetActive(true);
    }

    void FocusSideCamera()
    {
        diceCamera.SetActive(false);
        mainCamera.SetActive(false);
        sideCamera.SetActive(true);
    }
}
