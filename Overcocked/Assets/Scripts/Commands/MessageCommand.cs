using System.Collections.Generic;
using UnityEngine;

public class MessageCommand : Command
{
  private MessagePrompt.Message message;

  public MessageCommand(List<string> parameters)
  {
    message = new MessagePrompt.Message
    {
      Name = parameters[0],
      MessageText = parameters[1]
    };
  }

  public override void Execute(Canvas canvas)
  {
    MessagePrompt messagePrompt = canvas.GetComponentInChildren<MessagePrompt>();
    messagePrompt.CurrentMessage = message;
    messagePrompt.OnMessageFinished += NotifyCommandExecuted;
  }
}