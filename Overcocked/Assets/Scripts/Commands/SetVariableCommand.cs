// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections.Generic;

public class SetVariableCommand : Command
{
  private readonly string key;
  private readonly string value;

  public SetVariableCommand(Dictionary<string, string> parameters)
  {
    key = parameters["key"];
    value = parameters["value"];
  }

  public override void Execute(StoryInterpreter storyInterpreter)
  {
    storyInterpreter.SetVariable(key, value);
    NotifyCommandExecuted();
  }
}
