using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using TMPro;
using UnityEngine.UI;

namespace WhackAMole
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TextMeshPro timerText;
        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private MolesController molesController;
        [SerializeField] private GameObject ball;
        [Header("Setting")]
        [SerializeField] private float timeOut;
        private float elapsedTime;
        private int score;
        private bool isGameOver;
        private bool isGameStart;

        [Header("Panel")]
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject resultPanel;
        [SerializeField] private GameObject instructionPanel;

        [Header("Catch test")]
        [SerializeField] private int catchCount;

        public int count { get { return catchCount; } set { catchCount += value; } }
        public int totalScore { get { return score; } set { score += value; } }

        // Start is called before the first frame update
        void Start()
        {
            isGameOver = false;
            isGameStart = false;
            instructionPanel.gameObject.SetActive(true);
            SetMainPanel(false);
            molesController.gameObject.SetActive(false);
            ball.SetActive(false);
            SetResultPanel(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (isGameStart)
            {
                // 점수 표시
                scoreText.text = "Score : " + totalScore;

                // 타임아웃 게임 오버
                if (elapsedTime >= timeOut)
                {
                    resultPanel.transform.GetChild(2).GetComponent<Text>().text = "점수 : " + totalScore.ToString() + "점";
                    //gameEndText.GetComponent<TextMeshPro>().text = "Game Over !\nScore : " + totalScore.ToString();
                    SetMainPanel(false);
                    SetResultPanel(true);
                    ball.SetActive(false);
                    molesController.hideMoles();
                    isGameOver = true;
                    PlayerPrefs.SetString("Mole", score.ToString() + "점");
                }
                if (!isGameOver)
                {
                    elapsedTime += Time.deltaTime;
                    timerText.text = "Elapsed Time : " + string.Format("{0:N}", elapsedTime);
                }


                // 재시작
                if (Input.GetKeyDown(KeyCode.R))
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }

                // 다 잡았을 시

                if (catchCount == molesController.randomMoleSize)
                {
                    Debug.Log(catchCount.ToString() + "/" + molesController.randomMoleSize.ToString());
                    Debug.Log("라운드 클리어");
                    catchCount = 0;
                    molesController.roundClearState = true;
                }
            }
            
        }
        public void SetMainPanel(bool state)
        {
            mainPanel.SetActive(state);
        }

        public void SetResultPanel(bool state)
        {
            resultPanel.SetActive(state);
        }

        public void StartGame()
        {
            isGameStart = true;
            SetMainPanel(true);
            instructionPanel.gameObject.SetActive(false);
            molesController.gameObject.SetActive(true);
            ball.SetActive(true);
        }

    }

}