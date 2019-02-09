using UnityEngine;

public class InvalidCommand : Command
{
  private CommandData commandData;

  public InvalidCommand(CommandData commandData)
  {
    this.commandData = commandData;
  }

  public override void Execute(Canvas canvas)
  {
    Debug.LogError($"Invalid command: {commandData.command}, parameters {commandData.parameters}");
    NotifyCommandExecuted();
  }
}
