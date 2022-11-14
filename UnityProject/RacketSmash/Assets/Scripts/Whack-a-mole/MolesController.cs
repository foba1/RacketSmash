using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WhackAMole 
{
    public class MolesController : MonoBehaviour
    {
        [SerializeField] private GameObject[] moles;
        private int level;
        public int round { get { return level; } set { level += value; } }
        private System.Random random;
        // Start is called before the first frame update
        void Start()
        {
            random = new System.Random();

            // 랜덤 점수 할당
            for (int i=0; i<moles.Length; i++)
            {
                moles[i].GetComponent<MoleBase>().moleScore=random.Next(1, 10);
            }
        }

        // Update is called once per frame
        void Update()
        {
            
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


