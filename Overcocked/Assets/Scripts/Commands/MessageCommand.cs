using System;
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

  public override void Execute(StoryInterpreter storyInterpreter, Canvas canvas)
  {
    MessagePrompt messagePrompt = canvas.GetComponentInChildren<MessagePrompt>();
    messagePrompt.CurrentMessage = message;
    messagePrompt.OnMessageFinished += OnMessageFinished;
  }

  private void OnMessageFinished(MessagePrompt messagePrompt)
  {
    messagePrompt.OnMessageFinished -= OnMessageFinished;
    NotifyCommandExecuted();
  }
}