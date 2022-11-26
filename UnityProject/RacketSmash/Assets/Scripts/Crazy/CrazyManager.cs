using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyManager : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] GameObject[] healthBar;

    [Header("Ball Destroy Effect")]
    public GameObject destroyEffectPrefab;

    public bool isGameFinished;

    private float prevTime;
    private float speedTime;
    private float remainedTime;
    private float curSpeed;

    private float initRemainedTime = 60f;
    private float initSpeed = 1f;

    private static readonly float initLocalPosY = 2.5f;
    private static readonly float initHeight = 4f;
    private static readonly float failTime = 2f;

    static CrazyManager instance;
    public static CrazyManager Instance
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
        StartGame();
    }

    private void Update()
    {
        if (!isGameFinished)
        {
            float deltaTime = (Time.time - prevTime) * curSpeed;
            remainedTime -= deltaTime;
            UpdateHealthBar(deltaTime);
            UpdateSpeed();

            prevTime = Time.time;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        isGameFinished = false;
        BallManager.Instance.StartGame();

        speedTime = Time.time;
        prevTime = Time.time;
        remainedTime = initRemainedTime;
        curSpeed = initSpeed;

        for (int i = 0; i < healthBar.Length; i++)
        {
            healthBar[i].transform.localPosition = new Vector3(healthBar[i].transform.localPosition.x, initLocalPosY, healthBar[i].transform.localPosition.z);
        }
    }

    public void FailToReceiveBall()
    {
        remainedTime -= failTime;
        UpdateHealthBar(failTime);
        if (remainedTime <= 0)
        {
            GameOver();
        }
    }

    private void UpdateHealthBar(float time)
    {
        for (int i = 0; i < healthBar.Length; i++)
        {
            healthBar[i].transform.localPosition -= new Vector3(0f, (initHeight / initRemainedTime) * time, 0f);
        }
    }

    private void UpdateSpeed()
    {
        if (Time.time - speedTime >= 10f)
        {
            curSpeed += 0.5f;
            speedTime = Time.time;
        }
    }

    private void GameOver()
    {
        isGameFinished = true;
        BallManager.Instance.GameOver();
    }
}
