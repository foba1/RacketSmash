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
        private int[,] gameBoard = new int[3, 3] { {0, 0, 0}, { 0, 0, 0 }, { 0, 0, 0 } };
        private int winner;
 
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
                winner = checkIsGameEnd();
            }
        }

        protected virtual void OnBallCollision(BallBase ball)
        {
            Debug.Log(levelManager.GetCurrentTurn());
            int player = levelManager.GetCurrentTurn() + 1;
            float x = ball.transform.position.x;
            float y = ball.transform.position.y;
            float z = ball.transform.position.z;
            // A�� ���� ���
            if(x < -3.51 && 9.8<=y)
            {
                Debug.Log("A�� ����");
                gameBoard[0,0] = player;
                blockList[0].GetComponent<MeshRenderer>().material = mat[player];
            }
            // B�� ���� ���
            else if(-3.51<=x && x<3.51 && 9.8 <= y)
            {
                Debug.Log("B�� ����");
                gameBoard[0, 1] = player;
                blockList[1].GetComponent<MeshRenderer>().material = mat[player];
            }
            // C�� ���� ���
            else if(3.51<=x && 9.8 <= y)
            {
                Debug.Log("C�� ����");
                gameBoard[0, 2] = player;
                blockList[2].GetComponent<MeshRenderer>().material = mat[player];
            }
            // D�� ���� ���
            else if(x < -3.51 && 4.8<=y && y<9.8)
            {
                Debug.Log("D�� ����");
                gameBoard[1, 0] = player;
                blockList[3].GetComponent<MeshRenderer>().material = mat[player];
            }
            // E�� ���� ���
            else if(-3.51 <= x && x < 3.51 && 4.8 <= y && y < 9.8)
            {
                Debug.Log("E�� ����");
                gameBoard[1, 1] = player;
                blockList[4].GetComponent<MeshRenderer>().material = mat[player];
            }
            // F�� ���� ���
            else if (3.51<=x && 4.8 <= y && y < 9.8)
            {
                Debug.Log("F�� ����");
                gameBoard[1, 2] = player;
                blockList[5].GetComponent<MeshRenderer>().material = mat[player];
            }
            // G�� ���� ���
            else if (x < -3.51 && y<4.8)
            {
                Debug.Log("G�� ����");
                gameBoard[2, 0] = player;
                blockList[6].GetComponent<MeshRenderer>().material = mat[player];
            }
            // H�� ���� ���
            else if (-3.51 <= x && x < 3.51 && y < 4.8)
            {
                Debug.Log("H�� ����");
                gameBoard[2, 1] = player;
                blockList[7].GetComponent<MeshRenderer>().material = mat[player];
            }
            // I�� ���� ���
            else if (3.51 <= x && y < 4.8)
            {
                Debug.Log("I�� ����");
                gameBoard[2, 2] = player;
                blockList[8].GetComponent<MeshRenderer>().material = mat[player];
            }
        }

        private int checkIsGameEnd()
        {
           for(int i=0; i<3; i++)
            {
                // ���� üũ
                if(gameBoard[i,0]==gameBoard[i,1] && gameBoard[i,1]==gameBoard[i,2] && gameBoard[i, 0] != 0)
                {
                    return gameBoard[i, 0];
                }
                // ���� üũ
                if (gameBoard[0,i]==gameBoard[1,i] && gameBoard[1, i]==gameBoard[2, i]&& gameBoard[0, i] != 0)
                {
                    return gameBoard[0, i];
                }
                // \ üũ
                if(gameBoard[0,0]==gameBoard[1,1] && gameBoard[1,1]==gameBoard[2,2] && gameBoard[0, 0] != 0)
                {
                    return gameBoard[0, 0];
                }
                // / üũ
                if(gameBoard[0,2]==gameBoard[1,1] && gameBoard[1, 1] == gameBoard[2, 0] && gameBoard[0, 2] != 0)
                {
                    return gameBoard[0, 2];
                }
            }
            return 0;
        }

        public int getWinner()
        {
            return winner;
        }
    }
}

