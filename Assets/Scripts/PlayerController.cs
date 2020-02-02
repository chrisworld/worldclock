using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class PlayerController : MonoBehaviour
{
    [SerializeField]
    protected float JumpStrenght = 0.0f;
    [SerializeField]
    protected float MovementSpeed = 400.0f;

    // footstep sound time
    [SerializeField]
    protected float footstep_acti_time = 0.25f;

    [Range(0, 1)]
    [SerializeField]
    protected float CrouchSpeedModifier = 0.4f;
    [SerializeField]
    protected LayerMask GroundMask;
    [Range(0, 1)]
    [SerializeField]
    protected float MovementSmoothing = 0.1f;

    protected Rigidbody2D player_physics_body;

    [SerializeField]
    protected bool on_ground;

    protected Vector3 previous_velocity;

    protected enum Faces
    {
        Right,
        Streight,
        Left,
        Up,
        Down
    }

    protected Faces is_facing;

    protected void Awake()
    {
        player_physics_body = GetComponent<Rigidbody2D>();
    }

    public abstract void Move(float move_left_right, float move_up_down, bool crouch, bool jump);
}
