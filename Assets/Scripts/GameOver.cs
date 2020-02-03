using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
  public SpriteRenderer gameoverlay;
  public SpriteRenderer win_overlay;
  public SpriteRenderer start_overlay;

  // game overlay
  public void SetGameOverlayActive(bool acti)
  {
    FreezePlayer(acti);
    gameoverlay.enabled = acti;
  }

  // win overlay
  public void SetWinOverlayActive(bool acti)
  {
    FreezePlayer(acti);
    win_overlay.enabled = acti;
  }

  // start overlay
  public void SetStartOverlayActive(bool acti)
  {
    FreezePlayer(acti);
    start_overlay.enabled = acti;
  }

  // game overlay
  public void FreezePlayer(bool acti)
  {
    // activate overlay
    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

    // stop motion
    if (players.Length >= 1)
    {
      // stop movement
      players[0].GetComponent<PlayerInput>().SetFreeze(acti);

      // stop animator
      Animator[] anims = players[0].GetComponentsInChildren<Animator>();

      // deactivate
      foreach(Animator anim in anims)
      {
        anim.enabled = !acti;
      }
    }
  }
}
