using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRInteractionManager : MonoBehaviour
{
    private GameObject originObject;
    private GameObject leftControllerObject;
    private GameObject rightControllerObject;
    private GameObject racketObject;
    private List<InputDevice> leftDevices;
    private List<InputDevice> rightDevices;
    private InputDevice leftController;
    private InputDevice rightController;

    private bool prevLeftPrimaryButtonValue = false;
    private bool prevLeftSecondaryButtonValue = false;

    private void Start()
    {
        originObject = GameObject.Find("XR Origin");
        leftControllerObject = GameObject.Find("LeftHand Controller");
        rightControllerObject = GameObject.Find("RightHand Controller");

        leftDevices = new List<InputDevice>();
        rightDevices = new List<InputDevice>();
        GetController();

        racketObject = GameObject.Find("Racket");
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
            prevLeftPrimaryButtonValue = true;
        }
        else
        {
            if (prevLeftPrimaryButtonValue)
            {
                prevLeftPrimaryButtonValue = false;
                GameObject ball = GameObject.Find("Ball");
                if (ball != null) ball.GetComponent<Ball>().RespawnOrDrop();
            }
        }

        leftController.TryGetFeatureValue(CommonUsages.secondaryButton, out bool leftSecondaryButtonValue);
        if (leftSecondaryButtonValue)
        {
            //Debug.Log("Pressing left secondary button");
            prevLeftSecondaryButtonValue = true;
        }
        else
        {
            if (prevLeftSecondaryButtonValue)
            {
                prevLeftSecondaryButtonValue = false;
                Racket racket = racketObject.GetComponent<Racket>();
                int selectedRacket = racket.selectedRacket;
                if (selectedRacket >= racketObject.transform.childCount - 1) selectedRacket = 0;
                else selectedRacket++;
                racket.SelectRacket(selectedRacket);
            }
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