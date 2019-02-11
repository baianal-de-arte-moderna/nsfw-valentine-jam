// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitHandler : MonoBehaviour
{
  private void DoExit()
  {
    Application.Quit();
  }

  void Update() {
    if (Input.GetAxis("Cancel") != 0.0f) {
      DoExit();
    }
  }
}
