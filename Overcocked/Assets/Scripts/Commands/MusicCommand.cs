// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections.Generic;

public class MusicCommand : Command
{
  private readonly string snapshotName;

  public MusicCommand(Dictionary<string, string> parameters)
  {
    snapshotName = parameters["snapshotName"];
  }

  public override void Execute(StoryInterpreter storyInterpreter)
  {
    MusicPlayer musicPlayer = storyInterpreter.GetMusicPlayer();
    musicPlayer.TransitionTo(snapshotName);
    NotifyCommandExecuted();
  }
}
