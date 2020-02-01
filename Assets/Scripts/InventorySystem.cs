using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

  // amount of repair objects
  public int amount_repair_obj = 5;

  public int[] clock_inventory;
  public int[] world_inventory;

  // carry item with id
  private int carry_item_id = 0;
  private bool carry_flag = false;

  // Start is called before the first frame update
  void Start()
  {
    // init inventory array
    clock_inventory = new int[amount_repair_obj];
    world_inventory = new int[amount_repair_obj];
    ResetInventory();
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  // reset all inventories
  void ResetInventory()
  {
    // reset clock inventory
    for(int ci = 0; ci < amount_repair_obj; ci++)
    {
      clock_inventory[ci] = 0;
    }

    // reset world_iventory
    for(int wi = 0; wi < amount_repair_obj; wi++)
    {
      world_inventory[wi] = 0;
    }

    carry_flag = false;
    carry_item_id = 0;
  }

  // take item from world
  public void TakeItemFromWorld(int id)
  {
    // check if player already carry something
    if (!carry_flag)
    {
      world_inventory[id] = 0;
      carry_item_id = id;
      carry_flag = true;
    }
  }

  // put item into worldclock
  public void PlaceItemIntoClock()
  {
    // check carry
    if (carry_flag)
    {
      // check corresponding id
      if (clock_inventory[carry_item_id] == 0)
      {
        // set things
        clock_inventory[carry_item_id] = 1;
        carry_flag = false;
        GameObject.Find("WorldClock").GetComponent<WorldClock>().AddRepairTime();
      }
    }
  }
}
