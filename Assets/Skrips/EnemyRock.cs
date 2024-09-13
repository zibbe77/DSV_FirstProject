using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRock : MonoBehaviour
{
    [SerializeField]
    float moveSpeed = 2.0f;
    [SerializeField]
    float killBounc = 500;
    float DmgBounc = 100;
    [SerializeField] int dmg = 1;

    [SerializeField] float knockbackForceSide = 200f;
    [SerializeField] float knockbackForceUp = 100f;
    private SpriteRenderer sprRender;

    // Update is called once per frame

    void Start()
    {
        sprRender = GetComponent<SpriteRenderer>();
    }
    void FixedUpdate()
    {
        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);

        if (moveSpeed > 0)
        {
            FlipSprite(true);
        }

        if (moveSpeed < 0)
        {
            FlipSprite(false);
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed;
        }

        if (other.gameObject.CompareTag("Player"))
        {
            // Dmg
            other.gameObject.GetComponent<PlayerMovment>().TakeDamage(dmg);

            //KnockBack

            if (other.transform.position.x > transform.position.x)
            {
                other.gameObject.GetComponent<PlayerMovment>().TakeKnockBack(knockbackForceSide, knockbackForceUp);
            }
            else
            {
                other.gameObject.GetComponent<PlayerMovment>().TakeKnockBack(-knockbackForceSide, knockbackForceUp);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(other.GetComponent<Rigidbody2D>().velocity.x, 0);
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, killBounc));
            Destroy(this.gameObject);
        }
    }

    void FlipSprite(bool dir)
    {
        sprRender.flipX = dir;
    }
}
