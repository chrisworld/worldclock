using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObject : MonoBehaviour
{
    [SerializeField]
    private Transform ObjectToFollow = null;

    [Range(0, 1)]
    [SerializeField]
    protected float MovementSmoothing = 0.1f;

    private Vector3 cam_offset;

    private void Awake()
    {
        cam_offset = transform.position - ObjectToFollow.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, ObjectToFollow.position + cam_offset, MovementSmoothing);
    }
}
