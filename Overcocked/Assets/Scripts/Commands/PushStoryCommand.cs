using System.Collections.Generic;
using UnityEngine;

public class PushStoryCommand : Command
{
  private readonly string storyName;

  public PushStoryCommand(Dictionary<string, string> parameters)
  {
    storyName = parameters["storyName"];
  }

  public override void Execute(StoryInterpreter storyInterpreter, Canvas canvas)
  {
    storyInterpreter.PushStory(storyName);
    NotifyCommandExecuted();
  }
}
