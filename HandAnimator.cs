using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class HandAnimator : MonoBehaviour
{
    public XRNode leftHandNode;
    public XRNode rightHandNode;
    private GameObject LeftHandClone;
    private GameObject RightHandClone;
    private Animator LeftHandAnimator;
    private Animator RightHandAnimator;
    private bool handInitalize = true;

    private Illusa_PlayerInput playerInput;

    // Start is called before the first frame update
    void Start()
    {
        playerInput = new Illusa_PlayerInput();
        leftHandNode = playerInput.leftHandNode;
        rightHandNode = playerInput.rightHandNode;
    }

    void InitializeHands()
    {
        if (handInitalize)
        {
            LeftHandClone = GameObject.Find("Left Hand Model(Clone)");
            LeftHandAnimator = LeftHandClone.GetComponent<Animator>();
            RightHandClone = GameObject.Find("Right Hand Model(Clone)");
            RightHandAnimator = RightHandClone.GetComponent<Animator>();
            handInitalize = false;
        }
    }

    void UpdateHandAnimation(XRNode handNode, Animator handAnimator)
    {
        float triggerValue = playerInput.GetTriggerValue(handNode);
        float gripValue = playerInput.GetGripValue(handNode);

        if (triggerValue > 0.1f)
        {
            handAnimator.SetFloat("Trigger", triggerValue);
        }
        else
        {
            handAnimator.SetFloat("Trigger", 0);
        }

        if (gripValue > 0.1f)
        {
            handAnimator.SetFloat("Grip", gripValue);
        }
        else
        {
            handAnimator.SetFloat("Grip", 0);
        }
    }

    void Update()
    {
        InitializeHands();

        if (LeftHandAnimator != null)
        {
            UpdateHandAnimation(leftHandNode, LeftHandAnimator);
        }

        if (RightHandAnimator != null)
        {
            UpdateHandAnimation(rightHandNode, RightHandAnimator);
        }
    }
}
