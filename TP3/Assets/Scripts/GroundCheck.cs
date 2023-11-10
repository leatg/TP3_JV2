using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayer;
    public float distanceFromGround = 1.0f;

    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, distanceFromGround, groundLayer))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }
}
