using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using Photon.Pun;

public class VRTest : MonoBehaviourPun
{
    public GameObject leftControllerObject;
    public GameObject rightControllerObject;

    private GameObject ball;
    private Vector3 prevPosition;

    private List<InputDevice> leftDevices;
    private List<InputDevice> rightDevices;
    private InputDevice leftController;
    private InputDevice rightController;
    

    private void Start()
    {
        leftDevices = new List<InputDevice>();
        rightDevices = new List<InputDevice>();
        GetController();

        ball = PhotonNetwork.Instantiate("Ball", new Vector3(0.5f, 1f, -2f), Quaternion.identity);
        prevPosition = new Vector3(0.5f, 1f, -2f);
    }

    private void Update()
    {
        /**
         * Boolean: 버튼 누른 여부
         * Float: Axis Range 0 ~ 1
         * Vector2: Touchpad 움직임 -1 ~ 1
         */
        if (!leftController.isValid || !rightController.isValid)
        {
            GetController();
        }

        leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftPrimaryButtonValue);
        if (leftPrimaryButtonValue)
        {
            Debug.Log("Pressing left primary button");
            ball.transform.position = leftControllerObject.transform.position;
            prevPosition = ball.transform.position;
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }

        leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool leftSecondaryButtonValue);
        if (leftSecondaryButtonValue)
            Debug.Log("Pressing left secondary button");

        leftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue);
        if (leftTriggerValue > 0.1F)
            Debug.Log("Left trigger pressed " + leftTriggerValue);

        leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftPrimary2DAxisValue);
        if (leftPrimary2DAxisValue != Vector2.zero)
            Debug.Log("Left primary touchpad " + leftPrimary2DAxisValue);

        rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButtonValue);
        if (rightPrimaryButtonValue)
            Debug.Log("Pressing right primary button");

        rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool rightSecondaryButtonValue);
        if (rightSecondaryButtonValue)
            Debug.Log("Pressing right secondary button");

        rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue);
        if (rightTriggerValue > 0.1F)
            Debug.Log("Right trigger pressed " + rightTriggerValue);


        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightPrimary2DAxisValue);
        if (rightPrimary2DAxisValue != Vector2.zero)
            Debug.Log("Right primary touchpad " + rightPrimary2DAxisValue);
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        if (prevPosition != ball.transform.position && Physics.Linecast(prevPosition, ball.transform.position, out hit))
        {
            if (hit.transform.tag == "Racket")
            {
                Vector3 positionOffset = hit.point - prevPosition;
                positionOffset = Vector3.Reflect(positionOffset, hit.normal);
                ball.transform.position = hit.point + positionOffset;

                Rigidbody rb = ball.GetComponent<Rigidbody>();
                rb.velocity = Vector3.Reflect(rb.velocity, hit.normal) + hit.transform.gameObject.GetComponent<Racket>().velocity;
            }
        }
        prevPosition = ball.transform.position;
    }

    private void GetController()
    {
        InputDeviceCharacteristics leftControllerCharacteristics =
            InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(leftControllerCharacteristics, leftDevices);
        InputDeviceCharacteristics rightControllerCharacteristics =
            InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(rightControllerCharacteristics, rightDevices);

        if (leftDevices.Count > 0)
        {
            leftController = leftDevices[0];
        }
        if (rightDevices.Count > 0)
        {
            rightController = rightDevices[0];
        }
    }
}
