// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections.Generic;

[System.Serializable]
public class CommandData
{
  public string command;
  public Dictionary<string, string> parameters;
}
