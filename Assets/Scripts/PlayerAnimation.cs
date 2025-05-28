using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        // get inputs
        float moveHorizontal = Input.GetAxis("Horizontal");
        

        if (rb.linearVelocity[1] > 0.00f || rb.linearVelocity[1] < -0.00f)
        { 
            anim.SetBool("isJumping", true);
        }
        if(rb.linearVelocity[1] == 0.00f)
        {
            anim.SetBool("isJumping", false);
        }
        if (rb.linearVelocity[0] > 0.00f || rb.linearVelocity[0] < -0.00f)
        {
            if (moveHorizontal > 0.25 || moveHorizontal < -0.25)
            {
                anim.SetBool("isStopping", false);
                anim.SetBool("isRunning", true);
            }
            else
            {
                anim.SetBool("isStopping", true);
                anim.SetBool("isRunning", false);
            }
        }
        else
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isStopping", false);
            anim.SetBool("isRunning", false);
        }
    }
}
