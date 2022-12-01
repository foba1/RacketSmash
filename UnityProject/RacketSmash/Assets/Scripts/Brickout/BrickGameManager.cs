using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrickGameManager : MonoBehaviour
{
    [Header("Brick Destroy Effect")]
    [SerializeField] GameObject destroyEffectPrefab;

    public bool isGameFinished;

    private float speedTime;
    private float curSpeed;

    private float initSpeed = 1f;

    private static readonly float initLocalPosY = 2.5f;
    private static readonly float initHeight = 4f;

    static BrickGameManager instance;
    public static BrickGameManager Instance
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
        StartGame();
    }

    private void Update()
    {
        if (isGameFinished)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartGame();
            }
        }
    }

    public void StartGame()
    {
        isGameFinished = false;
        BrickManager.Instance.StartGame();

        speedTime = Time.time;
        curSpeed = initSpeed;
    }

    public void DestroyBrick(GameObject brick)
    {
        GameObject effect = Instantiate(destroyEffectPrefab, brick.transform.position, Quaternion.identity);
        Destroy(effect, 2f);
        BrickManager.Instance.DestroyBrick(brick);
        Destroy(brick);
    }

    IEnumerator DestroyBrickCoroutine(GameObject brick, float time)
    {
        yield return new WaitForSecondsRealtime(time);
        DestroyBrick(brick);
    }

    public void DestroyBrick(GameObject brick, float time)
    {
        StartCoroutine(DestroyBrickCoroutine(brick, time));
    }

    private void UpdateSpeed()
    {
        if (Time.time - speedTime >= 10f)
        {
            curSpeed += 0.5f;
            speedTime = Time.time;
        }
    }

    private void GameOver()
    {
        isGameFinished = true;
        BrickManager.Instance.GameOver();
    }
}
