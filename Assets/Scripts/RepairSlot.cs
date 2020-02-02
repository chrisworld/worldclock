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
      // get repaied from inventary
      int[] clock_slots = GameObject.Find("InventorySystem").GetComponent<InventorySystem>().clock_inventory;

      int i = 0;
      foreach(int slot in clock_slots)
      {
        if (slot_id == i)
        {
          if (slot == 1)
          {
            is_repaired = true;
          }
          else
          {
            is_repaired = false;
          }
        }
        i++;
      }
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
