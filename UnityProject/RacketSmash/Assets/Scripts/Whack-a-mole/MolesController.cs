using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackAMole 
{
    public class MolesController : MonoBehaviour
    {
        [SerializeField] private GameObject[] moles;
        private List<int> randomMoles;
        private int level;
        private bool isRoundInitialized=false;
        private bool isRoundCleared = false;
        public bool roundInitState { get { return isRoundInitialized; } set { isRoundInitialized =value; } }
        public bool roundClearState { get { return isRoundCleared; } set { isRoundCleared = value; } }
        public int round { get { return level; } set { level += value; } }
        private System.Random random;
        // Start is called before the first frame update
        void Start()
        {
            random = new System.Random();
            randomMoles = new List<int>();

            // 랜덤 점수 할당
            for (int i=0; i<moles.Length; i++)
            {
                moles[i].GetComponent<MoleBase>().moleScore=random.Next(1, 10);
            }
        }

        // Update is called once per frame
        void Update()
        {
            // 라운드 초기화 안됬을 시 랜덤으로 두더지 움직임 활성화
            if (!isRoundInitialized)
            {
                int iter = 0;
                while (iter < level)
                {
                    int num = random.Next(0, 10);
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
                // 두더지 움직임 활성화
                //showMoles();
                for (int i = 0; i < randomMoles.Count; i++)
                {
                    moles[randomMoles[i]].GetComponent<MoleBase>().setMoleMove();
                }
                isRoundInitialized = true;            
            }
            // 라운드 초기화 되고 클리어 되었을 시
            else if(isRoundInitialized&&isRoundCleared)
            {
                
                for (int i = 0; i < randomMoles.Count; i++)
                { 
                    // 두더지 타격 취소
                    moles[randomMoles[i]].GetComponent<MoleBase>().setMoleUnHit();
                    moles[randomMoles[i]].GetComponent<MoleBase>().setMoleNotMove();
                    moles[randomMoles[i]].GetComponent<MoleBase>().resetPosition();
                }
                //hideMoles();
                isRoundInitialized = false;
                isRoundCleared = false;
                randomMoles.Clear();
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


