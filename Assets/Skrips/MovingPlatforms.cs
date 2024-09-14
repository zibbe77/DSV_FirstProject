using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    [SerializeField] Transform target1, target2;
    [SerializeField] float moveSpeed = 2f;

    public bool active = false;

    Transform currentTarget;
    void Start()
    {
        currentTarget = target1;
    }

    // Update is called once per frame
    void Update()
    {
        if (active == false)
        {
            return;
        }

        if (transform.position == target1.position)
        {
            currentTarget = target2;
        }
        if (transform.position == target2.position)
        {
            currentTarget = target1;
        }


        transform.position = Vector2.MoveTowards(transform.position, currentTarget.position, moveSpeed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}
