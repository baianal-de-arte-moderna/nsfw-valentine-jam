using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStage : MonoBehaviour
{
  private List<CharacterScript> characterList;

  void Start()
  {
    characterList = new List<CharacterScript>();
  }

  public void Clear()
  {
    foreach (CharacterScript character in characterList)
    {
      Destroy(character.gameObject);
    }
    characterList.Clear();
  }

  public void AddCharacter(string characterName, string characterState)
  {
    CharacterScript character = Instantiate(Resources.Load<CharacterScript>("Characters/" + characterName));

    characterList.Add(character);

    if (Enum.TryParse(characterState, out CharacterScript.CharacterState state))
    {
      character.Emote(state);
    }
    RearrangeCharacters();
  }

  private void RearrangeCharacters()
  {
    float vCenter = 0.5f;
    float hStep = 1.0f / (characterList.Count + 1);

    float currentPosition = hStep;

    foreach (var character in characterList)
    {
      Transform characterTransform = character.GetComponent<Transform>();
      characterTransform.position = Camera.main.ViewportToWorldPoint(new Vector3(currentPosition, vCenter, 1));
      currentPosition += hStep;
    }
  }

  void Update() { }
}
