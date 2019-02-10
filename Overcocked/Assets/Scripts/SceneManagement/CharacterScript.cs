using UnityEngine;

public class CharacterScript : MonoBehaviour {
  public enum CharacterState {
    Idle
  };

  public string _characterName;

  public void Emote(CharacterState state) {
    Debug.Log("DoNothing");
  }
}
