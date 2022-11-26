using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball : MonoBehaviour
{
    [Header("EffectPrefab")]
    [SerializeField] GameObject[] hitEffectPrefab;

    [Header("CurrentMode")]
    [SerializeField] int curMode;

    [Header("Setting")]
    [SerializeField] float maxVelocity = 15f;

    private Rigidbody rb;
    private float prevCollisionTime = 0f;
    private GameObject leftControllerObject;
    private bool isStickedToController = false;
    public bool hitByRacket = false;
    private float[] amplitude = new float[5] { 0.5f, 0.38f, 0.44f, 0.6f, 0.45f };
    private float[] duration = new float[5] { 0.25f, 0.18f, 0.22f, 0.3f, 0.24f };

    [SerializeField] private int groundHitCount;
    public int groundHit { get { return groundHitCount; } set { groundHitCount += value; } }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        leftControllerObject = GameObject.Find("LeftHand Controller");
    }

    private void Update()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity *= maxVelocity / rb.velocity.magnitude;
        }
        if (isStickedToController)
        {
            transform.position = leftControllerObject.transform.position;
            rb.velocity = new Vector3(0f, 0f, 0f);
            rb.angularVelocity = new Vector3(0f, 0f, 0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Racket")
        {
            Racket racket = collision.transform.parent.GetComponent<Racket>();
            if (racket.velocity >= 0.015f && Time.time - prevCollisionTime >= 0.4f)
            {
                collision.transform.parent.GetComponent<AudioSource>().Play();
                GameObject.Find("RightHand Controller").GetComponent<XRController>().SendHapticImpulse(amplitude[racket.selectedRacket], duration[racket.selectedRacket]);

                GameObject effect = Instantiate(hitEffectPrefab[racket.selectedRacket], transform.position, Quaternion.identity);
                Destroy(effect, 2f);

                prevCollisionTime = Time.time;
            }
        }
        if (curMode == (int)Mode.mole)
        {
            if (collision.gameObject.tag == "Ground")
            {
                // �ٴڿ� ƨ�� Ƚ�� ����
                groundHit = 1;
            }
            else if (collision.gameObject.tag == "Mole")
            {
                // �δ��� Ÿ����
                AudioSource audioSource = collision.gameObject.GetComponent<AudioSource>();
                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }
            }
        }
        else if (curMode == (int)Mode.crazy)
        {
            if (collision.gameObject.tag == "Racket")
            {
                hitByRacket = true;
                Destroy(gameObject, 2f);
            }
            else if (collision.gameObject.tag == "Wall")
            {
                if (hitByRacket)
                {
                    Destroy(gameObject);
                }
            }
        }
        else if (curMode == (int)Mode.survival)
        {

        }
        else if (curMode == (int)Mode.brick)
        {

        }
    }

    public void RespawnOrDrop()
    {
        if (isStickedToController) isStickedToController = false;
        else if (leftControllerObject != null) isStickedToController = true;
    }

    public void SetCrazyMode()
    {
        curMode = (int)Mode.crazy;
    }

    public void OnDestroy()
    {
        if (curMode == (int)Mode.mole)
        {
            GameObject effect = Instantiate(CrazyManager.Instance.destroyEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 2f);
        }
    }
}
