using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inserter : MonoBehaviour
{
  // trigger
  void OnTriggerEnter2D(Collider2D col)
  {
    PlayerController pc = col.gameObject.GetComponent<PlayerController>();

    // is collided with player
    if (pc != null)
    {
      // Take from world then destroy if possible
      if (GameObject.Find("InventorySystem").GetComponent<InventorySystem>().PlaceItemIntoClock())
      {
        // Destroy object
        Debug.Log("inserted item!");
      }
      
    }
  }
}
