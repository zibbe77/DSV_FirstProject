using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PineApple : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] PlayerMovment playerSkript;
    bool followPlayer = false;

    Vector3 targetPos;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (followPlayer == true)
        {
            if (playerSkript.dirNum == 0)
            {
                targetPos = new Vector3(playerPos.position.x - 0.7f, playerPos.position.y + 0.4f, playerPos.position.z);
            }
            else
            {
                targetPos = new Vector3(playerPos.position.x + 0.7f, playerPos.position.y + 0.4f, playerPos.position.z);
            }

            transform.position = Vector2.Lerp(transform.position, targetPos, Time.deltaTime * 2);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            followPlayer = true;
            playerSkript.hasPineApple = true;
        }
    }
}
