using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewinder : MonoBehaviour
{
  // trigger
  void OnTriggerEnter2D(Collider2D col)
  {
    PlayerController pc = col.gameObject.GetComponent<PlayerController>();

    // is collided with player
    if (pc != null)
    {
      // Rewind the worldclock
      GameObject.Find("WorldClock").GetComponent<WorldClock>().Rewind();
    }
  }
}
