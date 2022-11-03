using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    public GameObject rightController;

    private void FixedUpdate()
    {
        if (transform.position != rightController.transform.position)
        {
            GetComponent<Rigidbody>().MovePosition(rightController.transform.position);
        }
        if (transform.eulerAngles != rightController.transform.eulerAngles)
        {
            GetComponent<Rigidbody>().MoveRotation(rightController.transform.rotation);
        }
    }
}
