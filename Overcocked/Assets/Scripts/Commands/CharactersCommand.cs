// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersCommand : Command
{
  private Dictionary<string, string> characters;

  public CharactersCommand(Dictionary<string, string> characters)
  {
    this.characters = characters;
  }

  public override void Execute(StoryInterpreter storyInterpreter)
  {
    CharacterStage stage = storyInterpreter.GetStage();
    stage.Clear();
    foreach (var character in characters)
    {
      stage.AddCharacter(character.Key, character.Value);
    }
    NotifyCommandExecuted();
  }
}
