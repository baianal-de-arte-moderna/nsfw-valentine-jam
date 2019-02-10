// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuInputHandler : MonoBehaviour
{
  public void OnStartButtonClicked()
  {
    DoStartPlay();
  }

  public void OnExitButtonClicked()
  {
    DoExit();
  }

  private void DoStartPlay()
  {
    SceneManager.LoadScene("MainGame");
  }

  private void DoExit()
  {
    Application.Quit();
  }

  void Start() { }

  void Update() {
    if (Input.GetAxis("Cancel") != 0.0f) {
      DoExit();
    }
  }
}
