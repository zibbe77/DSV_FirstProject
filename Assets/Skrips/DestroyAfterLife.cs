using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterLife : MonoBehaviour
{
    [SerializeField] float lifetime = 4f;

    // Update is called once per frame
    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
