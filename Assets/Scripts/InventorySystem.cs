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

  // world clock reference
  private GameObject world_clock;

  // Start is called before the first frame update
  void Awake()
  {
    // init inventory array
    clock_inventory = new int[amount_repair_obj];
    world_inventory = new int[amount_repair_obj];
    ResetInventory();

    // get references
    world_clock = GameObject.Find("WorldClock");
  }

  // Update is called once per frame
  void Update()
  {
      
  }

  // reset all inventories
  public void ResetInventory()
  { 
    Debug.Log("Reset Inventory");
    
    // reset clock inventory
    for(int ci = 0; ci < amount_repair_obj; ci++)
    {
      clock_inventory[ci] = 0;
    }

    // reset world_iventory
    for(int wi = 0; wi < amount_repair_obj; wi++)
    {
      world_inventory[wi] = 1;
    }

    // reset
    carry_flag = false;
    carry_item_id = 0;
  }

  // take item from world
  public bool TakeItemFromWorld(int id)
  {
    // check if player already carry something
    if (!carry_flag)
    {
      world_inventory[id] = 0;
      carry_item_id = id;
      carry_flag = true;
      return true;
    }
    return false;
  }

  // put item into worldclock
  public bool PlaceItemIntoClock()
  {
    // check carry
    if (carry_flag)
    {
      // check corresponding id is not set
      if (clock_inventory[carry_item_id] == 0)
      {
        // set things
        clock_inventory[carry_item_id] = 1;
        carry_flag = false;
        world_clock.GetComponent<WorldClock>().AddRepairTime();
        world_clock.GetComponent<WorldClock>().PutItemToSlot(carry_item_id);
        return true;
      }
    }
    return false;
  }

  // get carry flag
  public bool GetCarryFlag()
  {
    return carry_flag;
  }

  // get carry item id
  public int GetCarryItemId()
  {
    return carry_item_id;
  }

  // destroy allready collected inventory
  public void DestroyCollectedItemsInWorld()
  {
    GameObject[] items = GameObject.FindGameObjectsWithTag("repair_item");

    // run through each item
    foreach(GameObject item in items)
    {
      int item_id = item.GetComponent<RepairItem>().item_id;
      if(world_inventory[item_id] == 0)
      {
        Debug.Log("Destroy item wit id: " + item_id);
        Destroy(item);
      }
    }
  }
}
