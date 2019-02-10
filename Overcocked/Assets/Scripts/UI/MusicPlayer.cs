using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
  public AudioMixerSnapshot fuckSnapshot;
  public AudioMixerSnapshot muteSnapshot;
  public AudioMixerSnapshot routineSnapshot;
  public AudioMixerSnapshot themeSnapshot;

  public void TransitionTo(string snapshotName)
  {
    AudioMixerSnapshot snapshot;
    switch (snapshotName)
    {
      case "fuck":
        snapshot = fuckSnapshot;
        break;
      case "mute":
        snapshot = muteSnapshot;
        break;
      case "theme":
        snapshot = themeSnapshot;
        break;
      default:
        snapshot = routineSnapshot;
        break;
    }
    snapshot.TransitionTo(0.2f);
  }
}
