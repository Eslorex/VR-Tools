using UnityEngine;

public class ColliderUpdater : MonoBehaviour
{
    public Camera VRCamera;
    public LayerMask groundLayer; 
    public float maxColliderHeight = 2f; 
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        capsuleCollider = this.gameObject.AddComponent<CapsuleCollider>(); // Add a CapsuleCollider to this gameObject
        capsuleCollider.radius = 0.3f; // Set a suitable radius for your needs
    }

    void Update()
    {
        UpdateCollider();
    }

    void UpdateCollider()
    {
        Ray ray = new Ray(VRCamera.transform.position, Vector3.down);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundLayer))
        {
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green); // Draw the ray in green
            Debug.Log("Raycast hit point: " + hit.point);

            float cameraToGroundDistance = Vector3.Distance(VRCamera.transform.position, hit.point);

            // If the distance to the ground is greater than the max allowed height, limit the height
            float colliderHeight = Mathf.Min(cameraToGroundDistance, maxColliderHeight);

            // Calculate the position for the center of the collider
            Vector3 colliderCenter = VRCamera.transform.position - (Vector3.up * colliderHeight * 0.5f);
            capsuleCollider.center = this.transform.InverseTransformPoint(colliderCenter);

            // Set the height of the collider
            capsuleCollider.height = colliderHeight;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red); // Draw the ray in red
            Debug.Log("No ground detected beneath the camera");
        }
    }



}
