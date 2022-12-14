using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickGameManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject successPanel;
    [SerializeField] GameObject failPanel;
    // [SerializeField] GameObject highestScoreText;
    [SerializeField] GameObject startText;
    [SerializeField] GameObject bricks;
    [SerializeField] GameObject weightlessWall;

    public AudioSource bgmPlayer;
    public AudioClip brickoutBgm;
    public AudioClip successBgm;
    public AudioClip failBgm;

    public Transform ballSpawnPosition;
    public Transform userPoint;

    private GameObject ball;
    private GameObject ballPrefab;
    private GameObject bricksCloned;
    private bool isGameFinished = true;

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
        mainPanel.SetActive(true);
        // highestScoreText.SetActive(true);
        successPanel.SetActive(false);
        failPanel.SetActive(false);
        startText.SetActive(false);
    }

    public void PushStartButton()
    {
        bgmPlayer.clip = brickoutBgm;
        bgmPlayer.Play();
        mainPanel.SetActive(false);
        // highestScoreText.SetActive(false);
        startText.SetActive(true);
        Invoke("ShutDownMainPanel", 1.5f);
        StartGame();
    }

    public void ShutDownMainPanel()
    {
        startText.SetActive(false);
    }

    public void StartGame()
    {
        mainPanel.SetActive(false);
        // highestScoreText.SetActive(false);
        successPanel.SetActive(false);
        failPanel.SetActive(false);
        bricks.SetActive(false);
        if (bricksCloned != null)
        {
            Destroy(bricksCloned);
        }
        bricksCloned = Instantiate(bricks);
        bricksCloned.SetActive(true);
        isGameFinished = false;
        InitializeIsGettingOut();
        if (ball != null)
        {
            Destroy(ball);
        }
        Invoke("SpawnBall", 3f);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }

    private void Update()
    {
        if (isGameFinished == false)
        {
            if (ball != null)
            {
                if (ball.transform.position.z < -6f)
                {
                    Destroy(ball);
                    failPanel.SetActive(true);
                    bgmPlayer.PlayOneShot(failBgm);
                    isGameFinished = true;
                }
            }
            if (bricksCloned.transform.childCount == 0)
            {
                Destroy(ball);
                successPanel.SetActive(true);
                bgmPlayer.PlayOneShot(successBgm);
                isGameFinished = true;
            }
        }
    }

    public void SpawnBall()
    {
        ball = Instantiate(ballPrefab, ballSpawnPosition.transform.position, Quaternion.identity);
        ball.name = "Ball";
        Vector3 shootVelocity = userPoint.transform.position - ballSpawnPosition.transform.position;
        ball.gameObject.GetComponent<Rigidbody>().velocity = shootVelocity;
    }

    public void InitializeIsGettingOut()
    {
        weightlessWall.GetComponent<WeightlessWall>().isGettingOut = true;
    }
}
