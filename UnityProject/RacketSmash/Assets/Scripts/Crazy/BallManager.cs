using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    [Header("Ball Prefab")]
    [SerializeField] GameObject ballPrefab;

    [Header("Settings")]
    [SerializeField] float landZOffset = 1;
    [SerializeField] bool controlXAxis = false;

    private List<GameObject> ballPoint;
    private List<GameObject> ballList;
    private GameObject playerPoint;
    private float prevSpawnTime;
    private float[] spawnDeltaTime = new float[10] { 5f, 4.5f, 4f, 3.7f, 3.3f, 3f, 2.8f, 2.6f, 2.3f, 2f };
    private int curLevel = 0;

    private void Start()
    {
        prevSpawnTime = Time.time;
        playerPoint = GameObject.Find("PlayerPoint");
        ballPoint = new List<GameObject>();
        ballList = new List<GameObject>();

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
            ballList.Add(ball);

            Vector3 collisionPoint = ball.transform.position;

            Vector3 collisionToPlayer = collisionPoint - playerPoint.transform.position;
            float xOffset = collisionToPlayer.x * landZOffset / collisionToPlayer.z;
            if (!controlXAxis)
                xOffset = 0;

            Vector3 targetPoint = playerPoint.transform.position + new Vector3(xOffset, 0, landZOffset);
            Vector3 directLine = targetPoint - collisionPoint;

            float v0 = -Physics.gravity.y / 2 - Mathf.Abs(directLine.y);

            ball.GetComponent<Rigidbody>().velocity = new Vector3(directLine.x, v0, directLine.z);
            ball.GetComponent<Ball>().SetCrazyMode();

            if (curLevel < spawnDeltaTime.Length - 1) curLevel++;
        }

        for (int i = 0; i < ballList.Count; i++)
        {
            if (ballList[i].transform.position.z <= -6.5f)
            {
                GameObject ball = ballList[i];
                ballList.RemoveAt(i);
                if (!ball.GetComponent<Ball>().hitByRacket)
                {
                    CrazyManager.Instance.FailToReceiveBall();
                }
                Destroy(ball);
            }
        }
    }
}
