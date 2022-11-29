using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

using TMPro;

namespace SurvivalMode
{
    public class LevelManager : MonoBehaviour
    {
        [Header("Refs")]
        [SerializeField] Transform frontWall;
        [SerializeField] global::Ball ball;
        [Header("Monster")]
        [SerializeField] float yDistance;
        [SerializeField] float monsterSpawnInterval;
        [SerializeField] float rowSpawnInterval;
        [SerializeField] float dropWaitTime;
        [SerializeField] float dropInterval;
        [SerializeField] int monsterPerRow;
        [SerializeField] Monster monsterPrefab;

        [Header("Settings")]
        [SerializeField] int _life = 3;
        [SerializeField] int _score = 0;
        [SerializeField] List<string> monsterSpawnPatterns;

        int life { get { return _life;  } set { _life = value; lifeText.text = "Life : " + _life.ToString(); } }
        int score { get { return _score;  } set { _score = value; scoreText.text = "Score : " + _score.ToString(); } }

        [Header("UIs")]
        [SerializeField] TextMeshPro scoreText;
        [SerializeField] TextMeshPro lifeText;


        List<Monster> monsters = new List<Monster>();

        List<List<Monster>> rows = new List<List<Monster>>();

        int startXToggle = 1;
        public void Start()
        {
            StartCoroutine(SpawnCoroutine());
        }
        IEnumerator SpawnCoroutine()
        {
            while(true)
            {
                yield return SpawnRow(monsterPerRow);
                yield return SpawnRow(monsterPerRow);
                yield return SpawnRow(monsterPerRow);
                yield return new WaitForSeconds(dropWaitTime);
                yield return Drop();
                yield return Drop();
                yield return Drop();
                yield return new WaitForSeconds(rowSpawnInterval);

                foreach (List<Monster> row in rows.ToList())
                {
                    if (row.Count == 0)
                        rows.Remove(row);
                }

            }
            
        }
        IEnumerator Drop()
        {
            List<Monster> row = rows[rows.Count - 1];
            while(row.Count > 0)
            {
                int index = Random.Range(0, row.Count);
                row[index].StartFalling();
                row.RemoveAt(index);
                yield return new WaitForSeconds(dropInterval);
            }
        }
        IEnumerator SpawnRow(int monsterCount)
        {
            List<Monster> row = new List<Monster>();
            rows.Add(row);
            Vector3 wallScale = frontWall.localScale;
            float y = wallScale.y / 2 + frontWall.position.y - (rows.Count-1) * yDistance;
            float startX = startXToggle * wallScale.x / 2 + frontWall.position.x;

            for(int i=0; i<monsterCount; i++)
            {
                float x = -wallScale.x / 2 * startXToggle + frontWall.position.x;
                x += (i + 0.5f) * (wallScale.x / monsterCount) * startXToggle;
                row.Add(SpawnMonster(x, y, startX));
                yield return new WaitForSeconds(monsterSpawnInterval);
            }
            startXToggle *= -1;
        }
        Monster SpawnMonster(float x, float y, float startX)
        {
            float z = frontWall.position.z;
            Monster monster = Instantiate(monsterPrefab, new Vector3(startX, y, z), Quaternion.identity);
            monster.SetStartPosition(new Vector3(x, y, z));
            monsters.Add(monster);
            return monster;
        }

        private void Update()
        {
            foreach (Monster monster in monsters.ToList())
            {
                if (monster.transform.position.y + monster.transform.localScale.y/2 <= 0 || monster.CurrentState == Monster.State.Dead)
                {
                    if(monster.transform.position.y + monster.transform.localScale.y / 2 <= 0)
                    {
                        OnMonsterHitGround();
                    }
                    else
                    {
                        score += 100;
                    }
                    monsters.Remove(monster);
                    foreach(List<Monster> row in rows.ToList())
                    {
                        row.Remove(monster);
                        //if (row.Count == 0)
                            //rows.Remove(row);
                    }
                    Destroy(monster.gameObject);
                }
            }
        }
        public void OnBallBounceTwice()
        {
            life -= 1;
            if (life <= 0)
            {
                GameOver();
            }
        }
        void OnMonsterHitGround()
        {
            life -= 1;
            if (life <= 0)
            {
                GameOver();
            }
        }
        void GameOver()
        {
            Debug.Log("Game Over");
        }
    }

}
