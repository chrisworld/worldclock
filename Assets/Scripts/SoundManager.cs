using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// include this: 
//GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayFootstepSound();

public class SoundManager : MonoBehaviour
{
  // background music
  public AudioSource worldclock_theme;
  public AudioSource overworld_theme;

  // ticks
  public AudioSource tick2;
  public AudioSource tick4;
  public AudioSource tick8;
  public AudioSource tick16;

  // sound fx
  public AudioSource door;
  public AudioSource footsteps;
  public AudioSource rewind;
  public AudioSource repair;

  // Background Soud
  public void PlayBackgroundMusic(int back_id)
  {
    // play worldclock theme
    if (back_id == 0)
    {
      worldclock_theme.Play(0);
      overworld_theme.Stop();
    }
    // play overworld theme
    else
    {
      overworld_theme.Play(0);
      worldclock_theme.Stop();
    }
  }


  // play door sound
  public void PlayDoorSound()
  {
    door.Play(0);
  }

  // play footstep sound
  public void PlayFootstepSound()
  {
    footsteps.pitch = 1.0f + Random.Range(-0.1f, 0.1f);
    footsteps.Play(0);
  }

  // play footstep sound
  public void PlayFootstepAdventure()
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

  // play Repair sound
  public void PlayRepairSound()
  {
    repair.pitch = 1.0f + Random.Range(-0.1f, 0.1f);
    repair.Play(0);
  }
  
}
