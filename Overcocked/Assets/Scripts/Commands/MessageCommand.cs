// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections.Generic;
using UnityEngine;

public class MessageCommand : Command
{
  private readonly MessagePrompt.Message message;

  public MessageCommand(Dictionary<string, string> parameters)
  {
    message = new MessagePrompt.Message
    {
      Name = parameters.ContainsKey("name") ? parameters["name"] : "",
      MessageText = parameters["messageText"]
    };
  }

  public override void Execute(StoryInterpreter storyInterpreter)
  {
    Canvas canvas = storyInterpreter.GetCanvas();
    MessagePrompt messagePrompt = canvas.GetComponentInChildren<MessagePrompt>(true);
    messagePrompt.CurrentMessage = message;
    messagePrompt.OnMessageFinished += OnMessageFinished;
  }

  private void OnMessageFinished(MessagePrompt messagePrompt)
  {
    messagePrompt.OnMessageFinished -= OnMessageFinished;
    NotifyCommandExecuted();
  }
}
