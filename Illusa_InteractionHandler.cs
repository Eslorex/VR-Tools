using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Illusa_InteractionHandler
{
    public bool IsCompleted => isDone;
    public bool IsHolding => isHold;
    public bool IsMoving => isMov;

    private bool isDone = false;
    private bool isMov = false;
    private bool isHold = false;
    private float doneDuration = 0.3f;

    private Vector3 targetPosition;
    private Quaternion targetRotation;
    private XRGrabInteractable hitInteractable;
    private XRDirectInteractor interactor;

    public void SetInteractor(XRDirectInteractor newInteractor)
    {
        interactor = newInteractor;
    }

    public void SetInteractable(XRGrabInteractable newInteractable)
    {
        hitInteractable = newInteractable;
    }

    public void SetTargetPosition(Vector3 newPosition)
    {
        targetPosition = newPosition;
    }

    public void SetTargetRotation(Quaternion newRotation)
    {
        targetRotation = newRotation;
    }

    public void MoveAndRotate(GameObject obj, float curveFactor, float transitionTime)
    {
        CoroutineRunner.Instance.StartCoroutine(MoveAndRotateCoroutine(obj, curveFactor, transitionTime));
    }

    private void ResetRigidbody(Rigidbody rb)
    {
        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.inertiaTensorRotation = Quaternion.identity;
    }

    private IEnumerator MoveAndRotateCoroutine(GameObject obj, float curveFactor, float transitionTime)
    {
        InitializeMovement();

        Rigidbody rb = obj.GetComponent<Rigidbody>();
        ResetRigidbody(rb);

        Vector3 startPosition = obj.transform.position;
        Quaternion startRotation = obj.transform.rotation;
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            UpdateMovement(obj, curveFactor, startPosition, startRotation, elapsedTime, transitionTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        FinalizeMovement(obj);
    }

    private void InitializeMovement()
    {
        isDone = false;
        isMov = true;
    }

    private void UpdateMovement(GameObject obj, float curveFactor, Vector3 startPosition, Quaternion startRotation, float elapsedTime, float transitionTime)
    {
        float t = elapsedTime / transitionTime;
        Vector3 controlPoint = BezierCurve.CalculateControlPoint(startPosition, targetPosition, curveFactor);
        Vector3 bezierPosition = BezierCurve.CalculateBezierPoint(t, startPosition, controlPoint, targetPosition);
        Quaternion bezierRotation = Quaternion.Slerp(startRotation, targetRotation, t);

        obj.transform.position = bezierPosition;
        obj.transform.rotation = bezierRotation;
    }

    private void FinalizeMovement(GameObject obj)
    {
        obj.transform.position = targetPosition;
        obj.transform.rotation = targetRotation;
        isDone = true;
        Grab(hitInteractable);

        CoroutineRunner.Instance.StartCoroutine(DoneDuration());
    }

    private IEnumerator DoneDuration()
    {
        yield return new WaitForSeconds(doneDuration);
        isDone = false;
        isMov = false;
    }

    private void Grab(XRGrabInteractable interactable)
    {
        isHold = interactor.isPerformingManualInteraction;
        interactor.StartManualInteraction(interactable);
    }
}
