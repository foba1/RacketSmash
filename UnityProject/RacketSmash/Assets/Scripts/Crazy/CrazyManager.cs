using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyManager : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] GameObject[] healthBar;

    [Header("Ball Destroy Effect")]
    [SerializeField] GameObject destroyEffectPrefab;

    public bool isGameFinished;

    private float prevTime;
    private float speedTime;
    private float remainedTime;
    private float curSpeed;

    private float initRemainedTime = 60f;
    private float initSpeed = 1f;

    private static readonly float initLocalPosY = 2.5f;
    private static readonly float initHeight = 4f;
    private static readonly float successTime = 2.5f;
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
            UpdateHealthBar(-deltaTime);
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

    public void SuccessToReceiveBall()
    {
        remainedTime += successTime;
        UpdateHealthBar(successTime);
    }

    public void FailToReceiveBall()
    {
        remainedTime -= failTime;
        UpdateHealthBar(-failTime);
        if (remainedTime <= 0)
        {
            GameOver();
        }
    }

    public void DestroyBall(GameObject ball)
    {
        if (ball != null)
        {
            GameObject effect = Instantiate(destroyEffectPrefab, ball.transform.position, Quaternion.identity);
            Destroy(effect, 2f);
            BallManager.Instance.DestroyBall(ball);
            Destroy(ball);
        }
    }

    IEnumerator DestroyBallCoroutine(GameObject ball, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        DestroyBall(ball);
    }

    public void DestroyBall(GameObject ball, float time)
    {
        StartCoroutine(DestroyBallCoroutine(ball, time));
    }

    private void UpdateHealthBar(float time)
    {
        if (remainedTime >= initRemainedTime)
        {
            remainedTime = initRemainedTime;
            for (int i = 0; i < healthBar.Length; i++)
            {
                healthBar[i].transform.localPosition = new Vector3(healthBar[i].transform.localPosition.x, initLocalPosY, healthBar[i].transform.localPosition.z);
            }
        }
        else
        {
            for (int i = 0; i < healthBar.Length; i++)
            {
                healthBar[i].transform.localPosition += new Vector3(0f, (initHeight / initRemainedTime) * time, 0f);
            }
        }
    }

    private void UpdateSpeed()
    {
        if (Time.time - speedTime >= 10f)
        {
            curSpeed += 0.3f;
            speedTime = Time.time;
        }
    }

    private void GameOver()
    {
        isGameFinished = true;
        BallManager.Instance.GameOver();
    }
}
