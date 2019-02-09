// vim: set ts=2 sts=2 sw=2 expandtab:
using UnityEngine;

public class MessagePromptOnClick : MonoBehaviour
{
  // Update is called once per frame
  void Update() {
    if (Input.GetMouseButtonDown(0)) {
      MessagePrompt messagePrompt = GetComponent<MessagePrompt>();
      messagePrompt.OnClick();
    }
  }
}
