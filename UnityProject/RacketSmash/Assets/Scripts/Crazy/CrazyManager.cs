using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyManager : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] GameObject[] healthBar;

    [Header("Ball Destroy Effect")]
    public GameObject destroyEffectPrefab;

    private float prevTime;
    private float speedTime;
    private float remainedTime;
    private float curSpeed;
    private bool isGameFinished;

    private float initRemainedTime = 120f;
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
            UpdateHealthBar();
            UpdateSpeed();
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
        if (remainedTime <= 0)
        {
            GameOver();
        }
    }

    private void UpdateHealthBar()
    {
        float deltaTime = Time.time - prevTime;
        remainedTime -= deltaTime * curSpeed;
        for (int i = 0; i < healthBar.Length; i++)
        {
            healthBar[i].transform.localPosition -= new Vector3(0f, (initHeight / initRemainedTime) * deltaTime, 0f);
        }

        prevTime = Time.time;
    }

    private void UpdateSpeed()
    {
        if (Time.time - speedTime >= 10f)
        {
            curSpeed += 0.1f;
            speedTime = Time.time;
        }
    }

    private void GameOver()
    {
        isGameFinished = true;
    }
}
