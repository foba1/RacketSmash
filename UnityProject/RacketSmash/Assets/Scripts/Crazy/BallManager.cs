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
    [SerializeField] GameObject playerPoint;

    private List<GameObject> ballPoint;
    private List<GameObject> ballList;
    private float prevSpawnTime;
    private float waveDeltaTime = 3f;
    private float[] spawnDeltaTime = new float[10] { 1.5f, 1.3f, 1.1f, 0.9f, 0.7f, 0.5f, 0.4f, 0.3f, 0.2f, 0.1f };
    private int maxSpawnCount = 7;
    private int spawnCount;
    private int curSpawnCount;
    private int curLevel;

    static BallManager instance;
    public static BallManager Instance
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
    }

    private void Start()
    {
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
        if (!CrazyManager.Instance.isGameFinished)
        {
            if (curSpawnCount == -1)
            {
                if (Time.time - prevSpawnTime >= waveDeltaTime)
                {
                    curSpawnCount++;
                    prevSpawnTime = Time.time - spawnDeltaTime[curLevel];
                }
            }
            else if (curSpawnCount < spawnCount)
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

                    curSpawnCount++;
                }
            }
            else
            {
                curSpawnCount = -1;
                if (spawnCount < maxSpawnCount) spawnCount++;
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

    public void StartGame()
    {
        prevSpawnTime = Time.time;
        curLevel = 0;
        spawnCount = 2;
        curSpawnCount = -1;
    }

    public void GameOver()
    {
        for (int i = 0; i < ballList.Count; i++)
        {
            Destroy(ballList[i]);
        }
        ballList.Clear();
    }
}
