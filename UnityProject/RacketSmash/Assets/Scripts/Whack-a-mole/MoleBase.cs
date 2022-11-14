using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackAMole 
{
    public class MoleBase : MonoBehaviour
    {
        private LevelManager levelManger;
        private float currentPosition;
        [SerializeField] private float direction = 0.3f;
        [SerializeField] private float zMax = 0.8f;
        [SerializeField] private float zMin = 0.3f;
        [SerializeField] private int score;
        [SerializeField] private bool isMove;
        
        public int moleScore { get { return score; } set { score = value; } }
        // Start is called before the first frame update
        void Start()
        {
            levelManger = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            currentPosition = transform.localPosition.z;
            Debug.Log(currentPosition.ToString());
            isMove = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isMove)
            {
                currentPosition += Time.deltaTime * direction;
                // 나오기
                if (currentPosition >= zMax)
                {
                    direction *= -1;
                    currentPosition = zMax;
                }
                // 들어가기
                else if (currentPosition <= zMin)
                {
                    direction *= -1;
                    currentPosition = zMin;
                }
            }
            
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentPosition);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball != null) 
            { 
                // 두더지 잡은 횟수 증가
                levelManger.count = 1;
                // 총점 증가
                levelManger.totalScore = score;
                Debug.Log(score.ToString() + "점 획득! \n 현재 스코어 : " + levelManger.totalScore);
                
            }
        }

        public void changeMoveState()
        {
            isMove = !isMove;
        }
        
    }

}
