using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRTest : MonoBehaviour
{
    public GameObject originObject;
    public GameObject leftControllerObject;
    public GameObject rightControllerObject;

    private GameObject ball;

    private List<InputDevice> leftDevices;
    private List<InputDevice> rightDevices;
    private InputDevice leftController;
    private InputDevice rightController;

    private void Start()
    {
        leftDevices = new List<InputDevice>();
        rightDevices = new List<InputDevice>();
        GetController();

        ball = Instantiate(Resources.Load("Ball") as GameObject);
    }

    private void Update()
    {
        if (!leftController.isValid || !rightController.isValid)
        {
            GetController();
        }

        leftController.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftPrimaryButtonValue);
        if (leftPrimaryButtonValue)
        {
            //Debug.Log("Pressing left primary button");
            ball.transform.position = leftControllerObject.transform.position;
            ball.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        }

        leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool leftSecondaryButtonValue);
        if (leftSecondaryButtonValue)
        {
            //Debug.Log("Pressing left secondary button");
        }

        leftController.TryGetFeatureValue(CommonUsages.trigger, out float leftTriggerValue);
        if (leftTriggerValue > 0.1F)
        {
            //Debug.Log("Left trigger pressed " + leftTriggerValue);
        }

        leftController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 leftPrimary2DAxisValue);
        if (leftPrimary2DAxisValue != Vector2.zero)
        {
            //Debug.Log("Left primary touchpad " + leftPrimary2DAxisValue);
            originObject.transform.position += new Vector3(leftPrimary2DAxisValue.x / 30f, 0f, leftPrimary2DAxisValue.y / 30f);
        }

        rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButtonValue);
        if (rightPrimaryButtonValue)
        {
            //Debug.Log("Pressing right primary button");
        }

        rightController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool rightSecondaryButtonValue);
        if (rightSecondaryButtonValue)
        {
            //Debug.Log("Pressing right secondary button");
        }

        rightController.TryGetFeatureValue(CommonUsages.trigger, out float rightTriggerValue);
        if (rightTriggerValue > 0.1F)
        {
            //Debug.Log("Right trigger pressed " + rightTriggerValue);
        }

        rightController.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 rightPrimary2DAxisValue);
        if (rightPrimary2DAxisValue != Vector2.zero)
        {
            //Debug.Log("Right primary touchpad " + rightPrimary2DAxisValue);
        }
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
