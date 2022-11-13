using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackAMole 
{
    public class MoleBase : MonoBehaviour
    {
        private LevelManager levelManger;
        [SerializeField] private int score;
        public int moleScore { get { return score; } set { score = value; } }
        // Start is called before the first frame update
        void Start()
        {
            levelManger = GameObject.Find("LevelManager").GetComponent<LevelManager>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                levelManger.totalScore = score;
                Debug.Log(score.ToString() + "Á¡ È¹µæ! \n ÇöÀç ½ºÄÚ¾î : " + levelManger.totalScore);
            }
        }
    }

}
