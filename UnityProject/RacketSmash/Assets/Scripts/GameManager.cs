using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPun
{
    private GameObject ball;
    private GameObject racket;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ball = PhotonNetwork.Instantiate("Ball", new Vector3(0f, 2f, 0f), Quaternion.identity);
        }
        else
        {
            ball = GameObject.Find("Ball");
        }

        racket = PhotonNetwork.Instantiate("Racket", new Vector3(0f, 2f, -3f), Quaternion.identity);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ball.transform.position = racket.transform.position + new Vector3(0f, 0f, 0.5f);
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().AddForce(racket.transform.forward * 5.0f, ForceMode.Impulse);
        }
    }
}
