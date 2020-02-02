using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldClock : MonoBehaviour
{
    [SerializeField]
    protected float RewindSpeed = 6.0f;

    [Range(1, 360)]
    [SerializeField]
    protected int DiskreteTickModifier = 2;

    // Gameobjects
    public GameObject ClockHand;
  public GameObject Pendulum;

  // repairs
  public int amount_of_repairs = 4;
  private float add_repair_time;
  private float max_time_actual;

  // times
  public float max_time_end = 10 * 60;
  public float end_screen_time = 5;

  private float current_time;

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

    // repair time to add for each repair object
    add_repair_time = max_time_end / (amount_of_repairs + 1);

    // rotation step each secod
    rot_step = 360 / max_time_end;
     NewGame();
  }

  // Start
  void Start()
  {
    // Overlays
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

     RotateClockHand();

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
      if (current_time >= max_time_end)
      {
          EndGame();
          current_time = 0;
      }

      // win condition
      if (max_time_actual >= max_time_end && current_time >= max_time_end)
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
    Debug.Log("Game Over time stoped");
    new_game = true;
    current_time = 0;
    
    // Game Overlay
    GameObject.Find("GameOverlay").GetComponent<GameOver>().SetGameOverlayActive(true);

    //GameObject.Find("GameLogic").GetComponent<GameLogic>().LoadEndGame();

    // reset inventory
    GameObject.Find("InventorySystem").GetComponent<InventorySystem>().ResetInventory();
  }

  // win
  private void WinGame()
  {
    Debug.Log("Won -> credits");
    GameObject.Find("GameLogic").GetComponent<GameLogic>().LoadWinGame();
    new_game = true;
    // reset inventory
    GameObject.Find("InventorySystem").GetComponent<InventorySystem>().ResetInventory();
  }

  // go to start screen
  private void StartScreen()
  {
    Debug.Log("start screen");
    GameObject.Find("GameLogic").GetComponent<GameLogic>().LoadStartScreen();
    new_game = false;
    timer_active = false;
  }

  // new game
  public void NewGame()
  {
    Debug.Log("new game");

    // init stuff
    new_game = false;
    timer_active = true;

    // show world clock
    ShowWorldClock(true);

    // init max time
    max_time_actual = add_repair_time * amount_of_repairs;
    Debug.Log(String.Format("max_time_actual {0}", max_time_actual));
    current_time = max_time_actual;

    RotateClockHand();
  }

  // pendulum movement
  private void CalcPendulumAngle()
  {
    if (!new_game)
    {
      // update rotation
      Pendulum.transform.localRotation = Quaternion.Euler(0, 0, 20f * Mathf.Cos(pendulum_speed * current_time));
    }
  }

    // rewind
    public void Rewind(float rewind_for)
    {
        current_time -= rewind_for * RewindSpeed;
        if (current_time <= max_time_actual)
        {
            // reset times
            current_time = max_time_actual;
        }
        RotateClockHand();
    }

  // add repair time
  public void AddRepairTime()
  {
    Debug.Log("Add repair time");

    // extend time
    max_time_actual -= add_repair_time;
  }

  // activate parts
  public void ShowWorldClock(bool activate)
  {
    this.gameObject.transform.GetChild(0).gameObject.SetActive(activate);
  }

  private void RotateClockHand()
  {
    if (!new_game)
    {
      float angle = -rot_step * current_time;
      float remain = angle % DiskreteTickModifier;
      ClockHand.transform.localRotation = Quaternion.Euler(0, 0, angle-remain);
    }
  }
}
