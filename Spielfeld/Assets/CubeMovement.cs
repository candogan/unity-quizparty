using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Debug.Log("Hello World!");
    }

    // Update is called once per frame
    void Update()
    {
        // Lokale Achse, keine Worldspace Achse
        Vector3 v3 = new Vector3(0, 0, 5) * Time.deltaTime;
        transform.Translate(v3);
    }
}
