using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
  public AudioMixer musicMixer;

  public void TransitionTo(string snapshotName)
  {
    AudioMixerSnapshot snapshot = musicMixer.FindSnapshot(snapshotName);
    snapshot.TransitionTo(0.2f);
  }
}
