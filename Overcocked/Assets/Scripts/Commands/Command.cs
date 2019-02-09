using System.Collections.Generic;
using UnityEngine;

public abstract class Command
{
  public delegate void CommandExecutedAction();
  public event CommandExecutedAction OnCommandExecuted;

  abstract public void Execute(StoryInterpreter storyInterpreter, Canvas canvas);

  protected void NotifyCommandExecuted()
  {
    OnCommandExecuted();
  }
}
