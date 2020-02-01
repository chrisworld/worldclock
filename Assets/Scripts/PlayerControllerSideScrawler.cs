using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerControllerSideScrawler : PlayerController
{
    [SerializeField]
    private GameObject FeetCollider;
    [SerializeField]
    private GameObject HeadCollider;
    [SerializeField]
    private Collider2D CrouchCollider;

    protected bool can_standup;

    private void Awake()
    {
        base.Awake();
        var feet_collider = FeetCollider.GetComponent<OnCollisionTrigger>();
        feet_collider.DoEnter += FeetDoEnter;
        feet_collider.DoExit += FeetDoExit;
        var head_collider = HeadCollider.GetComponent<OnCollisionTrigger>();
        head_collider.DoEnter += HeadDoEnter;
        head_collider.DoExit += HeadDoExit;
        can_standup = true;
    }

    private void FeetDoEnter(Collider2D col)
    {
        on_ground = (GroundMask == (GroundMask | (1 << col.gameObject.layer)) && col.gameObject != gameObject);
    }

    private void FeetDoExit(Collider2D col)
    {
        on_ground = !(GroundMask == (GroundMask | (1 << col.gameObject.layer)) && col.gameObject != gameObject);
    }

    private void HeadDoEnter(Collider2D col)
    {
        Debug.Log(String.Format("HeadDoEnter"));
        can_standup = !(GroundMask == (GroundMask | (1 << col.gameObject.layer)) && col.gameObject != gameObject);
    }

    private void HeadDoExit(Collider2D col)
    {
        Debug.Log(String.Format("HeadDoExit"));
        can_standup = (GroundMask == (GroundMask | (1 << col.gameObject.layer)) && col.gameObject != gameObject);
    }

    public override void Move(float move_left_right, float move_up_down, bool crouch, bool jump)
    {
        if (on_ground && !crouch)
        {
            crouch = !can_standup;
        }

        if (on_ground && crouch)
        {
            move_left_right *= CrouchSpeedModifier;
            if(CrouchCollider != null)
                CrouchCollider.enabled = false;
        }
        else
        {
            if (CrouchCollider != null)
                CrouchCollider.enabled = true;
        }

        Vector3 targetVelocity = new Vector2(move_left_right * MovementSpeed, player_physics_body.velocity.y);
        player_physics_body.velocity = Vector3.SmoothDamp(player_physics_body.velocity, targetVelocity, ref previous_velocity, MovementSmoothing);

        if (Math.Abs(move_left_right) < 0.02f && is_facing != Faces.Streight)
        {
            //Debug.Log(String.Format("stright"));
            FrontRenderer.enabled = true;
            SideRenderer.enabled = false;
        }
        else
        {
            //Debug.Log(String.Format("side"));
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

        if (on_ground && jump && !crouch)
        {
            on_ground = false;
            player_physics_body.AddForce(new Vector2(0f, JumpStrenght));
        }
    }
}
