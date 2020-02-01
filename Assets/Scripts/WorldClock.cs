using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldClock : MonoBehaviour
{ 

  // Gameobjects
  public GameObject ClockHand;
  public GameObject Pendulum;

  // repairs
  public int amount_of_repairs = 2;

  // times
  public float max_time_end = 10 * 60;
  public float end_screen_time = 5;

  private float max_time_actual;
  private float add_repair_time;
  private float current_time;
  private float last_update_time;

  // pendulum
  public float pendulum_speed = 1.5f;

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
    // repair time to add for each repair object
    add_repair_time = max_time_end / amount_of_repairs;

    // rotation step each secod
    rot_step = 360 / max_time_end;

    // new game
    NewGame();
  }

  // Update is called once per frame
  void Update()
  { 
    if (timer_active)
    {
      // update time
      current_time += Time.deltaTime; 
      CalcPendulumAngle();
    }

    // update rotations
    if (current_time > last_update_time + 1.0f)
    {
      // update last time
      last_update_time = current_time;

      // actually rotate
      ClockHand.transform.Rotate(0.0f, 0.0f, rot_step, Space.Self);

      // Debug
      //Debug.Log("max_time_actual: " + max_time_actual);
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
      if (current_time > max_time_actual + 0.5f)
      {
        EndGame();
      }

      // win condition
      if (max_time_actual >= max_time_end && current_time <= 0.5f)
      {
        WinGame();
      }
    }

  }

  // slot repair
  public void PutItemToSlot(int id)
  {
    RepairSlot[] repair_slots = GetComponentsInChildren<RepairSlot>();
    foreach(RepairSlot rs in repair_slots)
    {
      if (id == rs.slot_id)
      {
        rs.Repair();
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

    // init max time
    max_time_actual = 0;
  }

  // win
  private void WinGame()
  {
    Debug.Log("Won -> credits");
    GameObject.Find("GameLogic").GetComponent<GameLogic>().LoadWinGame();
    new_game = true;
    current_time = 0;

    // init max time
    max_time_actual = 0;
  }

  // go to start screen
  private void StartScreen()
  {
    Debug.Log("start screen");
    GameObject.Find("GameLogic").GetComponent<GameLogic>().LoadStartScreen();
    new_game = false;
    timer_active = false;
    current_time = 0;
  }

  // new game
  public void NewGame()
  {
    Debug.Log("new game");

    // init stuff
    new_game = false;
    timer_active = true;

    // init max time
    max_time_actual = 0;

    // first repair
    AddRepairTime();

    // firs rewind
    Rewind();

  }

  // pendulum movement
  private void CalcPendulumAngle()
  {
    // update rotation
    Pendulum.transform.localRotation = Quaternion.Euler(0, 0, 20f * Mathf.Cos(pendulum_speed * current_time));
  }

  // rewind
  public void Rewind()
  {
    Debug.Log("rewind");
    
    // reset clockhand
    ClockHand.transform.localRotation = Quaternion.Euler(180f, 0, -max_time_actual * 360 / max_time_end);

    // reset times
    current_time = 0;
    last_update_time = 0;
  }

  // add repair time
  public void AddRepairTime()
  {
    Debug.Log("Add repair time");

    // extend time
    max_time_actual += add_repair_time;
  }
}
