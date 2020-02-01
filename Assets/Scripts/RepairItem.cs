using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairItem : MonoBehaviour
{ 
  // unique item id
  public int item_id;

  // trigger
  void OnTriggerEnter2D(Collider2D col)
  {
    PlayerController pc = col.gameObject.GetComponent<PlayerController>();

    // is collided with player
    if (pc != null)
    {

      // Take from world then destroy if possible
      if (GameObject.Find("InventorySystem").GetComponent<InventorySystem>().TakeItemFromWorld(item_id))
      {
        Debug.Log("Player picked up item");
        
        // Destroy object
        Destroy(this.gameObject);
      }
    }
  }
}
