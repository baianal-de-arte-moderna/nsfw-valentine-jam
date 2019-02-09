// vim: set ts=2 sts=2 sw=2 expandtab:
using UnityEngine;

class HideMessagePromptCommand : Command {
  public override void Execute(Canvas canvas) {
    MessagePrompt messagePrompt = canvas.GetComponentInChildren<MessagePrompt>();
    if (messagePrompt != null) {
      messagePrompt.CurrentMessage = null;
    }
    NotifyCommandExecuted();
  }
}
