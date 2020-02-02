using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
  // game overlay
  public void SetGameOverlayActive(bool acti)
  {
    // activate overlay
    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

    // stop motion
    if (players.Length >= 1)
    {
      Debug.Log("Deactivate player: " + players.Length);

      // stop movement
      players[0].GetComponent<PlayerInput>().enabled = !acti;

      // stop animator
      Animator[] anims = players[0].GetComponentsInChildren<Animator>();

      // deactivate
      foreach(Animator anim in anims)
      {
        anim.enabled = !acti;
      }
    }
    gameObject.GetComponent<SpriteRenderer>().enabled = acti;

  }
}
