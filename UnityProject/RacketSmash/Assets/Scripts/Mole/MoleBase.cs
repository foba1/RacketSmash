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
        private float elapsedTime;
        private float waitingTime;
        private bool isFront;
        private Animator anim;
        private float timer;
        private int moleCount;
        [Header("Spawn delay")]
        [SerializeField] private int spawnWaitingTime;
        float damp = 3.0f;
        Quaternion rotate;

        public int moleScore { get { return score; } set { score = value; } }
        public bool spawnState { get { return isSpawned; } set { isSpawned = value; } }
        public bool hitState { get { return isHit; } set { isHit = value; } }
        public int totalMoleCount { get { return moleCount; } set { moleCount = value; } }
        public int spawnTime { get { return spawnWaitingTime; } set { spawnWaitingTime = value; } }
        public float moleWaitTime { get { return waitingTime; } set { waitingTime = value; } }
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

            // 소환할 두더지면 코루틴 실행하여 랜덤으로 움직임 시작
            if (isSpawned)
            {
                StartCoroutine(setMoleMove(spawnTime));
                // 움직일 때
                if (isMove)
                {
                    // 안맞았을 때
                    if (!isHit)
                    {
                        // 두더지 똑바로 세우기
                        transform.rotation = initRotation;
                        anim.speed = 1;

                        // 나왔을 때 랜덤 시간 동안 머무르기
                        // 나오기
                        if (currentPosition == zMax)
                        {
                            anim.speed *= 1;
                        }
                        else if (currentPosition > zMax)
                        {

                            direction *= -1;
                            currentPosition = zMax;
                        }
                        // 들어가기
                        else if (currentPosition == zMin)
                        {
                            anim.speed *= -1;
                        }
                        else if (currentPosition < zMin)
                        {
                            isFront = true;
                            direction *= -1;
                            currentPosition = zMin;
                        }
                    }
                    // 맞았을 때
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
                    if (isFront)
                    {
                        elapsedTime += Time.deltaTime;
                        if (elapsedTime > waitingTime)
                        {
                            isFront = false;
                            elapsedTime = 0;
                        }
                    }
                    else
                    {
                        currentPosition += Time.deltaTime * direction;
                        transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, currentPosition);
                    }
                }
            }

        }


        private void OnCollisionEnter(Collision collision)
        {
            Ball ball = collision.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                if (!isHit&&isSpawned)
                {
                    // 두더지 피격 효과
                    randomEffectIdx = random.Next(0, 4);
                    GameObject effect = Instantiate(hitEffectPrefab[randomEffectIdx], transform.position, Quaternion.identity);
                    effect.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                    Destroy(effect, 2f);
                    // 두더지 잡은 횟수 증가
                    levelManger.count = 1;
                    // 총점 증가
                    levelManger.totalScore = score*10;
                    Debug.Log(gameObject.name+"/"+score.ToString() + "점 획득! \n 현재 스코어 : " + levelManger.totalScore);
                    // 두더지 타격 체크
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
