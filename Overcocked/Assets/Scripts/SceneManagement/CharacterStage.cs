using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStage : MonoBehaviour
{
  private List<CharacterScript> characterList = new List<CharacterScript>();

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
    CharacterScript characterScript = Resources.Load<CharacterScript>("Characters/" + characterName);
    if (characterScript != null)
    {
      CharacterScript character = Instantiate(characterScript);
      characterList.Add(character);

      if (Enum.TryParse(characterState, out CharacterScript.CharacterState state))
      {
        character.Emote(state);
      }
      RearrangeCharacters();
    }
    else
    {
      Debug.LogError($"Couldn't load character prefab: {characterName}, {characterState}");
    }
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
