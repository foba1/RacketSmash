using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBouncer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Rigidbody ball;
    [SerializeField] float landZOffset = 1;

    [SerializeField] Vector3 ballShootDir = new Vector3(0, 10, 10);

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
            Vector3 targetPoint = player.transform.position + new Vector3(0, 0, landZOffset);

            float y = targetPoint.z * Physics.gravity.y / collisionPoint.z - collisionPoint.y;

            float power = ballRB.velocity.magnitude;

            Vector3 directLine = targetPoint - collisionPoint;
            ballRB.velocity = new Vector3(directLine.x, y, directLine.z);
        }
    }

    float SolveEquation(float a, float b, float c)
    {
        return (-b + Mathf.Sqrt(b * b - 4 * a * c)) / 2 * a;
    }

}
