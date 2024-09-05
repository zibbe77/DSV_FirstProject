using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovment : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    private float horizontalValue;
    private Rigidbody2D rgbd;
    private SpriteRenderer sprRender;
    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        sprRender = GetComponent<SpriteRenderer>();
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
    }
    void FixedUpdate()
    {
        rgbd.velocity = new Vector2(horizontalValue * moveSpeed, rgbd.velocity.y);
    }

    void FlipSprite(bool dir)
    {
        sprRender.flipX = dir;
    }
}
