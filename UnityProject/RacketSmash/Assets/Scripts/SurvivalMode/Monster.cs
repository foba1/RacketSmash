using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SurvivalMode
{

    public class Monster : MonoBehaviour
    {
        public enum State { Spawning, Moving, Dead}

        [SerializeField] int maxHp;
        int currentHp;
        [SerializeField] float moveSpeed;
        [SerializeField] float idleRange;
        [SerializeField] float idleSpeed;

        [SerializeField] float fallSpeed;

        [SerializeField] Material hurtMaterial;

        [SerializeField] GameObject hitEffect;
        [SerializeField] GameObject dieEffect;

        MeshRenderer renderer;

        public State CurrentState { get { return currentState; } private set { currentState = value; } }

        public SFXPlayer SFXPlayer { get; set; }

        [SerializeField] State currentState = State.Spawning;
        public void SetStartPosition(Vector3 startPosition)
        {
            CurrentState = State.Spawning;
            StartCoroutine(EntranceCoroutine(startPosition));
        }
        private void Awake()
        {
            currentHp = maxHp;
            renderer = GetComponent<MeshRenderer>();
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
        public void OnHitted()
        {
            currentHp -= 1;
            if (currentHp <= 0)
            {
                Instantiate(dieEffect, transform.position, Quaternion.identity);
                CurrentState = State.Dead;
                SFXPlayer.PlaySound("MonsterDead");
            }
            else
            {
                Instantiate(hitEffect, transform.position, Quaternion.identity);
                if (hurtMaterial != null)
                    renderer.material = hurtMaterial;
                SFXPlayer.PlaySound("MonsterHitted");
            }
            Debug.Log("Monser Hit!");
        }
        public void Stop()
        {
            GetComponent<Collider>().enabled = false;
            fallSpeed = 0f;
        }
        public void Kill()
        {
            StartCoroutine(Die());
        }
        IEnumerator Die()
        {
            float eTime = 0f;
            float duration = 0.5f;
            Vector3 originalScale = transform.localScale;
            while(eTime < duration)
            {
                eTime += Time.deltaTime;
                transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, eTime / duration);
                yield return null;
            }
            Destroy(gameObject);
        }
        void Update()
        {
            if(CurrentState == State.Moving)
                transform.position += new Vector3(0, -1, 0) * fallSpeed * Time.deltaTime;

        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<Ball>() != null)
            {
                OnHitted();
            }
        }
    }
}