// vim: set ts=2 sts=2 sw=2 expandtab:
using MiniJSON;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

public class StoryInterpreter : MonoBehaviour
{
  [SerializeField]
  private Canvas canvas;

  [SerializeField]
  private TextAsset defaultEndingStory;

  [SerializeField]
  private TextAsset initialStory;

  [SerializeField]
  private SpriteRenderer background;

  [SerializeField]
  private CharacterStage characterStage;

  [SerializeField]
  private MusicPlayer musicPlayer;

  [SerializeField]
  private OnClickHandler onClickHandler;

  public SpriteRenderer GetBackground()
  {
    return background;
  }

  public Canvas GetCanvas()
  {
    return canvas;
  }

  public MusicPlayer GetMusicPlayer()
  {
    return musicPlayer;
  }

  public OnClickHandler GetOnClickHandler()
  {
    return onClickHandler;
  }

  public CharacterStage GetStage()
  {
    return characterStage;
  }

  private List<Command> commands;
  private Dictionary<string, string> variables = new Dictionary<string, string>();

  private void Start()
  {
    MessagePrompt messagePrompt = canvas.GetComponentInChildren<MessagePrompt>();
    onClickHandler.OnMouseClicked += messagePrompt.OnClick;

    commands = LoadStoryCommands(initialStory);
    Invoke("ExecuteCommand", 0.1f);
  }

  private List<Command> LoadStoryCommands(string storyName)
  {
    TextAsset story = Resources.Load<TextAsset>($"Stories/{storyName}");
    if (story == null)
    {
      story = defaultEndingStory;
    }
    return LoadStoryCommands(story);
  }

  private List<Command> LoadStoryCommands(TextAsset story)
  {
    List<CommandData> storyData = new List<CommandData>();

    List<object> storyJson = (List<object>)Json.Deserialize(story.text);
    foreach (Dictionary<string, object> commandJson in storyJson)
    {
      CommandData commandData = new CommandData
      {
        command = (string)commandJson["command"],
        parameters = new Dictionary<string, string>()
      };

      Dictionary<string, object> parameterJson = (Dictionary<string, object>)commandJson["parameters"];
      foreach (string parameterName in parameterJson.Keys)
      {
        commandData.parameters[parameterName] = (string)parameterJson[parameterName];
      }

      storyData.Add(commandData);
    }

    return storyData.Select(CommandFactory.CreateCommand).ToList();
  }

  public void PushStory(string storyName)
  {
    commands.InsertRange(0, LoadStoryCommands(storyName));
  }

  public void PushVarStory(string varStoryName)
  {
    string storyName = varStoryName;
    foreach (string key in variables.Keys)
    {
      storyName = Regex.Replace(storyName, $"\\[{key}\\]", variables[key]);
    }
    PushStory(storyName);
  }

  public void SetVariable(string key, string value)
  {
    variables[key] = value;
  }

  private void ExecuteCommand()
  {
    if (commands.Count > 0)
    {
      Command command = commands[0];
      commands.RemoveAt(0);
      command.OnCommandExecuted += ExecuteCommand;
      command.Execute(this);
    }
  }
}
