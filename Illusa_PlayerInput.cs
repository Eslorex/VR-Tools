using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class Illusa_PlayerInput
{
    public XRNode leftHandNode = XRNode.LeftHand;
    public XRNode rightHandNode = XRNode.RightHand;
    
    private InputDevice GetDevice(XRNode node)
    {
        InputDevice device = InputDevices.GetDeviceAtXRNode(node);
        return device;
    }

    private bool GetButtonState(XRNode node, InputFeatureUsage<bool> button)
    {
        var device = GetDevice(node);
        if (device.isValid)
        {
            if (device.TryGetFeatureValue(button, out bool isPressed))
            {
                return isPressed;
            }
        }
        return false;
    }

    private float GetAxisValue(XRNode node, InputFeatureUsage<float> axis)
    {
        var device = GetDevice(node);
        if (device.isValid)
        {
            if (device.TryGetFeatureValue(axis, out float value))
            {
                return value;
            }
        }
        return 0f;
    }

    private Vector2 Get2DAxisValue(XRNode node, InputFeatureUsage<Vector2> axis)
    {
        var device = GetDevice(node);
        if (device.isValid)
        {
            if (device.TryGetFeatureValue(axis, out Vector2 value))
            {
                return value;
            }
        }
        return Vector2.zero;
    }

    public bool IsPrimaryButtonPressed(XRNode node)
    {
        return GetButtonState(node, CommonUsages.primaryButton);
    }

    public bool IsPrimaryButtonTouched(XRNode node)
    {
        return GetButtonState(node, CommonUsages.primaryTouch);
    }

    public bool IsSecondaryButtonPressed(XRNode node)
    {
        return GetButtonState(node, CommonUsages.secondaryButton);
    }

    public bool IsSecondaryButtonTouched(XRNode node)
    {
        return GetButtonState(node, CommonUsages.secondaryTouch);
    }

    public bool IsTriggerPressed(XRNode node)
    {
        return GetAxisValue(node, CommonUsages.trigger) > 0.5f;
    }

    public bool IsTriggerTouched(XRNode node)
    {
        return GetButtonState(node, CommonUsages.gripButton);
    }

    public float GetTriggerValue(XRNode node)
    {
        return GetAxisValue(node, CommonUsages.trigger);
    }

    public bool IsGripPressed(XRNode node)
    {
        return GetButtonState(node, CommonUsages.gripButton);
    }

    public float GetGripValue(XRNode node)
    {
        return GetAxisValue(node, CommonUsages.grip);
    }

    public bool IsThumbstickClicked(XRNode node)
    {
        return GetButtonState(node, CommonUsages.primary2DAxisClick);
    }

    public bool IsThumbstickTouched(XRNode node)
    {
        return GetButtonState(node, CommonUsages.primary2DAxisTouch);
    }

    public Vector2 GetThumbstickValue(XRNode node)
    {
        return Get2DAxisValue(node, CommonUsages.primary2DAxis);
    }

    public bool IsMenuPressed(XRNode node)
    {
        return GetButtonState(node, CommonUsages.menuButton);
    }
}
