using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static System.TimeZoneInfo;
using UnityEngine.XR.Interaction.Toolkit;

public class CameraRaycastGrab : MonoBehaviour
{
    public bool isMoving;
    public bool isCompleted;
    public float curveFactor;
    public float transitionTime;
    public Illusa_InteractionHandler handler = new Illusa_InteractionHandler();
    // Start is called before the first frame update
    public Camera camera;

    void Start()
    {
        
    }
    public void MoveInteractableFromActiveCameraToRaycastHit()
    {
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100f))
        {
            StartCoroutine(ShowRay(ray, 5f, hit.point)); // Start the ShowRay coroutine with the hit position
            XRBaseInteractable hitInteractable = hit.transform.GetComponent<XRBaseInteractable>(); // Get the XRBaseInteractable of the hit object
            
            if (hitInteractable && !isMoving) // If we hit an interactable (object that has XRBaseInteractable) and if it's not moving
            {
                Debug.Log("Interactor name : " + gameObject.name);
                Debug.Log("Interactable name: " + hitInteractable);
                DistanceInfo distanceInfo = hitInteractable.GetDistance(camera.transform.position); // myInteractable is your instance of the Interactable class
                float distance = Mathf.Sqrt(distanceInfo.distanceSqr);
                Debug.Log("Distance: " + distance);
                Debug.Log("Interactable distance to Interactor: " + distance);
                handler.SetTargetPosition(camera.transform.position); // Set the target position
                handler.SetTargetRotation(camera.transform.rotation); // Set the target rotation

                handler.MoveAndRotate(hitInteractable.gameObject, curveFactor, transitionTime);
            }
        }
    }


    IEnumerator ShowRay(Ray ray, float duration, Vector3 hitPosition)
    {
        float endTime = Time.time + duration;

        while (Time.time <= endTime)
        {
            float distance = Vector3.Distance(ray.origin, hitPosition);
            Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);
            yield return null;  // Wait for next frame
        }

        Debug.DrawRay(ray.origin, ray.direction * 0f, Color.clear);
    }
    // Update is called once per frame
    void Update()
    {
        isMoving = handler.isMoving();
        isCompleted = handler.isCompleted();
        handler.SetTargetPosition(camera.transform.position); // Set the target position
        handler.SetTargetRotation(camera.transform.rotation);
        if (Input.GetMouseButtonDown(0))
        {
            MoveInteractableFromActiveCameraToRaycastHit();
        }
    }
}
