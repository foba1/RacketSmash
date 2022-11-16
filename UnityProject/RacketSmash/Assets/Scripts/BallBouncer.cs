using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBouncer : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Rigidbody ball;
    [SerializeField] float landZOffset = 1;

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
            ball.velocity = new Vector3(0, 1, 1) * 10;
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

            Vector3 directLine = (targetPoint - collisionPoint);

            //S(t) = S0 + V0 * dt + G * dt^2
            //0 = Physics.gravity * t^2 + directLine * t + collisionPoint


            float estimatedTime = SolveEquation(Physics.gravity.y, directLine.y, collisionPoint.y);

            ballRB.velocity = (targetPoint - collisionPoint);

        }
    }

    float SolveEquation(float a, float b, float c)
    {
        return (-b + Mathf.Sqrt(b * b - 4 * a * c)) / 2 * a;
    }

}
