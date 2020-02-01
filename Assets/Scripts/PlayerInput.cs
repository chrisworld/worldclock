using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private PlayerController player_controler;

    private float move_left_right = 0.0f;
    private float move_up_down = 0.0f;
    private bool crouch = false;
    private bool jump = false;

    private void Update()
    {
        move_left_right = Input.GetAxisRaw("Horizontal");
        move_up_down = Input.GetAxisRaw("Vertical");
        jump = Input.GetButtonDown("Jump") || jump;
        crouch = Input.GetButtonDown("Crouch") || !Input.GetButtonUp("Crouch") && crouch;
    }

    private void FixedUpdate()
    {
        player_controler.Move(move_left_right * Time.fixedDeltaTime, move_up_down * Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
