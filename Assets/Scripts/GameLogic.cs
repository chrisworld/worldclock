using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

  // timer of holding esc for quit
  private float quit_time = 0;


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
        Debug.Log("quit game");
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
  }

  // start game scene
  public void LoadStartScreen()
  {
    SceneManager.LoadScene("start_scene");
  }

  public void LoadNewGame()
  {
    if (GameObject.Find("WorldClock"))
    {
      GameObject.Find("WorldClock").GetComponent<WorldClock>().NewGame();
    }
    SceneManager.LoadScene("clock_scene");
  }

  // end game scene
  public void LoadEndGame()
  {
    SceneManager.LoadScene("end_scene");
  }

  // end game scene
  public void LoadWinGame()
  {
    SceneManager.LoadScene("win_scene");
  }

}
