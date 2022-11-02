using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseSquash
{
    public class FrontWallBase : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            BallBase ball = collision.gameObject.GetComponent<BallBase>();
            if(ball != null)
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
