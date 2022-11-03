using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseSquash
{
    public class BallBase : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] int bounceGroundCount = 0;
        [SerializeField] new Rigidbody rigidbody;

        public Rigidbody Rigidbody { get { return rigidbody; } }

        protected virtual void OnGroundHit()
        {
            if (bounceGroundCount == 2)
                Debug.Log("Hitted ground twice");
        }
        public virtual void OnHittedByRacket(Vector3 hitVector)
        {
            rigidbody.velocity = hitVector;
            bounceGroundCount = 0;
        }
        private void OnCollisionEnter(Collision collision)
        {
            GroundBase ground = collision.gameObject.GetComponent<GroundBase>();
            if (ground != null)
            {
                bounceGroundCount++;
                OnGroundHit();
                ground.OnHittedByBall(this);
            }
        }
    }
}
