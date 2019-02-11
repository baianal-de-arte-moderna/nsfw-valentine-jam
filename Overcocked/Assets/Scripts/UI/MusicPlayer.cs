using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicPlayer : MonoBehaviour
{
  private const float TRANSITION_DURATION = 2.0f;

  [SerializeField]
  private AudioMixer audioMixer;

  private List<AudioSource> audioSources;

  private void Start()
  {
    audioSources = new List<AudioSource>(GetComponents<AudioSource>());
  }

  public void TransitionTo(string snapshotName)
  {
    AudioSource audioSource = audioSources.Find(
      (AudioSource a) =>
      {
        AudioMixerGroup audioMixerGroup = a.outputAudioMixerGroup;
        return audioMixerGroup.name == snapshotName;
      }
    );

    if (audioSource != null)
    {
      audioSource.time = audioSource.clip.length - (TRANSITION_DURATION / 2);
    }

    AudioMixerSnapshot snapshot = audioMixer.FindSnapshot(snapshotName);
    snapshot.TransitionTo(TRANSITION_DURATION);
  }
}
