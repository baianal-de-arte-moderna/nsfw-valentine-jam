// vim: set ts=2 sts=2 sw=2 expandtab:
public abstract class Command
{
  public delegate void CommandExecutedAction();
  public event CommandExecutedAction OnCommandExecuted;

  abstract public void Execute(StoryInterpreter storyInterpreter);

  protected void NotifyCommandExecuted()
  {
    OnCommandExecuted();
  }
}
