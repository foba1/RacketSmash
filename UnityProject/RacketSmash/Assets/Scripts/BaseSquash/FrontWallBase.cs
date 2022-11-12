using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseSquash
{
    public class FrontWallBase : MonoBehaviour
    {
        [SerializeField] LevelManagerBase levelManager;

        protected LevelManagerBase LevelManager { get { return levelManager; } }
        private void OnCollisionEnter(Collision collision)
        {
            BallBase ball = collision.gameObject.GetComponent<BallBase>();
            if(ball != null)
            {
                OnBallCollision(ball);
                levelManager.OnBallHitWall();
            }
        }

        protected virtual void OnBallCollision(BallBase ball)
        {
        }
    }
}
