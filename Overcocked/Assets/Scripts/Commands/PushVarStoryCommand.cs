using System.Collections.Generic;
using UnityEngine;

public class PushVarStoryCommand : Command
{
  private readonly string varStoryName;

  public PushVarStoryCommand(Dictionary<string, string> parameters)
  {
    varStoryName = parameters["varStoryName"];
  }

  public override void Execute(StoryInterpreter storyInterpreter, Canvas canvas)
  {
    storyInterpreter.PushVarStory(varStoryName);
    NotifyCommandExecuted();
  }
}
