using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.2f;
    public Vector3 offset = new Vector3(0, 0, 2);
    public float lookAheadFactor = 2f;
    private Vector3 currentVelocity;
    
    void LateUpdate()
    {
        Vector3 targetDirection = (target.position - transform.position).normalized;
        Vector3 desiredPosition = target.position + offset + (targetDirection * lookAheadFactor);
    
        transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, smoothSpeed);
    }
}