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

    private bool isGettingOut = true;

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
        Ball ball = collider.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            isGettingOut = !isGettingOut;
        }
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

        Vector3 collisionToPlayer = player.transform.position - collisionPoint;


        ballRB.velocity = collisionToPlayer;
    }
}
