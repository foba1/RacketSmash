using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackAMole 
{
    public class MoleBase : MonoBehaviour
    {
        private LevelManager levelManger;
        private float currentPosition;
        [Header("EffectPrefab")]
        [SerializeField] GameObject[] hitEffectPrefab;
        [SerializeField] private float direction = 0.3f;
        [SerializeField] private float zMax = 0.8f;
        [SerializeField] private float zMin = 0.3f;
        [SerializeField] private int score;
        [SerializeField] private bool isMove=false;
        [SerializeField] private bool isHit=true;
        private Vector3 initPosition;
        private System.Random random;
        private int randomEffectIdx;
        
        public int moleScore { get { return score; } set { score = value; } }
        // Start is called before the first frame update
        void Start()
        {
            levelManger = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            currentPosition = transform.localPosition.z; 
            initPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
            random = new System.Random();
        }

        // Update is called once per frame
        void Update()
        {
            currentPosition += Time.deltaTime * direction;
            // ������ ��
            if (isMove)
            {
                // �ȸ¾��� ��
                if (!isHit)
                {
                    // ������
                    if (currentPosition >= zMax)
                    {
                        direction *= -1;
                        currentPosition = zMax;
                    }
                    // ����
                    else if (currentPosition <= zMin)
                    {
                        direction *= -1;
                        currentPosition = zMin;
                    }
                }
                // �¾��� ��
                else
                {
                    if (direction < 0)
                    {
                        direction *= -1;
                    }
                    if (transform.localPosition.z >= initPosition.z)
                    {
                        currentPosition = initPosition.z;
                    }
                }
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentPosition);
            }


        }

        private void OnCollisionEnter(Collision collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball != null) 
            {
                if (!isHit)
                {
                    // �δ��� �ǰ� ȿ��
                    randomEffectIdx = random.Next(0, 4);
                    GameObject effect = Instantiate(hitEffectPrefab[randomEffectIdx], transform.position, Quaternion.identity);
                    Destroy(effect, 2f);
                    // �δ��� ���� Ƚ�� ����
                    levelManger.count = 1;
                    // ���� ����
                    levelManger.totalScore = score;
                    Debug.Log(score.ToString() + "�� ȹ��! \n ���� ���ھ� : " + levelManger.totalScore);
                    // �δ��� Ÿ�� üũ
                    isHit = true;
                }
            }
        }

        public void setMolehit()
        {
            Debug.Log("Set isHit to :" + isHit);
            isHit=true;
        }

        public void setMoleUnHit()
        {
            isHit = false;
        }

        public void setMoleMove(int idx)
        {
            Debug.Log("Mole" + idx.ToString() + "Set to move");
            isMove =true;
        }

        public void setMoleNotMove()
        {
            isMove = false;
        }
        
        public void resetPosition()
        {
            transform.localPosition = initPosition;
        }
    }

}