using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

  // main scene
  public string main_scene = "clock_scene";

  // all the world scenes
  public string[] world_scene;

  // timer of holding esc for quit
  private float quit_time = 0;

  // spawn player
  private int spawn_player_door = 0;

  // old scene for scene change
  private int old_scene_idx = 0;


  // awake
  void Awake()
  {
    // find all objects
    GameObject[] objs = GameObject.FindGameObjectsWithTag("game_logic");

    // see if more than one
    if (objs.Length > 1)
    {
      Destroy(this.gameObject);
    }

    // dont destroy this
    DontDestroyOnLoad(this.gameObject);
  }

  void Start()
  {
    // deactivate mouse
    Cursor.visible = false;
  }

  // update
  void Update()
  {
    // read input
    if (Input.GetKey("escape"))
    {
      // update press timer
      quit_time += Time.deltaTime;

      // quit game
      if (quit_time > 1f)
      {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
      }
    }
    else
    {
      // reset quit time
      quit_time = 0;

      // if in start scene
      if (SceneManager.GetActiveScene().name == "start_scene")
      {
        // load new game
        if (Input.anyKey)
        {
          LoadNewGame();
        }
      }
    }

    // spawn player to corresponding door position
    if (spawn_player_door != 0)
    {
      SpawnLogic();
    }

    // Scene change
    if (old_scene_idx != SceneManager.GetActiveScene().buildIndex)
    {
      Debug.Log("Scene changed to: " + SceneManager.GetActiveScene().name);

      // update scene idx
      old_scene_idx = SceneManager.GetActiveScene().buildIndex;

      // revisit inventory to delete collected obj
      GameObject.Find("InventorySystem").GetComponent<InventorySystem>().DestroyCollectedItemsInWorld();

      // Background Music selection
      if (SceneManager.GetActiveScene().name == main_scene)
      {
        // play worldclock theme
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayBackgroundMusic(0);
      }
      else
      {
        // play overworld theme
        GameObject.Find("SoundManager").GetComponent<SoundManager>().PlayBackgroundMusic(1);
      }
    }
  }

  public void LoadNewGame()
  {
    SceneManager.LoadScene(main_scene);
  }

  // load world scene
  public void LoadWorld(int world_id)
  {

    // load world
    if (SceneManager.GetActiveScene().name == main_scene)
    {
      SceneManager.LoadScene(world_scene[world_id]);
      GameObject.Find("WorldClock").GetComponent<WorldClock>().ShowWorldClock(false);
    }

    // go back to watch main scene
    else
    {
      // load scene
      SceneManager.LoadScene(main_scene);
      spawn_player_door = world_id + 1;
    }
  }

  // Spawn Logic
  private void SpawnLogic()
  {
    // spawn
    GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
    
    if (players.Length == 1)
    {
      GameObject[] doors = GameObject.FindGameObjectsWithTag("world_door");
      if (doors.Length >= spawn_player_door-1)
      {
        Debug.Log("spawn to door: " + spawn_player_door);

        foreach(GameObject door in doors)
        {
          if(door.GetComponent<WorldDoor>().world_id == spawn_player_door-1)
          {
            // set player pos to door
            players[0].transform.position = door.transform.position;
            spawn_player_door = 0;

            // show the clock
            GameObject.Find("WorldClock").GetComponent<WorldClock>().ShowWorldClock(true);

            // get repaire slots in worldclock
            GameObject[] slots = GameObject.FindGameObjectsWithTag("clock_slot");

            // resotre each slot
            foreach (GameObject slot in slots)
            {
              slot.GetComponent<RepairSlot>().RestoreRepair(); 
            }
          }
        }

      }
    }
  }

}
