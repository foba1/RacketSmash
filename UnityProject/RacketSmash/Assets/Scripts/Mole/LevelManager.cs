using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using TMPro;

namespace WhackAMole
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TextMeshPro timerText;
        [SerializeField] private GameObject gameEndText;
        [SerializeField] private TextMeshPro scoreText;
        [SerializeField] private MolesController molesController;
        [SerializeField] private GameObject ball;
        [Header("Setting")]
        [SerializeField] private float timeOut;
        private float elapsedTime;
        private int score;
        private bool isGameOver;
        
        [Header("Catch test")]
        [SerializeField] private int catchCount;
        public int count { get { return catchCount; } set { catchCount += value; } }
        public int totalScore { get { return score; } set { score += value; } }

        // ���� ���� �ؽ�Ʈ Ÿ�̸�
        private float clearTime;

        // Start is called before the first frame update
        void Start()
        {
            gameEndText.SetActive(false);
            isGameOver = false;
        }

        // Update is called once per frame
        void Update()
        {
            // ���� ǥ��
            scoreText.text = "Score : " + totalScore;

            // ���� Ŭ���� �ؽ�Ʈ ����
            if (Time.time - clearTime > 5)
            {
                gameEndText.SetActive(false);
                molesController.showMoles();
            }

            // Ÿ�Ӿƿ� ���� ����
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeOut)
            {
                gameEndText.GetComponent<TextMeshPro>().text = "Game Over !\nScore : " + totalScore.ToString();
                gameEndText.SetActive(true);

                ball.SetActive(false);
                molesController.hideMoles();
                isGameOver = true;
            }
            if (!isGameOver) { timerText.text = "Elapsed Time : " + string.Format("{0:N}", elapsedTime); }
            

            // �����
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
            }

            // �� ����� ��
            if (catchCount == molesController.randomMoleSize)
            {
                Debug.Log("���� Ŭ����");
                catchCount = 0;
                molesController.roundClearState = true;
            }

            
        }

        
    }
}