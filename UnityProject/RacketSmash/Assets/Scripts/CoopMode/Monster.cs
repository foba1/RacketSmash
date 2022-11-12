using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Coop
{

    public class Monster : MonoBehaviour
    {
        public enum State { Spawning, Moving, Dead}

        [SerializeField] float moveSpeed;
        [SerializeField] float idleRange;
        [SerializeField] float idleSpeed;

        [SerializeField] float fallSpeed;
        [SerializeField] float hitRadius;

        public State CurrentState { get; private set; } = State.Spawning;
        public float HitRadius { get { return hitRadius; } }

        public void SetStartPosition(Vector3 startPosition)
        {
            CurrentState = State.Spawning;
            StartCoroutine(EntranceCoroutine(startPosition));
        }
        IEnumerator EntranceCoroutine(Vector3 startPosition)
        {
            while(transform.position != startPosition)
            {
                transform.position = Vector3.MoveTowards(transform.position, startPosition, moveSpeed * Time.deltaTime);
                yield return null;
            }
            float eTime = 0f;
            while (CurrentState == State.Spawning)
            {
                float y = Mathf.Sin(eTime * Mathf.PI * idleSpeed) * idleRange;
                transform.position = startPosition + new Vector3(0, y, 0);
                eTime += Time.deltaTime;
                yield return null;
            }
        }
        public void StartFalling()
        {
            CurrentState = State.Moving;
        }
        public void OnHitted(Vector2 ballPos)
        {
            CurrentState = State.Dead;
            Debug.Log("Monser Hit!");
        }
        private void OnDrawGizmos()
        {

            Gizmos.DrawWireSphere(transform.position, hitRadius);
        }

        void Update()
        {
            if(CurrentState == State.Moving)
                transform.position += new Vector3(0, -1, 0) * fallSpeed * Time.deltaTime;

        }
    }
}