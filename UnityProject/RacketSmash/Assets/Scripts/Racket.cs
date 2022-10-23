using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Racket : MonoBehaviourPun
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
                Vector3 direction = new Vector3(0f, 0.425f, 1f);
                gameManager.ball.GetComponent<Rigidbody>().velocity = direction * 10.0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0.425f, 1f) * 10.0f;
    }
}
