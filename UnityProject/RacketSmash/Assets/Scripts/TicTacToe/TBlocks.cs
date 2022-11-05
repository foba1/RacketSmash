using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BaseSquash;

namespace TicTacToe
{ 
    public class TBlocks : MonoBehaviour
    {
        [SerializeField] private List<GameObject> blockList = new List<GameObject>();
        [SerializeField] private Material[] mat = new Material[3];
        [SerializeField] private FrontWallBase frontWall;
        [SerializeField] private LevelManagerBase levelManager;
        private int[] texIdx = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
 
        // Start is called before the first frame update
        void Start()
        {
            frontWall = GameObject.Find("Front").GetComponent<FrontWallBase>();
            levelManager = frontWall.GetComponent<LevelManagerBase>();
 
            blockList.Add(GameObject.Find("A"));
            blockList.Add(GameObject.Find("B"));
            blockList.Add(GameObject.Find("C"));
            blockList.Add(GameObject.Find("D"));
            blockList.Add(GameObject.Find("E"));
            blockList.Add(GameObject.Find("F"));
            blockList.Add(GameObject.Find("G"));
            blockList.Add(GameObject.Find("H"));
            blockList.Add(GameObject.Find("I"));
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                texIdx[0] = (texIdx[0] + 1) % 3;
                blockList[0].GetComponent<MeshRenderer>().material = mat[texIdx[0]];
            }
            if (Input.GetKeyDown(KeyCode.B))
            {
                texIdx[1] = (texIdx[1] + 1) % 3;
                blockList[1].GetComponent<MeshRenderer>().material = mat[texIdx[1]];
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                texIdx[2] = (texIdx[2] + 1) % 3;
                blockList[2].GetComponent<MeshRenderer>().material = mat[texIdx[2]];
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                texIdx[3] = (texIdx[3] + 1) % 3;
                blockList[3].GetComponent<MeshRenderer>().material = mat[texIdx[3]];
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                texIdx[4] = (texIdx[4] + 1) % 3;
                blockList[4].GetComponent<MeshRenderer>().material = mat[texIdx[4]];
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                texIdx[5] = (texIdx[5] + 1) % 3;
                blockList[5].GetComponent<MeshRenderer>().material = mat[texIdx[5]];
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                texIdx[6] = (texIdx[6] + 1) % 3;
                blockList[6].GetComponent<MeshRenderer>().material = mat[texIdx[6]];
            }
            if (Input.GetKeyDown(KeyCode.H))
            {
                texIdx[7] = (texIdx[7] + 1) % 3;
                blockList[7].GetComponent<MeshRenderer>().material = mat[texIdx[7]];
            }
            if (Input.GetKeyDown(KeyCode.I))
            {
                texIdx[8] = (texIdx[8] + 1) % 3;
                blockList[8].GetComponent<MeshRenderer>().material = mat[texIdx[8]];
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            BallBase ball = collision.gameObject.GetComponent<BallBase>();
            if (ball != null)
            {
                OnBallCollision(ball);
                levelManager.OnBallHitWall();
            }
        }

        protected virtual void OnBallCollision(BallBase ball)
        {
            Debug.Log(ball.transform.position);
            float x = ball.transform.position.x;
            float y = ball.transform.position.y;
            float z = ball.transform.position.z;
            // A�� ���� ���
            if(-6.44<=x && x<=0.56 && -2.42<=y && y <= 2.58)
            {
                Debug.Log("A�� ����");
            }
            // B�� ���� ���
            if(0.68<=x && x<=7.68 && -2.42 <= y && y <= 2.58)
            {
                Debug.Log("B�� ����");
            }
            // C�� ���� ���
            if(7.78<=x && x<=14.78 && -2.42 <= y && y <= 2.58)
            {
                Debug.Log("C�� ����");
            }
            // D�� ���� ���
            if(-6.44 <= x && x <= 0.56 && -2.47 <= y && y <= -7.47)
            {
                Debug.Log("D�� ����");
            }
            // E�� ���� ���
            if(0.68 <= x && x <= 7.68 && -2.47 <= y && y <= -7.47)
            {
                Debug.Log("E�� ����");
            }
            // F�� ���� ���
            if(7.78 <= x && x <= 14.78 && -2.47 <= y && y <= -7.47)
            {
                Debug.Log("F�� ����");
            }
            // G�� ���� ���
            if (-6.44 <= x && x <= 0.56 && -7.52 <= y && y <= -12.52)
            {
                Debug.Log("G�� ����");
            }
            // H�� ���� ���
            if (0.68 <= x && x <= 7.68 && -7.52 <= y && y <= -12.52)
            {
                Debug.Log("H�� ����");
            }
            // I�� ���� ���
            if (7.78 <= x && x <= 14.78 && -7.52 <= y && y <= -12.52)
            {
                Debug.Log("I�� ����");
            }
        }
    }
}

