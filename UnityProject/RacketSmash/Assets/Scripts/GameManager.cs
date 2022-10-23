using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPun
{
    public GameObject ball;
    public GameObject player;

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            ball = PhotonNetwork.Instantiate("Ball", new Vector3(1.3f, 1f, -2f), Quaternion.identity);
        }
        else
        {
            ball = GameObject.Find("Ball");
        }

        player = PhotonNetwork.Instantiate("Player", new Vector3(0f, 0.35f, -3f), Quaternion.identity);
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && Input.GetKeyDown(KeyCode.Space))
        {
            ball.transform.position = player.transform.GetChild(0).position + new Vector3(0f, 0.5f, 0.5f);
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }
}
