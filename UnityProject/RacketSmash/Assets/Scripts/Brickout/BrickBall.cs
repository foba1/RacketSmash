using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BrickBall : MonoBehaviour
{
    private float minVelocity = 5f;
    private GameObject player;
    private Rigidbody rb;
    private bool flag = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        player = GameObject.Find("UserPoint");
    }

    private void Update()
    {
        if (rb.velocity.magnitude < minVelocity)
        {
            rb.velocity *= minVelocity / rb.velocity.magnitude;
        }
        if (Vector3.Distance(transform.position,player.transform.position) <= 1f)
        {
            if (flag)
            {
                BrickGameManager.Instance.InitializeIsGettingOut();
            }
        }
        else
        {
            if (!flag) flag = true;
        }
    }
}
