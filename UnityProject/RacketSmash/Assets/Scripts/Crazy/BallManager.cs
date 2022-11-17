using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{
    public GameObject[] ballPoint;
    public GameObject playerPoint;
    public GameObject ballPrefab;

    private float time;
    private float magnitude = 5f;

    private void Start()
    {
        time = Time.time;
    }

    private void Update()
    {
        if (Time.time - time >= 1.5f)
        {
            time = Time.time;
            int index = Random.Range(0, 12);
            GameObject ball = Instantiate(ballPrefab, ballPoint[index].transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody>().velocity = (playerPoint.transform.position - ballPoint[index].transform.position);
            Destroy(ball, 3f);
        }
    }
}
