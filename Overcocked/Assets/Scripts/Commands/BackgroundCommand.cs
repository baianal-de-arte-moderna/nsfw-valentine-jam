// vim: set ts=2 sts=2 sw=2 expandtab:
using System;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundCommand : Command
{
  private readonly string imageName;

  public BackgroundCommand(Dictionary<string, string> parameters)
  {
    imageName = parameters["imageName"];
  }

  public override void Execute(StoryInterpreter storyInterpreter)
  {
    Sprite newBackground = Resources.Load<Sprite>($"Sprites/Backgrounds/{imageName}");
    if (newBackground == null)
    {
      newBackground = Resources.Load<Sprite>("Sprites/Backgrounds/Black screen");
    }

    float originalRatio = 16f / 9f;
    float newRatio = (float)Screen.width / (float)Screen.height;

    SpriteRenderer background = storyInterpreter.GetBackground();
    background.transform.localScale = new Vector3(newRatio / originalRatio, 1, 1);
    background.sprite = newBackground;

    NotifyCommandExecuted();
  }
}
