using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{
    public int selectedRacket;
    public float velocity;

    private Vector3 prevPosition;
    private GameObject rightController;

    private void Start()
    {
        rightController = GameObject.Find("RightHand Controller");
        selectedRacket = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == selectedRacket);
        }
    }

    private void FixedUpdate()
    {
        velocity = (transform.GetChild(selectedRacket).position - prevPosition).magnitude;

        if (transform.position != rightController.transform.position)
        {
            transform.GetChild(selectedRacket).GetComponent<Rigidbody>().MovePosition(rightController.transform.position);
        }
        if (transform.eulerAngles != rightController.transform.eulerAngles)
        {
            transform.GetChild(selectedRacket).GetComponent<Rigidbody>().MoveRotation(rightController.transform.rotation);
        }

        prevPosition = transform.GetChild(selectedRacket).position;
    }

    public void SelectRacket(int index)
    {
        if (index < 0 || index >= transform.childCount) return;

        selectedRacket = index;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == selectedRacket);
            transform.GetChild(i).transform.position = rightController.transform.position;
        }
    }
}
