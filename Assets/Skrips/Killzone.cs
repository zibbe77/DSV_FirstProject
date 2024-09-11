using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Killzone : MonoBehaviour
{
    [SerializeField] Transform spawnPositon;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = spawnPositon.position;
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }
}
