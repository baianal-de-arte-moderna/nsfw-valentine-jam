public static class CommandFactory
{
  public static Command CreateCommand(CommandData commandData)
  {
    switch (commandData.command)
    {
      case "message":
        return new MessageCommand(commandData.parameters);
    }

    return new InvalidCommand(commandData);
  }
}