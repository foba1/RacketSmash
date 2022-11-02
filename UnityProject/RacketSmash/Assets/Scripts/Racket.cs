using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    public Vector3 velocity;
    private Vector3 prevPosition = new Vector3(0f, 0f, 0f);

    private void FixedUpdate()
    {
        if (prevPosition != transform.position)
        {
            velocity = transform.position - prevPosition;
        }

        prevPosition = transform.position;
    }
}
