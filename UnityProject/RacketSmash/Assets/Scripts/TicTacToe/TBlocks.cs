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
            levelManager = GameObject.Find("LevelManager").GetComponent<LevelManagerBase>();

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
            /*if (Input.GetKeyDown(KeyCode.A))
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
            }*/
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
            // A에 맞을 경우
            if(x < -3.51 && 9.8<=y)
            {
                Debug.Log("A에 맞음");
                texIdx[0] = (texIdx[0] + 1) % 3;
                blockList[0].GetComponent<MeshRenderer>().material = mat[texIdx[0]];

            }
            // B에 맞을 경우
            else if(-3.51<=x && x<3.51 && 9.8 <= y)
            {
                Debug.Log("B에 맞음");
                texIdx[1] = (texIdx[1] + 1) % 3;
                blockList[1].GetComponent<MeshRenderer>().material = mat[texIdx[1]];
            }
            // C에 맞을 경우
            else if(3.51<=x && 9.8 <= y)
            {
                Debug.Log("C에 맞음");
                texIdx[2] = (texIdx[2] + 1) % 3;
                blockList[2].GetComponent<MeshRenderer>().material = mat[texIdx[2]];
            }
            // D에 맞을 경우
            else if(x < -3.51 && 4.8<=y && y<9.8)
            {
                Debug.Log("D에 맞음");
                texIdx[3] = (texIdx[3] + 1) % 3;
                blockList[3].GetComponent<MeshRenderer>().material = mat[texIdx[3]];
            }
            // E에 맞을 경우
            else if(-3.51 <= x && x < 3.51 && 4.8 <= y && y < 9.8)
            {
                Debug.Log("E에 맞음");
                texIdx[4] = (texIdx[4] + 1) % 3;
                blockList[4].GetComponent<MeshRenderer>().material = mat[texIdx[4]];
            }
            // F에 맞을 경우
            else if (3.51<=x && 4.8 <= y && y < 9.8)
            {
                Debug.Log("F에 맞음");
                texIdx[5] = (texIdx[5] + 1) % 3;
                blockList[5].GetComponent<MeshRenderer>().material = mat[texIdx[5]];
            }
            // G에 맞을 경우
            else if (x < -3.51 && y<4.8)
            {
                Debug.Log("G에 맞음");
                texIdx[6] = (texIdx[6] + 1) % 3;
                blockList[6].GetComponent<MeshRenderer>().material = mat[texIdx[6]];
            }
            // H에 맞을 경우
            else if (-3.51 <= x && x < 3.51 && y < 4.8)
            {
                Debug.Log("H에 맞음");
                texIdx[7] = (texIdx[7] + 1) % 3;
                blockList[7].GetComponent<MeshRenderer>().material = mat[texIdx[7]];
            }
            // I에 맞을 경우
            else if (3.51 <= x && y < 4.8)
            {
                Debug.Log("I에 맞음");
                texIdx[8] = (texIdx[8] + 1) % 3;
                blockList[8].GetComponent<MeshRenderer>().material = mat[texIdx[8]];
            }
        }
    }
}

