using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairSlot : MonoBehaviour
{
  // set slot id
  public int slot_id;

  // repair flag
  private bool is_repaired = false;

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
      // TODO: change sprite
      gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 0f, 0f);
      is_repaired = true;
    }

  }


}
