using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBouncer : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Transform player;
    [SerializeField] float landZOffset = 1;
    [SerializeField] float landTime = 1;
    [SerializeField] bool controlXAxis = false;

    [Header("Test Variables")]
    [SerializeField] Rigidbody ball;
    [SerializeField] Vector3 ballShootDir = new Vector3(0, 10, 10);


    public Transform Player { get { return player; } set { player = value; } }
    private void OnDrawGizmos()
    {
        if(player != null)
        {
            Gizmos.DrawLine(player.transform.position + new Vector3(-1, -player.position.y + 0.05f, landZOffset),
                player.transform.position + new Vector3(1, -player.position.y + 0.05f, landZOffset));
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if (ball != null)
                ball.velocity = ballShootDir;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            Rigidbody ballRB = ball.gameObject.GetComponent<Rigidbody>();

            Vector3 collisionPoint = ball.transform.position;
            Vector3 collisionToPlayer = collisionPoint - player.transform.position;
            float xOffset = collisionToPlayer.x * landZOffset / collisionToPlayer.z;   
            if (!controlXAxis)
                xOffset = 0;

            
            Vector3 targetPoint = player.transform.position + new Vector3(xOffset, 0, landZOffset);
            Vector3 directLine = targetPoint - collisionPoint;

            //float v0 =  -Physics.gravity.y/2 - Mathf.Abs(directLine.y);
            float t = landTime;
            float v0 = -Physics.gravity.y/2 * t - Mathf.Abs(directLine.y) / t;


            ballRB.velocity = new Vector3(directLine.x / t , v0, directLine.z / t);
        }
    }

    float SolveEquation(float a, float b, float c)
    {
        return (-b + Mathf.Sqrt(b * b - 4 * a * c)) / 2 * a;
    }

}
