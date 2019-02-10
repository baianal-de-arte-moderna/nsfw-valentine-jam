public class PauseCommand : Command
{
  private OnClickHandler.MouckClickAction mouseClickAction;

  public override void Execute(StoryInterpreter storyInterpreter)
  {
    OnClickHandler onClickHandler = storyInterpreter.GetOnClickHandler();
    mouseClickAction = () =>
    {
      onClickHandler.OnMouseClicked -= mouseClickAction;
      NotifyCommandExecuted();
    };
    onClickHandler.OnMouseClicked += mouseClickAction;
  }
}
