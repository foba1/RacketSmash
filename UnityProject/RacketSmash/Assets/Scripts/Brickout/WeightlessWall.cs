using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightlessWall : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] Transform player;
    [SerializeField] float landZOffset = 1;
    [SerializeField] bool controlXAxis = false;

    [Header("Test Variables")]
    [SerializeField] Rigidbody ball;
    [SerializeField] Vector3 ballShootDir = new Vector3(0, 10, 10);

    private bool isGettingOut = false;

    private void OnDrawGizmos()
    {
        if (player != null)
        {
            Gizmos.DrawLine(player.transform.position + new Vector3(-1, -player.position.y + 0.05f, landZOffset),
                player.transform.position + new Vector3(1, -player.position.y + 0.05f, landZOffset));
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        isGettingOut = !isGettingOut;
    }

    private void OnTriggerExit(Collider collider)
    {
        if (isGettingOut)
        {
            Ball ball = collider.gameObject.GetComponent<Ball>();
            if (ball != null)
            {
                ShootBallToRacket(ball);
            }
        }
    }

    private void ShootBallToRacket(Ball ball)
    {
        Rigidbody ballRB = ball.gameObject.GetComponent<Rigidbody>();

        Vector3 collisionPoint = ball.transform.position;

        Vector3 collisionToPlayer = collisionPoint - player.transform.position;
        float xOffset = collisionToPlayer.x * landZOffset / collisionToPlayer.z;
        if (!controlXAxis)
            xOffset = 0;


        Vector3 targetPoint = player.transform.position + new Vector3(xOffset, 0, landZOffset);
        Vector3 directLine = targetPoint - collisionPoint;

        float v0 = -Mathf.Abs(directLine.y);

        ballRB.velocity = new Vector3(directLine.x, v0, directLine.z);
    }

    float SolveEquation(float a, float b, float c)
    {
        return (-b + Mathf.Sqrt(b * b - 4 * a * c)) / 2 * a;
    }

}
