using System.Collections;
using UnityEngine;

public class PushableBridge : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float pushDistance = 1f;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;
    [SerializeField] private float fallDuration = 1.5f;
    [SerializeField] private AnimationCurve fallCurve;
    [SerializeField] private float promptRange = 2f;
    
    [Header("References")]
    [SerializeField] private GameObject interactionPrompt;
    [SerializeField] private Collider2D bridgeCollider;
    [SerializeField] private AudioClip interactionSound;
    [SerializeField] private Transform pivotPoint;

    private Transform player;
    private bool isInRange = false;
    private bool isPushed = false;
    
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private float objectHeight;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        if (fallCurve == null || fallCurve.length == 0)
        {
            fallCurve = new AnimationCurve(
                new Keyframe(0, 0, 0, 0),
                new Keyframe(0.3f, 0.2f, 1, 1),
                new Keyframe(1, 1, 0, 0)
            );
        }
        
        originalPosition = transform.position;
        originalRotation = transform.rotation;
        objectHeight = GetComponent<SpriteRenderer>().bounds.size.y;
        
        if (pivotPoint == null)
        {
            pivotPoint = new GameObject("PivotPoint").transform;
            pivotPoint.SetParent(transform);
            pivotPoint.localPosition = new Vector3(0, -0.5f, 0);
        }
        
        if (bridgeCollider != null) bridgeCollider.enabled = false;
    }

    void Update()
    {
        if (isPushed) return;

        float distance = Vector3.Distance(transform.position, player.position);
        bool nowInRange = distance <= pushDistance;

        if (nowInRange != isInRange)
        {
            isInRange = nowInRange;
        }
        
        if (distance <= promptRange)
        { 
            interactionPrompt.SetActive(false);
        }
        else
        {
            interactionPrompt.SetActive(true);
        }

        if (isInRange && Input.GetKeyDown(interactionKey))
        {
            PushBridge();
        }
    }

    void PushBridge()
    {
        isPushed = true;
        if (interactionPrompt != null) interactionPrompt.SetActive(false);

        if (interactionSound != null)
            AudioSource.PlayClipAtPoint(interactionSound, transform.position);

        StartCoroutine(FallBridgeAnimation());
    }

    IEnumerator FallBridgeAnimation()
    {
        float elapsed = 0f;
        Vector3 pivotWorldPosition = pivotPoint.position;
        
        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;

        while (elapsed < fallDuration)
        {
            float t = elapsed / fallDuration;
            float curveValue = fallCurve.Evaluate(t);
            float currentAngle = -90 * curveValue;

            Quaternion rotation = Quaternion.Euler(0, 0, currentAngle);
            
            Vector3 directionFromPivot = initialPosition - pivotWorldPosition;
            Vector3 rotatedDirection = rotation * directionFromPivot;
            transform.position = pivotWorldPosition + rotatedDirection;
            
            transform.rotation = initialRotation * rotation;
            
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        float finalAngle = -90f;
        Quaternion finalRotation = Quaternion.Euler(0, 0, finalAngle);
        Vector3 finalDirection = finalRotation * (initialPosition - pivotWorldPosition);
        transform.position = pivotWorldPosition + finalDirection;
        transform.rotation = initialRotation * finalRotation;
        
        float halfHeight = objectHeight * 0.5f;
        transform.position = new Vector3(
            transform.position.x,
            pivotWorldPosition.y,
            transform.position.z
        );
        
        if (bridgeCollider != null) bridgeCollider.enabled = true;
        
        if (interactionPrompt != null)
            interactionPrompt.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, pushDistance);
        
        if (pivotPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(pivotPoint.position, 0.1f);
        }
    }
}