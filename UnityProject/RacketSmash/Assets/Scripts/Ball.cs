using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball : MonoBehaviour
{
    public GameObject[] hitEffectPrefab;

    private float maxVelocity = 12f;
    private Rigidbody rb;
    private float prevCollisionTime = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity *= maxVelocity / rb.velocity.magnitude;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Racket")
        {
            Racket racket = collision.gameObject.GetComponent<Racket>();
            if (racket.velocity >= 0.015f && Time.time - prevCollisionTime >= 0.5f)
            {
                collision.gameObject.GetComponent<AudioSource>().Play();
                GameObject.Find("RightHand Controller").GetComponent<XRController>().SendHapticImpulse(0.4f, 0.2f);

                GameObject effect = Instantiate(hitEffectPrefab[racket.selectedRacket], transform.position, Quaternion.identity);
                Destroy(effect, 2f);

                prevCollisionTime = Time.time;
            }
        }
    }
}
