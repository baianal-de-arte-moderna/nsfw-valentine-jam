// vim: set ts=2 sts=2 sw=2 expandtab:
using UnityEngine;

public class InvalidCommand : Command
{
  private CommandData commandData;

  public InvalidCommand(CommandData commandData)
  {
    this.commandData = commandData;
  }

  public override void Execute(StoryInterpreter storyInterpreter)
  {
    Debug.LogError($"Invalid command: {commandData.command}, parameters {commandData.parameters}");
    NotifyCommandExecuted();
  }
}
