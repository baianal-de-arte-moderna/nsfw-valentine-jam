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
    SpriteRenderer background = storyInterpreter.GetBackground();
    Sprite newBackground = Resources.Load<Sprite>($"Sprites/Backgrounds/{imageName}");

    float vRatio = (float)Screen.height / newBackground.rect.height;
    float hRatio = (float)Screen.width  / newBackground.rect.width;

    Transform bgTransform = background.GetComponent<Transform>();
    bgTransform.localScale = new Vector3(hRatio, vRatio, 1);

    background.sprite = newBackground;

    NotifyCommandExecuted();
  }
}
