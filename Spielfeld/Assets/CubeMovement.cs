using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMovement : MonoBehaviour
{

    float countdown = 20;
    float restartCountdown = 20;
    Vector3 startLocation = new Vector3(0,0,0);

    // Start is called before the first frame update
    void Start()
    {
        this.startLocation = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        // Lokale Achse, keine Worldspace Achse
        Vector3 movementVector = new Vector3(0, 0, 5) * Time.deltaTime;
        transform.Translate(movementVector);

        countdown -= Time.deltaTime;
        if (countdown <= 0) {
            transform.position = this.startLocation;
            
            Vector3 secondMovementVector = new Vector3(0, 0, 5) * Time.deltaTime;
            transform.Translate(secondMovementVector);

            countdown = restartCountdown;
        }
    }
}
