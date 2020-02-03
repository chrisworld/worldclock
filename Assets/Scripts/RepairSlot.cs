using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSlot : MonoBehaviour
{
    // set slot id
    public int slot_id;

    // repair flag
    private bool is_repaired = false;
    private Animator RepairAnimator;

    private void Awake()
    {
        RepairAnimator = GetComponent<Animator>();
    }

    // repair slot
    public void Repair()
    {
        // repair
        if (!is_repaired)
        {
            RepairAnimator.SetTrigger("Repair");
            is_repaired = true;
        }
    }

    // check if repaired
    public void RestoreRepair()
    {
      if (is_repaired)
      {
        RepairAnimator.SetTrigger("Repair");
      }
    }
}
