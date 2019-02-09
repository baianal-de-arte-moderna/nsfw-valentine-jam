using System.Collections.Generic;
using UnityEngine;

public class BackgroundCommand : Command
{
  private readonly string imageName;

  public BackgroundCommand(Dictionary<string, string> parameters)
  {
    imageName = parameters["imageName"];
  }

  public override void Execute(StoryInterpreter storyInterpreter, Canvas canvas)
  {
    Background background = canvas.GetComponentInChildren<Background>();
    background.image.sprite = Resources.Load<Sprite>($"Sprites/Backgrounds/{imageName}");
    NotifyCommandExecuted();
  }
}
