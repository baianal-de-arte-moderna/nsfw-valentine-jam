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
      CharacterScript character = Instantiate(characterScript, transform);
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
    if (characterList.Count > 0)
    {
      float width = Screen.width;
      float hStep = width / (characterList.Count + 1);

      float currentPosition = hStep;

      foreach (var character in characterList)
      {
        Transform characterTransform = character.GetComponent<Transform>();
        Vector3 oldPosition = Camera.main.WorldToScreenPoint(characterTransform.position);
        Vector3 newPosition = new Vector3(currentPosition, oldPosition.y, oldPosition.z);
        characterTransform.position = Camera.main.ScreenToWorldPoint(newPosition);
        currentPosition += hStep;
      }
    }
  }
}
