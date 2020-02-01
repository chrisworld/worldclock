using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanClimbBehaviour : MonoBehaviour
{
    [SerializeField] public bool CanClimb;
    [SerializeField] protected LayerMask ClimbMask;

    private int climb_count = 0;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (ClimbMask == (ClimbMask | (1 << col.gameObject.layer)))
        {
            climb_count++;
            CanClimb = climb_count > 0;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (ClimbMask == (ClimbMask | (1 << col.gameObject.layer)))
        {
            climb_count--;
            CanClimb = climb_count > 0;
        }
    }
}
