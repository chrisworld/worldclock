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

    // 
    void Start()
    {
        is_repaired = false;
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
}
