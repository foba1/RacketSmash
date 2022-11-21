using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [Header("Ball Prefab")]
    [SerializeField] GameObject ballPrefab;

    private List<GameObject> ballPoint;
    private GameObject playerPoint;
    private float prevSpawnTime;
    private float[] spawnDeltaTime = new float[10] { 5f, 4.5f, 4f, 3.7f, 3.3f, 3f, 2.8f, 2.6f, 2.3f, 2f };
    private int curLevel = 0;

    private void Start()
    {
        prevSpawnTime = Time.time;
        playerPoint = GameObject.Find("PlayerPoint");
        ballPoint = new List<GameObject>();

        GameObject curvedWall = GameObject.Find("CurvedWall");
        for (int i = 0; i < curvedWall.transform.childCount; i++)
        {
            ballPoint.Add(curvedWall.transform.GetChild(i).gameObject);
        }
    }

    private void Update()
    {
        if (Time.time - prevSpawnTime >= spawnDeltaTime[curLevel])
        {
            prevSpawnTime = Time.time;
            int index = Random.Range(0, ballPoint.Count);
            GameObject ball = Instantiate(ballPrefab, ballPoint[index].transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().velocity = (playerPoint.transform.position - ballPoint[index].transform.position);
            ball.GetComponent<Ball>().SetCrazyMode();

            if (curLevel < spawnDeltaTime.Length - 1) curLevel++;
        }
    }
}
