using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Ball : MonoBehaviour
{
    private float maxVelocity = 9f;
    
    private Rigidbody rb;

    private int groundHitCount;
    public int groundHit { get { return groundHitCount; } set { groundHitCount += value; } }

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
            AudioSource audioSource = collision.gameObject.GetComponent<AudioSource>();
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
                GameObject.Find("RightHand Controller").GetComponent<XRController>().SendHapticImpulse(0.4f, 0.2f);
            }
        }else if (collision.gameObject.tag == "Ground")
        {
            // ¹Ù´Ú¿¡ Æ¨±ä È½¼ö Áõ°¡
            groundHit = 1;
        }
    }
}
