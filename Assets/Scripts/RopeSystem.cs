using UnityEngine;

public class RopeSystem : MonoBehaviour
{
    [Header("Rope Settings")]
    [SerializeField] private Animator ropeAnimator;
    [SerializeField] private float swingSpeed = 1.5f;
    [SerializeField] private KeyCode releaseKey = KeyCode.Space;
    [SerializeField] private float attachOffsetY = 0.5f;

    [Header("Release Settings")]
    [SerializeField] private float releaseForce = 10f;
    [SerializeField] private float completeAnimationTime = 0.5f;
    [SerializeField] private string animParamCompleted = "Completed";
    
    private bool isAttached;
    private float originalGravityScale;
    private bool originalKinematic;
    private float originalDrag;
    private RigidbodyConstraints2D originalConstraints;

    private void Start()
    {
        if (ropeAnimator != null)
        {
            ropeAnimator.enabled = false;
            ropeAnimator.SetBool(animParamCompleted, false);
        }
    }

    private void Update()
    {
        if (isAttached && Input.GetKeyDown(releaseKey))
        {
            StartCoroutine(DetachFromRope());
        }
    }

    private void FixedUpdate()
    {
        if (isAttached)
        {
            Vector3 targetPosition = ropeAnimator.transform.position + Vector3.down * attachOffsetY;
            transform.position = Vector3.Lerp(transform.position, targetPosition, 0.1f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Rope") && !isAttached)
        {
            AttachToRope(other.GetComponent<Animator>());
        }
    }

    private void AttachToRope(Animator animator)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        originalGravityScale = rb.gravityScale;
        originalKinematic = rb.isKinematic;
        originalDrag = rb.linearDamping;
        originalConstraints = rb.constraints;

        rb.gravityScale = 0f;
        rb.isKinematic = true;
        rb.linearDamping = 0f;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        ropeAnimator = animator;
        isAttached = true;

        ropeAnimator.enabled = true;
        ropeAnimator.Play("RopeSwing", 0, 0f);
        ropeAnimator.speed = swingSpeed;
        ropeAnimator.SetBool(animParamCompleted, false);
    }

    private System.Collections.IEnumerator DetachFromRope()
    {
        isAttached = false;
        ropeAnimator.SetBool(animParamCompleted, true);

        yield return new WaitForSeconds(completeAnimationTime);
        
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = originalGravityScale;
        rb.isKinematic = originalKinematic;
        rb.linearDamping = originalDrag;
        rb.constraints = originalConstraints;

        Vector2 releaseDirection = (transform.position - ropeAnimator.transform.position).normalized;
        rb.AddForce(releaseDirection * releaseForce, ForceMode2D.Impulse);

        ropeAnimator.enabled = false;
        ropeAnimator.SetBool(animParamCompleted, false);
    }
}