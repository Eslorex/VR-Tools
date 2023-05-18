using UnityEngine;

public class ColliderUpdater : MonoBehaviour
{
    public Camera VRCamera;
    public LayerMask groundLayer; 
    public float maxColliderHeight = 2f; 
    private CapsuleCollider capsuleCollider;

    void Start()
    {
        capsuleCollider = this.gameObject.AddComponent<CapsuleCollider>(); 
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
            Debug.DrawRay(ray.origin, ray.direction * hit.distance, Color.green); 
            Debug.Log("Raycast hit point: " + hit.point);

            float cameraToGroundDistance = Vector3.Distance(VRCamera.transform.position, hit.point);
            float colliderHeight = Mathf.Min(cameraToGroundDistance, maxColliderHeight);
            Vector3 colliderCenter = VRCamera.transform.position - (Vector3.up * colliderHeight * 0.5f);
            capsuleCollider.center = this.transform.InverseTransformPoint(colliderCenter);

            // Set the height of the collider
            capsuleCollider.height = colliderHeight;
        }
        else
        {
            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red); 
            capsuleCollider.height = maxColliderHeight;
            Debug.Log("No ground detected beneath the camera");
        }
    }



}
