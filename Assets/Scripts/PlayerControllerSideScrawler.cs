using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;

public class PlayerControllerSideScrawler : PlayerController
{
    [SerializeField]
    private GameObject FeetCollider = null;
    [SerializeField]
    private GameObject HeadCollider = null;
    [SerializeField]
    private Collider2D CrouchCollider = null;

    [SerializeField]
    private Collider2D LadderCollider = null;
    [Range(0, 1)]
    [SerializeField]
    protected float ClimbSpeedModifier = 0.01f;

    [SerializeField]
    protected SpriteRenderer SideRenderer;
    protected Animator SideAnimator;

    private CircleCollider2D feet_collider;
    private CircleCollider2D head_collider;
    private CanClimbBehaviour can_climb;
    private float climb_change_timer;

    new private void Awake()
    {
        base.Awake();
        is_facing = Faces.Right;
        SideRenderer.enabled = true;
        SideAnimator = SideRenderer.GetComponent<Animator>();

        feet_collider = FeetCollider.GetComponent<CircleCollider2D>();
        head_collider = HeadCollider.GetComponent<CircleCollider2D>();
        can_climb     = FeetCollider.GetComponent<CanClimbBehaviour>();
    }

    private void FixedUpdate()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(feet_collider.transform.position, feet_collider.radius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (GroundMask == (GroundMask | (1 << colliders[i].gameObject.layer)))
            {
                on_ground = true;
                SideAnimator.SetBool("Is_Jumping", false);
                break;
            }
        }
    }

    public override void Move(float move_left_right, float move_up_down, bool crouch, bool jump)
    {
        if (on_ground && !crouch)
        {
            Collider2D collider = Physics2D.OverlapCircle(head_collider.transform.position, head_collider.radius,
                GroundMask);
            crouch = collider != null;
        }

        if (on_ground && crouch)
        {
            move_left_right *= CrouchSpeedModifier;
            if (CrouchCollider != null)
                CrouchCollider.enabled = false;
        }
        else
        {
            if (CrouchCollider != null)
                CrouchCollider.enabled = true;
        }

        Vector3 targetVelocity = new Vector2(move_left_right * MovementSpeed, player_physics_body.velocity.y);
        player_physics_body.velocity = Vector3.SmoothDamp(player_physics_body.velocity, targetVelocity,
            ref previous_velocity, MovementSmoothing);

        SideAnimator.SetBool("Is_Walking", Math.Abs(move_left_right) > 0.0001f);

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

        if (on_ground && jump && !crouch)
        {
            SideAnimator.SetBool("Is_Jumping", true);
            on_ground = false;
            player_physics_body.AddForce(new Vector2(0f, JumpStrenght));
        }

        if (Math.Abs(move_up_down) > 0 && can_climb.CanClimb)
        {
            if(!SideAnimator.GetBool("Is_Climbing"))
                SideAnimator.SetBool("Is_Climbing", true);
            Vector2 up = new Vector2(0, move_up_down * ClimbSpeedModifier * MovementSpeed);
            player_physics_body.MovePosition(player_physics_body.position + up);
            LadderCollider.isTrigger = true;
        }
        else
        {
            LadderCollider.isTrigger = false;
            SideAnimator.SetBool("Is_Climbing", false);            
        }
    }
}
