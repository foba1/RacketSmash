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
        [SerializeField] private MolesController molesController;
        [SerializeField] private GameObject ball;
        [SerializeField] private float timeOut;
        [SerializeField] private int maxLevel;
        [SerializeField] private int level=1;
        public int round { get { return level; } set { level += value; } }
        private float elapsedTime;
        private int score;
        [SerializeField] private int catchCount;
        public int count { get { return catchCount; } set { catchCount += value; } }
        public int totalScore { get { return score; } set { score += value; } }

        // ���� ���� �ؽ�Ʈ Ÿ�̸�
        private float clearTime;

        // Start is called before the first frame update
        void Start()
        {
            gameEndText.SetActive(false);
            molesController.round = level;
        }

        // Update is called once per frame
        void Update()
        {
            // ���� Ŭ���� �ؽ�Ʈ ����
            if (Time.time - clearTime > 5)
            {
                gameEndText.SetActive(false);
                molesController.showMoles();
            }

            // ������ ������� �����ϸ� �¸�
            if (level > maxLevel)
            {
                gameEndText.GetComponent<TextMeshPro>().text = "You Win!\nScore : " + totalScore.ToString();
                gameEndText.SetActive(true);
                ball.SetActive(false);
                molesController.hideMoles();
            }

            // �ٴڿ� ���� �ι� ƨ��� ���� ����
            //if (ball.GetComponent<Ball>().groundHit == 2)
            //{
            //    gameEndText.GetComponent<TextMeshPro>().text = "Game Over !\nScore : " + totalScore.ToString();
            //    gameEndText.SetActive(true);

            //    ball.SetActive(false);
            //    molesController.hideMoles();
            //}

            // Ÿ�Ӿƿ� ���� ����
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeOut)
            {
                gameEndText.GetComponent<TextMeshPro>().text = "Game Over !\nScore : " + totalScore.ToString();
                gameEndText.SetActive(true);

                ball.SetActive(false);
                molesController.hideMoles();
            }
            timerText.text = "Elapsed Time : " + string.Format("{0:N}", elapsedTime.ToString());

            // ���� ���� �׽�Ʈ
            if (Input.GetKeyDown(KeyCode.Space))
            {
                gameEndText.GetComponent<TextMeshPro>().text = "Game Over !\nScore : " + totalScore.ToString();
                gameEndText.SetActive(true);
            }

            // ���� Ȯ�� (�Ͻ� ���� �� �ٽ� ����)
            if (catchCount >= level)
            {
                Debug.Log("���� Ŭ����");
                gameEndText.GetComponent<TextMeshPro>().text="Round "+level.ToString()+ " Clear !\nScore : " + totalScore.ToString();
                gameEndText.SetActive(true);
                molesController.hideMoles();
                level += 1;
                molesController.round = 1;
                clearTime = Time.time;
                catchCount = 0;
                molesController.roundClearState = true;
            }

            
        }

        
    }
}