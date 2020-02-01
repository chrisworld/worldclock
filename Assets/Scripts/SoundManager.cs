using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
  // background music
  public AudioSource background_music;

  // ticks
  public AudioSource tick2;
  public AudioSource tick4;
  public AudioSource tick8;
  public AudioSource tick16;

  // door
  public AudioSource door;

  // play door sound
  public void PlayDoorSound()
  {
    door.Play(0);
  }

  
}
