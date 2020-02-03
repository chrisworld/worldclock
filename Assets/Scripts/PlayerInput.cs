using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private PlayerController player_controler = null;

    private float move_left_right = 0.0f;
    private float move_up_down = 0.0f;
    private bool crouch = false;
    private bool jump = false;

    private bool freeze = false;

    private void Update()
    {
        move_left_right = Input.GetAxisRaw("Horizontal");
        move_left_right = Math.Abs(move_left_right) < 0.02 ? 0 : move_left_right;
        move_up_down = Input.GetAxisRaw("Vertical");
        move_up_down = Math.Abs(move_up_down) < 0.02 ? 0 : move_up_down;
        jump = Input.GetButtonDown("Jump") || jump;
        crouch = Input.GetButtonDown("Crouch") || !Input.GetButtonUp("Crouch") && crouch;
    }

    private void FixedUpdate()
    {
        if (freeze)
        {
            player_controler.Move(0, 0, crouch, jump);
        }
        else
        {
            player_controler.Move(move_left_right * Time.fixedDeltaTime, move_up_down * Time.fixedDeltaTime, crouch, jump);
            jump = false;  
        }

    }

    public void SetFreeze(bool acti)
    {
        freeze = acti;
    }
}
