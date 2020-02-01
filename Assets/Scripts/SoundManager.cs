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

  // sound fx
  public AudioSource door;
  public AudioSource footsteps;
  public AudioSource rewind;

  // play door sound
  public void PlayDoorSound()
  {
    door.Play(0);
  }

  // play footstep sound
  public void PlayFootstepSound()
  {
    footsteps.Play(0);
  }

  // play footstep sound
  public void PlayFootstepCrouchSound()
  {
    footsteps.pitch = 1.0f + Random.Range(-0.1f, 0.1f);
    footsteps.Play(0);
  }

  // play rewind sound
  public void PlayRewindSound()
  {
    rewind.pitch = 1.0f + Random.Range(-0.1f, 0.1f);
    rewind.loop = true;
    rewind.Play(0);
  }

  // stop rewind sound
  public void StopRewindSound()
  {
    rewind.loop = false;
    rewind.Stop();
  }

  
}
