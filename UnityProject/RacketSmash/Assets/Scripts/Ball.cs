using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float maxVelocity = 10f;

    private Rigidbody rb;

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
}
