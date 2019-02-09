using System.Collections.Generic;
using UnityEngine;

public class SetVariableCommand : Command
{
  private readonly string key;
  private readonly string value;

  public SetVariableCommand(Dictionary<string, string> parameters)
  {
    key = parameters["key"];
    value = parameters["value"];
  }

  public override void Execute(StoryInterpreter storyInterpreter, Canvas canvas)
  {
    storyInterpreter.SetVariable(key, value);
    NotifyCommandExecuted();
  }
}
