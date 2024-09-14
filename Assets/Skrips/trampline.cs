using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trampline : MonoBehaviour
{
    [SerializeField] float jumpForce = 500f;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody2D playerRigidbody = other.GetComponent<Rigidbody2D>();
            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            GetComponent<Animator>().SetTrigger("Jump");
        }
    }
}
