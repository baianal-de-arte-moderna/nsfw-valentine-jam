// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections.Generic;

public class PushStoryCommand : Command
{
  private readonly string storyName;

  public PushStoryCommand(Dictionary<string, string> parameters)
  {
    storyName = parameters["storyName"];
  }

  public override void Execute(StoryInterpreter storyInterpreter)
  {
    storyInterpreter.PushStory(storyName);
    NotifyCommandExecuted();
  }
}
