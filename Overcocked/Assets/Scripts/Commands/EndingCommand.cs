using UnityEngine.SceneManagement;

public class EndingCommand : Command
{
  public override void Execute(StoryInterpreter storyInterpreter)
  {
    SceneManager.LoadScene("MainMenu");
  }
}
