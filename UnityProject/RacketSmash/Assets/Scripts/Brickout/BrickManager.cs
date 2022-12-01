using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickManager : MonoBehaviour
{
    [Header("Brick Prefab")]
    [SerializeField] GameObject brickPrefab;

    private List<GameObject> brickList;

    private int maxSpawnCount = 7;
    private int spawnCount;


    static BrickManager instance;
    public static BrickManager Instance
    {
        get
        {
            if (!instance)
            {
                return null;
            }
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        brickList = new List<GameObject>();

    }

    public void StartGame()
    {
        GameObject ball = Instantiate(brickPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        brickList.Add(ball);
    }

    public void DestroyBrick(GameObject brick)
    {
        for (int i = 0; i < brickList.Count; i++)
        {
            if (brickList[i] == brick)
            {
                brickList.RemoveAt(i);
                return;
            }
        }
    }

    public void GameOver()
    {
        for (int i = 0; i < brickList.Count; i++)
        {
            Destroy(brickList[i]);
        }
        brickList.Clear();
    }
}
