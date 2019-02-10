// vim: set ts=2 sts=2 sw=2 expandtab:
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
    SpriteRenderer background = storyInterpreter.GetBackground();
    Sprite newBackground = Resources.Load<Sprite>($"Sprites/Backgrounds/{imageName}");

    float vRatio = Screen.height / newBackground.rect.height;
    float hRatio = Screen.width / newBackground.rect.width;

    Transform bgTransform = background.GetComponent<Transform>();
    bgTransform.localScale = new Vector3(hRatio, vRatio, 1);

    background.sprite = newBackground;

    NotifyCommandExecuted();
  }
}
