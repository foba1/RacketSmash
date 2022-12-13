using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BrickGameManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject resultPanel;
    [SerializeField] GameObject startText;

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
        mainPanel.SetActive(true);
        resultPanel.SetActive(false);
    }

    public void PushStartButton()
    {
        mainPanel.SetActive(false);
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
        Invoke("SpawnBall", 3f);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Main");
    }

    private void Update()
    {
        if (ball != null)
        {
            if (ball.transform.position.z < -6f)
            {
                Destroy(ball);
            }
        }
    }

    public void SpawnBall()
    {
        ball = Instantiate(ballPrefab, ballSpawnPosition.transform.position, Quaternion.identity);
        Vector3 shootVelocity = userPoint.transform.position - ballSpawnPosition.transform.position;
        ball.gameObject.GetComponent<Rigidbody>().velocity = shootVelocity;
    }
}
