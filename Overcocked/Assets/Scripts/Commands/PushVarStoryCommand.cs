// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections.Generic;

public class PushVarStoryCommand : Command
{
  private readonly string varStoryName;

  public PushVarStoryCommand(Dictionary<string, string> parameters)
  {
    varStoryName = parameters["varStoryName"];
  }

  public override void Execute(StoryInterpreter storyInterpreter)
  {
    storyInterpreter.PushVarStory(varStoryName);
    NotifyCommandExecuted();
  }
}
