public static class CommandFactory
{
  public static Command CreateCommand(CommandData commandData)
  {
    switch (commandData.command)
    {
      case "background":
        return new BackgroundCommand(commandData.parameters);
      case "choice":
        return new ChoiceCommand(commandData.parameters);
      case "message":
        return new MessageCommand(commandData.parameters);
      case "music":
        return new MusicCommand(commandData.parameters);
      case "push_story":
        return new PushStoryCommand(commandData.parameters);
      case "hide_prompt":
        return new HideMessagePromptCommand();
      case "characters":
        return new CharactersCommand(commandData.parameters);
      case "push_var_story":
        return new PushVarStoryCommand(commandData.parameters);
      case "set_variable":
        return new SetVariableCommand(commandData.parameters);
    }

    return new InvalidCommand(commandData);
  }
}
