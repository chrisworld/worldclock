using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLogic : MonoBehaviour
{

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

  // start game scene
  public void LoadStartScreen()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("start_scene");
  }

  // end game scene
  public void LoadEndGame()
  {
    UnityEngine.SceneManagement.SceneManager.LoadScene("end_scene");
  }


}
