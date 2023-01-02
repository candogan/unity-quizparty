using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Vector3 initPosition;
    Vector3 currentLocation;

    // Start is called before the first frame update
    void Start()
    {
        initPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currentLocation = transform.position;
        if (currentLocation.x >= initPosition.x - 7) {
                Animator.SetTrigger();
                Vector3 destiny = new Vector3(0, 0, 2) * Time.deltaTime;
                transform.Translate(destiny);
        } else {
            Vector3 speed = new Vector3(0, 0, 0) * Time.deltaTime;
            transform.Translate(speed);
        }
    }
}
