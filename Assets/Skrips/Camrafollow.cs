using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camrafollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] Vector3 offset = new Vector3(0, 0, -10f);
    [SerializeField] float smoothing = 1.0f;

    void LateUpdate()
    {
        Vector3 position = Vector3.Lerp(transform.position, target.position + offset, smoothing * Time.deltaTime);
        transform.position = position;
    }
}
