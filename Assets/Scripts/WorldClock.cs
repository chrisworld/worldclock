using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldClock : MonoBehaviour
{ 

  // Gameobjects
  public GameObject ClockArm;
  public GameObject Pendulum;

  // repairs
  public int amount_of_repairs = 2;

  // times
  public float max_time_end = 10 * 60;
  public float end_screen_time = 5;

  private float add_repair_time;
  private float current_time;
  private float last_update_time;

  // rotations
  private float rot_step;

  // some stat vars
  private bool new_game = false;

  // timer active
  private bool timer_active = true;

  // awake
  void Awake()
  {
    // find all objects
    GameObject[] objs = GameObject.FindGameObjectsWithTag("world_clock");

    // see if more than one
    if (objs.Length > 1)
    {
      Destroy(this.gameObject);
    }

    // dont destroy this
    DontDestroyOnLoad(this.gameObject);
  }

  // Start is called before the first frame update
  void Start()
  {
    // init stuff
    NewGame();

    // repair time to add for each repair object
    add_repair_time = max_time_end / amount_of_repairs;

    // rotation step each secod
    rot_step = 360 / max_time_end;
  }

  // Update is called once per frame
  void Update()
  { 
    if (timer_active)
    {
      // update time
      current_time += Time.deltaTime; 
    }


    // update rotations
    if (current_time > last_update_time + 1.0f)
    {
      // update last time
      last_update_time = current_time;

      // actually rotate
      ClockArm.transform.Rotate(0.0f, 0.0f, rot_step, Space.Self);
    }


    // new game
    if (new_game)
    {
      if (current_time > end_screen_time)
      {
        StartScreen();
      }
    }
    else
    {
      // end game
      if (current_time > max_time_end)
      {
        EndGame();
      }
    }

  }

  // end game
  private void EndGame()
  {
    Debug.Log("end game");
    GameObject.Find("GameLogic").GetComponent<GameLogic>().LoadEndGame();
    new_game = true;
    current_time = 0;
  }

  // go to start screen
  private void StartScreen()
  {
    Debug.Log("start screen");
    GameObject.Find("GameLogic").GetComponent<GameLogic>().LoadStartScreen();
    new_game = false;
    current_time = 0;
    timer_active = false;
  }

  // new game
  public void NewGame()
  {
    Debug.Log("new game");

    // init stuff
    current_time = 0;
    last_update_time = 0;
    new_game = false;
    timer_active = true;
  }
}
