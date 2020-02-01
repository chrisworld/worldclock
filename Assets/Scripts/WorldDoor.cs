using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldDoor : MonoBehaviour
{

  public int world_id;
  private bool player_at_door;

  void Start()
  {
    player_at_door = false;
  }

  void Update()
  {
    if (player_at_door)
    {
      // go to world
      if(Input.GetAxisRaw("Vertical") != 0)
      {
        GameObject.Find("GameLogic").GetComponent<GameLogic>().LoadWorld(world_id);
      }
    }
  }

  void OnTriggerEnter2D(Collider2D col)
  {
    // is collided with player
    if (col.gameObject.GetComponent<PlayerController>() != null)
    {
      player_at_door = true;
    }
  }

  void OnTriggerExit2D(Collider2D col)
  {
    // is collided with player
    if (col.gameObject.GetComponent<PlayerController>() != null)
    {
      player_at_door = false;
    }
  }


}
