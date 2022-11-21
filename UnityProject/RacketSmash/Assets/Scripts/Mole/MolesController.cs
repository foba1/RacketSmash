using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackAMole 
{
    public class MolesController : MonoBehaviour
    { 
        [SerializeField] private GameObject[] moles;
        private List<int> randomMoles;
        private bool isRoundCleared = false;
        private float timer;
        private int randomSize;
        public int randomMoleSize { get { return randomSize; } set { randomSize = value; } }

        [Header("test")]
        [SerializeField] private int waitingTime;
        public bool roundClearState { get { return isRoundCleared; } set { isRoundCleared = value; } }
        private System.Random random;
        // Start is called before the first frame update
        void Start()
        {
            timer = 0.0f;
            random = new System.Random();
            randomMoles = new List<int>();

            // 랜덤 점수 할당
            for (int i=0; i<moles.Length; i++)
            {
                moles[i].GetComponent<MoleBase>().moleScore=random.Next(1, 10);
            }

            // 랜덤으로 두더지 움직임 활성화
            spawnMoles();
        }

        // Update is called once per frame
        void Update()
        {
            // 다 맞추면 또 랜덤으로 활성화
            if (isRoundCleared)
            {
                // 바로 초기화 하면 놓치는 두더지가 생김
                timer += Time.deltaTime;

                if (timer > waitingTime)
                {
                    // 두더지 초기화
                    for (int i = 0; i < randomMoles.Count; i++)
                    {
                        moles[randomMoles[i]].GetComponent<MoleBase>().setMoleNotMove();
                        moles[randomMoles[i]].GetComponent<MoleBase>().setMoleUnHit();
                    }
                    randomMoles.Clear();
                    spawnMoles();
                    isRoundCleared = false;
                    timer = 0.0f;
                }
                
            }
        }

        private void spawnMoles()
        {
            int iter = 0;
            randomSize = random.Next(1, 10);
            while (randomMoles.Count != randomSize)
            {
                int num = random.Next(0, 9);
                if (randomMoles.Contains(num))
                {
                    continue;
                }
                else
                {
                    randomMoles.Add(num);
                    iter++;
                }
            }
            Debug.Log("randomMoles size : "+ randomMoles.Count.ToString());
            // 두더지 움직임 활성화
            for (int i = 0; i < randomSize; i++)
            {   
                moles[randomMoles[i]].GetComponent<MoleBase>().setMoleMove(randomMoles[i]);
            }
        }

        public void hideMoles()
        {
            for (int i=0; i<moles.Length; i++)
            {
                moles[i].SetActive(false);
            } 
        }

        public void showMoles()
        {
            for(int i=0; i<moles.Length; i++)
            {
                moles[i].SetActive(true);
            }
        }
    }
}


