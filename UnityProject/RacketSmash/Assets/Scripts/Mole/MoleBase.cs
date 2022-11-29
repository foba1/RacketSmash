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
        [Header("Settings")]
        [SerializeField] private float direction = 0.3f;
        [SerializeField] private float zMax = 0.8f;
        [SerializeField] private float zMin = 0.3f;
        [SerializeField] private int score;
        [SerializeField] private bool isSpawned = false;
        [SerializeField] private bool isMove=false;
        [SerializeField] private bool isHit=true;
        [SerializeField] private float moleSpeed;
        private Vector3 initPosition;
        [SerializeField] private Quaternion initRotation;
        private System.Random random;
        private int randomEffectIdx;
        private Animator anim;
        private float timer;
        private int moleCount;
        [Header("Spawn delay")]
        [SerializeField] private int spawnWaitingTime;
        float damp = 3.0f;
        Quaternion rotate;

        public int moleScore { get { return score; } set { score = value; } }
        public bool moveState { get { return isSpawned; } set { isSpawned = value; } }
        public bool hitState { get { return isHit; } set { isHit = value; } }
        public int totalMoleCount { get { return moleCount; } set { moleCount = value; } }
        public int spawnTime { get { return spawnWaitingTime; } set { spawnWaitingTime = value; } }
        // Start is called before the first frame update
        void Start()
        {
            levelManger = GameObject.Find("LevelManager").GetComponent<LevelManager>();
            currentPosition = transform.localPosition.z; 
            initPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
            initRotation = transform.rotation;
            random = new System.Random();
            anim = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            currentPosition += Time.deltaTime * direction;

            // ��ȯ�� �δ����� �ڷ�ƾ �����Ͽ� �������� ������ ����
            if (isSpawned)
            {
                StartCoroutine(setMoleMove(spawnTime));
                // ������ ��
                if (isMove)
                {
                    // �ȸ¾��� ��
                    if (!isHit)
                    {
                        // �δ��� �ȹٷ� �����
                        transform.rotation = initRotation;
                        anim.speed = 1;
                        // ������
                        if (currentPosition == zMax)
                        {
                            anim.speed *= 1;
                        }
                        else if (currentPosition > zMax)
                        {

                            direction *= -1;
                            currentPosition = zMax;
                        }
                        // ����
                        else if (currentPosition == zMin)
                        {
                            anim.speed *= -1;
                        }
                        else if (currentPosition < zMin)
                        {
                            direction *= -1;
                            currentPosition = zMin;
                        }
                    }
                    // �¾��� ��
                    else
                    {
                        rotate = Quaternion.LookRotation(new Vector3(0, -transform.localPosition.y, 0));
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotate, Time.deltaTime * damp);
                        anim.speed = 0;

                        if (direction < 0)
                        {

                            direction *= -1;
                        }
                        if (transform.localPosition.z >= zMax)
                        {
                            currentPosition = zMax;
                            isSpawned = false;
                        }
                    }
                    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentPosition);
                }
            }

        }

        public void collisionEnter(Collision collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                if (!isHit)
                {
                    // �δ��� �ǰ� ȿ��
                    randomEffectIdx = random.Next(0, 4);
                    GameObject effect = Instantiate(hitEffectPrefab[randomEffectIdx], transform.position, Quaternion.identity);
                    effect.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
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
        
        
        public IEnumerator setMoleMove(int time)
        {            
            if (!isMove) {
                yield return new WaitForSeconds(time);
                isMove = true;
                spawnWaitingTime = time;
            }            
        }

        public void resetPosition()
        {
            transform.localPosition = initPosition;
        }
    }

}
