using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControllerAdventure : PlayerController
{
    [SerializeField]
    private SpriteRenderer FrontRenderer;
    [SerializeField]
    private SpriteRenderer SideRenderer;

    private Animator FrontAnimator;
    private Animator SideAnimator;

    [Range(0, 1)]
    [SerializeField]
    private float FeetColliderRadius = 0.5f;
    [Range(0, 1)]
    [SerializeField]
    private float HeadColliderRadius = 0.5f;

    [SerializeField]
    private Collider2D FullSizeCollider;
    [SerializeField]
    private Collider2D SmallSizeCollider;

    private float ground_collide_radius;
    private float head_collide_radius;

    private float ground_collide_pos;
    private float head_collide_pos;

    private void Awake()
    {
        base.Awake();
        ground_collide_radius = transform.localScale.x * FeetColliderRadius;
        head_collide_radius = transform.localScale.x * HeadColliderRadius;
        ground_collide_pos = (transform.localScale.y * 0.5f) - FeetColliderRadius;
        head_collide_pos = (transform.localScale.y * 0.5f) - HeadColliderRadius;

        is_facing = Faces.Streight;
        FrontRenderer.enabled = true;
        SideRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        Vector3 gcc = transform.position;
        gcc.Set(gcc.x, gcc.y - ground_collide_pos, gcc.z);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(gcc, ground_collide_radius, GroundMask);
        for (int i = 0; i < colliders.Length; i++)
        {
            on_ground = colliders[i].gameObject != gameObject;
        }
    }

    public override void Move(float move_left_right, float move_up_down, bool crouch, bool jump)
    {
        if (!crouch)
        {
            Vector3 gcc = transform.position;
            gcc.Set(gcc.x, gcc.y + head_collide_pos, gcc.z);
            if (Physics2D.OverlapCircle(gcc, head_collide_radius, GroundMask))
            {
                crouch = true;
            }
        }

        if (on_ground)
        {
            if (crouch)
            {
                move_left_right *= CrouchSpeedModifier;
                FullSizeCollider.enabled = false;
                SmallSizeCollider.enabled = true;
            }
            else
            {
                FullSizeCollider.enabled = true;
                SmallSizeCollider.enabled = false;
            }

            Vector3 targetVelocity = new Vector2(move_left_right * MovementSpeed, player_physics_body.velocity.y);
            player_physics_body.velocity = Vector3.SmoothDamp(player_physics_body.velocity, targetVelocity, ref previous_velocity, MovementSmoothing);

            if (move_left_right == 0.0f && is_facing != Faces.Streight)
            {
                FrontRenderer.enabled = true;
                SideRenderer.enabled = false;
            }
            else
            {
                FrontRenderer.enabled = false;
                SideRenderer.enabled = true;
                Vector3 theScale = transform.localScale;
                if (move_left_right > 0 && is_facing != Faces.Right)
                {
                    is_facing = Faces.Right;
                    theScale.x *= -1;
                }
                else if (move_left_right < 0 && is_facing != Faces.Left)
                {
                    is_facing = Faces.Left;
                    theScale.x *= -1;
                }
                transform.localScale = theScale;
            }
        }

        if (on_ground && jump)
        {
            on_ground = false;
            player_physics_body.AddForce(new Vector2(0f, JumpStrenght));
        }
    }
}
