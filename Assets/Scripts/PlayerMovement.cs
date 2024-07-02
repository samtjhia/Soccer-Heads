using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2;
    public float jumpForce;
    public float dashDistance = 2f;
    public float dashCooldown = 1f; // time before the player can dash again
    public float doubleTapTimeThreshold = 0.3f; //  time between key presses to be considered double tap
    private float lastDashTime;
    private KeyCode lastKeyPressed;

    public bool isGrounded = false;
    Rigidbody2D rb;
    Animator anim;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        lastDashTime = -dashCooldown; // iinitial dashtime
        lastKeyPressed = KeyCode.None;
    }

    void Update()
    {
        HandleMovementInput();
        HandleJumpInput();
        HandleDashInput();
        HandleKickInput();
    }

    private void HandleMovementInput()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput < 0)
        {
            MoveLeft();
        }
        else if (horizontalInput > 0)
        {
            MoveRight();
        }
        else
        {
            speed = 0;
            anim.SetFloat("speed", speed);
        }
    }

    private void MoveLeft()
    {
        speed = 2;
        anim.SetFloat("speed", speed);
        Vector2 targetPos = new Vector2(transform.position.x - 1, transform.position.y);
        transform.position = Vector2.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void MoveRight()
    {
        speed = 2;
        anim.SetFloat("speed", speed);
        Vector2 targetPos = new Vector2(transform.position.x + 1, transform.position.y);
        transform.position = Vector2.Lerp(transform.position, targetPos, speed * Time.deltaTime);
    }

    private void HandleJumpInput()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        isGrounded = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Field")
        {
            isGrounded = true;
        }
    }

    private void HandleDashInput()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            float currentTime = Time.time;

            if (currentTime - lastDashTime < dashCooldown && lastKeyPressed == (Input.GetKeyDown(KeyCode.A) ? KeyCode.A : KeyCode.D))
            {
                Dash();
                lastDashTime = -dashCooldown; // reset dash timer
            }
            else
            {
                lastDashTime = currentTime; // record press time
            }

            lastKeyPressed = (Input.GetKeyDown(KeyCode.A) ? KeyCode.A : KeyCode.D);
        }
    }

    private void Dash()
    {
        // check direction ofp playre
        float dashDirection = (lastKeyPressed == KeyCode.D) ? 1f : -1f;
        Vector2 dashVector = new Vector2(dashDirection * dashDistance, 0f);

        // apply dash force
        rb.velocity = dashVector;
    }

    private void HandleKickInput()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            anim.SetTrigger("kick");
        }
    }
}
