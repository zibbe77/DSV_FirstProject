using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpForce = 300f;
    [SerializeField] Transform leftfoot, rightFoot;
    [SerializeField] LayerMask whatIsGround;
    bool isGrounded;
    float rayDistance = 0.25f;

    private float horizontalValue;
    private Rigidbody2D rgbd;
    private SpriteRenderer sprRender;
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        sprRender = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame

    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if (horizontalValue < 0)
        {
            FlipSprite(true);
        }

        if (horizontalValue > 0)
        {
            FlipSprite(false);
        }

        if (Input.GetButton("Jump") && CheckIfGrounded())
        {
            Jump();
        }

        anim.SetFloat("MoveSpeed", Mathf.Abs(rgbd.velocity.x));
        anim.SetFloat("VerticalSpeed", rgbd.velocity.y);
        anim.SetBool("IsGrounded", CheckIfGrounded());
    }
    void FixedUpdate()
    {
        rgbd.velocity = new Vector2(horizontalValue * moveSpeed, rgbd.velocity.y);
        Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.red, 0.25f);
    }

    void FlipSprite(bool dir)
    {
        sprRender.flipX = dir;
    }

    void Jump()
    {
        rgbd.AddForce(new Vector2(0, jumpForce));
    }

    bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftfoot.position, Vector2.down, rayDistance, whatIsGround);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, whatIsGround);

        if (leftHit.collider != null && leftHit.collider.CompareTag("Ground") || rightHit.collider != null && rightHit.collider.CompareTag("Ground"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
