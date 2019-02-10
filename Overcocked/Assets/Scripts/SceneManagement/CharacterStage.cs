using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStage : MonoBehaviour
{
  private List<CharacterScript> characterList;

  void Start() {
    this.characterList = new List<CharacterScript>();
  }

  public void Clear() {
    this.characterList.Clear();
  }

  public void AddCharacter(string characterName, string characterState) {
    CharacterScript character = Instantiate(Resources.Load<CharacterScript>("Characters/" + characterName));

    this.characterList.Add(character);

    CharacterScript.CharacterState state;
    if (Enum.TryParse<CharacterScript.CharacterState>(characterState,
          out state)) {
      character.Emote(state);
    }
    RearrangeCharacters();
  }

  private void RearrangeCharacters() {
    int vCenter = Screen.height / 2;
    int hStep = Screen.width / (characterList.Count + 1);

    int currentPosition = hStep;

    foreach(var character in this.characterList) {
      Transform characterTransform = character.GetComponent<Transform>();
      characterTransform.position = new Vector3(currentPosition,vCenter,1);
      currentPosition += hStep;
    }
  }

  void Update() {}
}
