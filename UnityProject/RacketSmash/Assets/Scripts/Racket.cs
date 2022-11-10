using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Racket : MonoBehaviourPun
{
    private GameObject rightController;

    private void Start()
    {
        rightController = GameObject.Find("RightHand Controller");
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine)
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
}
