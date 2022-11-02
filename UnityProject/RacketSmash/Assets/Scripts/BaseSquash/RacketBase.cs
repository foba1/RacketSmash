using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseSquash
{
    public class RacketBase : MonoBehaviour
    {
        [SerializeField] protected float HitPower = 40f;
        private void OnCollisionEnter(Collision collision)
        {
            BallBase ball = collision.gameObject.GetComponent<BallBase>();
            if (ball != null)
            {
                OnBallCollision(ball);
            }
        }

        protected virtual void OnBallCollision(BallBase ball)
        {
            Debug.Log(ball.transform.position);
        }
    }
}
