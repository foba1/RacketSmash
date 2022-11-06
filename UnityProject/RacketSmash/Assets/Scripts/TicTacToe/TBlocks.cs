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
            Debug.Log(levelManager.GetCurrentTurn());
            float x = ball.transform.position.x;
            float y = ball.transform.position.y;
            float z = ball.transform.position.z;
            // A�� ���� ���
            if(x < -3.51 && 9.8<=y)
            {
                Debug.Log("A�� ����");
                blockList[0].GetComponent<MeshRenderer>().material = mat[levelManager.GetCurrentTurn()+1];

            }
            // B�� ���� ���
            else if(-3.51<=x && x<3.51 && 9.8 <= y)
            {
                Debug.Log("B�� ����");
                blockList[1].GetComponent<MeshRenderer>().material = mat[levelManager.GetCurrentTurn() + 1];
            }
            // C�� ���� ���
            else if(3.51<=x && 9.8 <= y)
            {
                Debug.Log("C�� ����");
                blockList[2].GetComponent<MeshRenderer>().material = mat[levelManager.GetCurrentTurn() + 1];
            }
            // D�� ���� ���
            else if(x < -3.51 && 4.8<=y && y<9.8)
            {
                Debug.Log("D�� ����");
                blockList[3].GetComponent<MeshRenderer>().material = mat[levelManager.GetCurrentTurn() + 1];
            }
            // E�� ���� ���
            else if(-3.51 <= x && x < 3.51 && 4.8 <= y && y < 9.8)
            {
                Debug.Log("E�� ����");
                blockList[4].GetComponent<MeshRenderer>().material = mat[levelManager.GetCurrentTurn() + 1];
            }
            // F�� ���� ���
            else if (3.51<=x && 4.8 <= y && y < 9.8)
            {
                Debug.Log("F�� ����");
                blockList[5].GetComponent<MeshRenderer>().material = mat[levelManager.GetCurrentTurn() + 1];
            }
            // G�� ���� ���
            else if (x < -3.51 && y<4.8)
            {
                Debug.Log("G�� ����");
                blockList[6].GetComponent<MeshRenderer>().material = mat[levelManager.GetCurrentTurn() + 1];
            }
            // H�� ���� ���
            else if (-3.51 <= x && x < 3.51 && y < 4.8)
            {
                Debug.Log("H�� ����");
                blockList[7].GetComponent<MeshRenderer>().material = mat[levelManager.GetCurrentTurn() + 1];
            }
            // I�� ���� ���
            else if (3.51 <= x && y < 4.8)
            {
                Debug.Log("I�� ����");
                blockList[8].GetComponent<MeshRenderer>().material = mat[levelManager.GetCurrentTurn() + 1];
            }
        }
    }
}

