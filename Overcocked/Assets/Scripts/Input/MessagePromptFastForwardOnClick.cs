// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePromptFastForwardOnClick : MonoBehaviour
{
  // Update is called once per frame
  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      MessagePrompt messagePrompt = GetComponent<MessagePrompt>();
      messagePrompt.FastForward();
    }
  }
}
