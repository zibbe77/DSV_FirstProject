using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brige : MonoBehaviour
{

    [SerializeField] Animator animator;
    bool hasPlayadAnimation = false;


    void Start()
    {
        // animator = GetComponent<Animator>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !hasPlayadAnimation)
        {
            hasPlayadAnimation = true;
            animator.SetTrigger("Move");
        }
    }
}
