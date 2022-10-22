using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Racket : MonoBehaviourPun
{
    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = 1f;
            gameObject.transform.position = Camera.main.ScreenToWorldPoint(mousePosition);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.AddForce(gameObject.transform.forward * 5.0f, ForceMode.Impulse);
    }
}
