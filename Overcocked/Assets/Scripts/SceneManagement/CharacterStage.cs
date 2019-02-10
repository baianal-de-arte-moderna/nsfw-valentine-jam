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
    float vCenter = 0.5f;
    float hStep = 1.0f / (float)(characterList.Count + 1);

    float currentPosition = hStep;

    foreach(var character in this.characterList) {
      Transform characterTransform = character.GetComponent<Transform>();
      characterTransform.position = Camera.main.ViewportToWorldPoint(new Vector3(currentPosition,vCenter,1));
      currentPosition += hStep;
    }
  }

  void Update() {}
}
