using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BaseSquash
{
    public class RacketBase : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] protected float HitPower = 40f;
        [SerializeField] protected Vector3 HitDirection = new Vector3(0, 1, 1);
        [SerializeField] float swingDuration = 1f;

        [Header("References")]
        [SerializeField] Collider collider;

        protected Collider Collider { get { return collider; } }
        public bool IsCurrentTurn
        {
            get { return isCurrentTurn; }
            set
            {
                if (isCurrentTurn && !value)
                    OnTurnEnd();
                else if (!isCurrentTurn && value)
                    OnTurnStart();
                isCurrentTurn = value;
            }
        }
        bool isCurrentTurn = false;
        protected bool IsSwinging { get; set; } = false;
        protected bool HasHittedBall { get; set; } = false;

        protected virtual void OnTurnStart()
        {
            collider.isTrigger = false;
        }
        protected virtual void OnTurnEnd()
        {
            collider.isTrigger = true;
        }
        private void OnCollisionEnter(Collision collision)
        {
            BallBase ball = collision.gameObject.GetComponent<BallBase>();
            if (ball != null)
            {
                OnBallCollision(ball);
            }
        }
        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                StartSwing();
            }
        }
        void StartSwing()
        {
            IsSwinging = true;
            HasHittedBall = false;
            OnStartSwing();
        }
        public void StopSwing()
        {
            IsSwinging = false;
            OnStopSwing();
        }
        //Call for starting animation or sound
        public virtual void OnStartSwing()
        {

        }
        //Call for starting animation or sound
        public virtual void OnStopSwing()
        {
            
        }
        
        protected virtual void OnBallCollision(BallBase ball)
        {
            if(IsSwinging && isCurrentTurn && !HasHittedBall)
            {
                ball.OnHittedByRacket(HitDirection.normalized * HitPower);
                HasHittedBall = true;
            }
        }
    }
}
