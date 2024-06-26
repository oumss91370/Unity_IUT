using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public CharacterController2D controller;
    public float runSpeed = 40f;
    public float climbSpeed = 5f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool crouch = false;
    private float originalGravity;
    public bool isClimbing = false;
    private Ladder currentLadder;

    private void Start()
    {
        Application.targetFrameRate = 60;
        originalGravity = rb.gravityScale;
    }

    void Update()
    {
        if (isClimbing)
        {
            rb.gravityScale = 0;
            verticalMove = Input.GetAxis("Vertical") * climbSpeed;
        }
        else
        {
            rb.gravityScale = originalGravity;
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

            if (Input.GetButtonDown("Crouch"))
            {
                crouch = true;
            }
            else if (Input.GetButtonUp("Crouch"))
            {
                crouch = false;
            }
        }
    }
    public void OnCrouching(bool isCrouching)
    {
        animator.SetBool("isCrounch", isCrouching);
    }

    void FixedUpdate()
    {
        if (isClimbing)
        {
            rb.velocity = new Vector2(rb.velocity.x, verticalMove);
        }
        else
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
            jump = false;
        }

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }

    public void StartClimbing()
    {
        if (currentLadder != null)
        {
            isClimbing = true;
        }
    }

    public void StopClimbing()
    {
        isClimbing = false;
    }

    public void SetCurrentLadder(Ladder ladder)
    {
        currentLadder = ladder;
    }
}