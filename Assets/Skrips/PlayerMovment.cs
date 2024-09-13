using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] float jumpForce = 300f;
    [SerializeField] Transform leftfoot, rightFoot;
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] Transform spawnPos;
    [SerializeField] Slider hpSlider;
    [SerializeField] Image fillColor;
    [SerializeField] TMP_Text appleText;

    bool isGrounded;
    float rayDistance = 0.25f;

    public int dirNum = 0;
    public bool hasPineApple = false;

    int playerStartHp = 5;
    int currenthp = 0;

    public int applesCollected = 0;

    bool canMove = true;

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
        updateHpSlider();
        uppdateApples();
    }

    // Update is called once per frame

    void Update()
    {
        horizontalValue = Input.GetAxis("Horizontal");

        if (horizontalValue < 0)
        {
            dirNum = 1;
            FlipSprite(true);
        }

        if (horizontalValue > 0)
        {
            dirNum = 0;
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
        if (canMove == false)
        {
            return;
        }

        rgbd.velocity = new Vector2(horizontalValue * moveSpeed, rgbd.velocity.y);
        Debug.DrawRay(rightFoot.position, Vector2.down * rayDistance, Color.red, 0.25f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Apple"))
        {
            Destroy(other.gameObject);
            applesCollected++;
            uppdateApples();
        }

        if (other.CompareTag("HpBack"))
        {
            RestoreHp(other.gameObject);
        }
    }

    void RestoreHp(GameObject hpBack)
    {
        if (currenthp >= playerStartHp)
        {
            return;
        }
        else
        {
            currenthp += hpBack.GetComponent<HpPickUp>().HpRestored;
            updateHpSlider();
            Destroy(hpBack);
        }
    }

    void uppdateApples()
    {
        appleText.text = applesCollected.ToString();
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
        updateHpSlider();

        if (currenthp <= 0)
        {
            Respawn();
        }
    }

    void Respawn()
    {
        currenthp = playerStartHp;
        updateHpSlider();

        transform.position = spawnPos.position;
        rgbd.velocity = Vector2.zero;
    }

    void updateHpSlider()
    {
        hpSlider.value = currenthp;

        if (currenthp >= 2)
        {
            fillColor.color = Color.red;
        }
        else
        {
            fillColor.color = Color.magenta;
        }
    }

    public void TakeKnockBack(float knockbackForceSide, float knockbackForceUp)
    {
        canMove = false;
        rgbd.AddForce(new Vector2(knockbackForceSide, knockbackForceUp));
        Invoke("CanMoveAgain", 0.25f);
    }

    void CanMoveAgain()
    {
        canMove = true;
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
