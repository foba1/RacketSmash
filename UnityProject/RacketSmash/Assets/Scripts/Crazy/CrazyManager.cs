using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CrazyManager : MonoBehaviour
{
    [Header("Health Bar")]
    [SerializeField] GameObject[] healthBar;

    [Header("Panel")]
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject startText;

    [Header("Ball Destroy Effect")]
    [SerializeField] GameObject destroyEffectPrefab;
    [SerializeField] GameObject successSound;

    public bool isGameFinished;

    private float gameStartTime;
    private int ballSuccessCount;

    private float prevTime;
    private float speedTime;
    private float remainedTime;
    private float curSpeed;
    private static readonly float[] successTime = new float[8] { 2.5f, 2.4f, 2.3f, 2.2f, 2.1f, 2.0f, 1.7f, 1.4f };

    private float initRemainedTime = 60f;
    private float initSpeed = 2f;

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
        isGameFinished = true;
        SetMainPanel(true);
        SetResultPanel(false);
    }

    private void Update()
    {
        if (!isGameFinished)
        {
            float deltaTime = (Time.time - prevTime) * curSpeed;
            remainedTime -= deltaTime;
            UpdateHealthBar(-deltaTime);
            UpdateSpeed();

            if (remainedTime <= 0f) GameOver();

            prevTime = Time.time;
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }

    public void SetMainPanel(bool state)
    {
        mainPanel.SetActive(state);
    }

    public void SetResultPanel(bool state)
    {
        resultPanel.SetActive(state);
    }

    IEnumerator StartText()
    {
        startText.SetActive(true);

        yield return new WaitForSecondsRealtime(1.5f);

        startText.SetActive(false);
    }

    public void StartGame()
    {
        isGameFinished = false;
        BallManager.Instance.StartGame();
        SetMainPanel(false);
        SetResultPanel(false);
        StartCoroutine(StartText());

        speedTime = Time.time;
        prevTime = Time.time;
        remainedTime = initRemainedTime;
        curSpeed = initSpeed;
        gameStartTime = Time.time;
        ballSuccessCount = 0;

        for (int i = 0; i < healthBar.Length; i++)
        {
            healthBar[i].transform.localPosition = new Vector3(healthBar[i].transform.localPosition.x, initLocalPosY, healthBar[i].transform.localPosition.z);
        }
    }

    public void SuccessToReceiveBall()
    {
        successSound.GetComponent<AudioSource>().Play();
        float time = successTime[BallManager.Instance.curLevel];
        remainedTime += time;
        UpdateHealthBar(time);
        ballSuccessCount++;
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
        if (Time.time - speedTime >= 5f)
        {
            curSpeed += 0.15f;
            speedTime = Time.time;
        }
    }

    private void GameOver()
    {
        isGameFinished = true;
        BallManager.Instance.GameOver();

        SetMainPanel(false);
        SetResultPanel(true);

        float score = Mathf.Round((Time.time - gameStartTime) * 10f) * 0.1f;
        string hitScore = (Mathf.Round((float)ballSuccessCount / (float)BallManager.Instance.ballCount * 1000f) * 0.1f).ToString();
        resultPanel.transform.GetChild(2).GetComponent<Text>().text = "버틴 시간 : " + score.ToString() + "초\n공 타격률 : " + hitScore + "%";

        if (PlayerPrefs.HasKey("Crazy"))
        {
            string prevHighScore = PlayerPrefs.GetString("Crazy");
            if (float.Parse(prevHighScore.Substring(0, prevHighScore.Length - 1)) < score)
            {
                PlayerPrefs.SetString("Crazy", score.ToString() + "초");
                PlayerPrefs.Save();
            }
        }
        else
        {
            PlayerPrefs.SetString("Crazy", score.ToString() + "초");
            PlayerPrefs.Save();
        }
    }
}
