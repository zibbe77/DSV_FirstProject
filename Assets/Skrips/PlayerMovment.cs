using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpForce = 300f;
    [SerializeField] Transform leftfoot, rightFoot;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform spawnPos;
    [SerializeField] Slider hpSlider;
    bool isGrounded;
    float rayDistance = 0.25f;

    int playerStartHp = 5;
    int currenthp = 0;

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

        currenthp = playerStartHp;
        hpSlider.value = currenthp;
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

    public void TakeDamage(int dmg)
    {
        currenthp -= dmg;
        hpSlider.value = currenthp;

        if (currenthp <= 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        currenthp = playerStartHp;
        hpSlider.value = currenthp;

        transform.position = spawnPos.position;
        rgbd.velocity = Vector2.zero;
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
