using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class CameraRaycastGrab : MonoBehaviour
{
    public bool IsMoving => handler.isMoving();
    public bool IsCompleted => handler.isCompleted();
    public float CurveFactor;
    public float TransitionTime;
    public Camera Camera;
    
    private Illusa_InteractionHandler handler = new Illusa_InteractionHandler();

    void Update()
    {
        UpdateHandler();
        
        if (Input.GetMouseButtonDown(0))
        {
            ProcessMouseInput();
        }
    }

    private void UpdateHandler()
    {
        handler.SetTargetPosition(Camera.transform.position);
        handler.SetTargetRotation(Camera.transform.rotation);
    }

    private void ProcessMouseInput()
    {
        Ray ray = Camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f))
        {
            StartCoroutine(ShowRay(ray, 5f, hit.point));
            XRBaseInteractable hitInteractable = hit.transform.GetComponent<XRBaseInteractable>();

            if (hitInteractable && !IsMoving)
            {
                MoveAndRotateInteractable(hitInteractable);
            }
        }
    }

    private void MoveAndRotateInteractable(XRBaseInteractable interactable)
    {
        DebugInfo(interactable);
        DistanceInfo distanceInfo = interactable.GetDistance(Camera.transform.position);
        float distance = Mathf.Sqrt(distanceInfo.distanceSqr);
        handler.MoveAndRotate(interactable.gameObject, CurveFactor, TransitionTime);
    }

    private void DebugInfo(XRBaseInteractable interactable)
    {
        Debug.Log($"Interactor name: {gameObject.name}");
        Debug.Log($"Interactable name: {interactable}");
        DistanceInfo distanceInfo = interactable.GetDistance(Camera.transform.position);
        float distance = Mathf.Sqrt(distanceInfo.distanceSqr);
        Debug.Log($"Distance: {distance}");
    }

    private IEnumerator ShowRay(Ray ray, float duration, Vector3 hitPosition)
    {
        float endTime = Time.time + duration;
        while (Time.time <= endTime)
        {
            float distance = Vector3.Distance(ray.origin, hitPosition);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            yield return null;
        }
        Debug.DrawRay(ray.origin, ray.direction * 0f, Color.clear);
    }
}
