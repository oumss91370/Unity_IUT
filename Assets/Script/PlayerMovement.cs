using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    float verticalMove = 0f;
    bool jump = false;
    bool crouch = false;
    public bool isClimbing = false;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
        verticalMove = Input.GetAxisRaw("Vertical") * runSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            Debug.Log("Jump");
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

    void FixedUpdate()
    {
        if (isClimbing)
        {
            controller.Climb(verticalMove * Time.fixedDeltaTime / 2);
        }
        else
        {
            controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        }

        jump = false;
        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", characterVelocity);
        animator.SetBool("isClimbing", isClimbing);
    }
}
