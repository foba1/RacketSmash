using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGameManager : MonoBehaviour
{
    public bool isGameFinished;

    public Transform ballSpawnPosition;
    public Transform userPoint;

    private GameObject ball;
    private GameObject ballPrefab;


    static BrickGameManager instance;
    public static BrickGameManager Instance
    {
        get
        {
            if (!instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
        ballPrefab = Resources.Load<GameObject>("Ball_NoGravity");
    }

    private void Start()
    {
        StartGame();
    }

    private void Update()
    {

    }

    public void StartGame()
    {
        isGameFinished = false;

        Invoke("SpawnBall", 3);
        //Instantiate Ball
    }

    public void SpawnBall()
    {
        ball = Instantiate(ballPrefab, ballSpawnPosition.transform.position, Quaternion.identity);
        Vector3 shootVelocity = userPoint.transform.position - ballSpawnPosition.transform.position;
        ball.gameObject.GetComponent<Rigidbody>().velocity = shootVelocity;
    }

    private void GameOver()
    {
        isGameFinished = true;
    }
}
