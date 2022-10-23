using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Player : MonoBehaviourPun
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Vector3.Distance(gameManager.ball.transform.position, gameManager.player.transform.GetChild(0).position) <= 1.0f)
            {
                photonView.RPC("HitBall", RpcTarget.MasterClient);
            }
        }

        if (photonView.IsMine)
        {
            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                transform.position += new Vector3(0f, 0f, 0.005f);
            }
            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                transform.position -= new Vector3(0f, 0f, 0.005f);
            }
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                transform.position -= new Vector3(0.005f, 0f, 0f);
            }
            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                transform.position += new Vector3(0.005f, 0f, 0f);
            }
        }
    }

    [PunRPC]
    public void HitBall()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Vector3 direction = new Vector3(0f, 0.425f, 1f);
            gameManager.ball.GetComponent<Rigidbody>().velocity = direction * 10.0f;
        }
    }
}
