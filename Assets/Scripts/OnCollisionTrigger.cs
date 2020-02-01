using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionTrigger : MonoBehaviour
{
    public delegate void Enter(Collider2D col);
    public delegate void Exit(Collider2D col);

    public Enter DoEnter;
    public Exit DoExit;

    void OnTriggerEnter2D(Collider2D col)
    {
        DoEnter(col);
    }

    void OnTriggerExit2D(Collider2D col)
    {
        DoExit(col);
    }
}
