using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace WhackAMole
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private TextMeshPro timer;
        [SerializeField] private GameObject gameOverText;
        [SerializeField] private float timeOut;
        [SerializeField] private int maxLevel;
        private int level=1;
        private float elapsedTime;
        private int score;
        public int totalScore
        {
            get { return score; }
            set { score += value; }
        }
        // Start is called before the first frame update
        void Start()
        {
            gameOverText.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= timeOut)
            {
                gameOverText.SetActive(true);
            }
            timer.text = "Elapsed Time : " + string.Format("{0:N2}", elapsedTime.ToString());
        }

        
    }
}