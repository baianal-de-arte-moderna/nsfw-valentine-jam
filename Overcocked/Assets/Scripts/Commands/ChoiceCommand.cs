// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections.Generic;
using UnityEngine;

public class ChoiceCommand : Command
{
  private readonly string button0Text;
  private readonly string button0StoryName;
  private readonly string button1Text;
  private readonly string button1StoryName;

  private ChoicePrompt.ButtonPressedAction button0PressedAction;
  private ChoicePrompt.ButtonPressedAction button1PressedAction;

  public ChoiceCommand(Dictionary<string, string> parameters)
  {
    button0Text = parameters["button0Text"];
    button0StoryName = parameters["button0StoryName"];
    button1Text = parameters["button1Text"];
    button1StoryName = parameters["button1StoryName"];
  }

  public override void Execute(StoryInterpreter storyInterpreter)
  {
    Canvas canvas = storyInterpreter.GetCanvas();
    ChoicePrompt choicePrompt = canvas.GetComponentInChildren<ChoicePrompt>();

    choicePrompt.panel.SetActive(true);

    button0PressedAction = () =>
    {
      storyInterpreter.PushStory(button0StoryName);
      DismissChoicePrompt(choicePrompt);
    };
    choicePrompt.button0Text.text = button0Text;
    choicePrompt.OnButton0Pressed += button0PressedAction;

    button1PressedAction = () =>
    {
      storyInterpreter.PushStory(button1StoryName);
      DismissChoicePrompt(choicePrompt);
    };
    choicePrompt.button1Text.text = button1Text;
    choicePrompt.OnButton1Pressed += button1PressedAction;
  }

  private void DismissChoicePrompt(ChoicePrompt choicePrompt)
  {
    choicePrompt.panel.SetActive(false);
    choicePrompt.OnButton0Pressed -= button0PressedAction;
    choicePrompt.OnButton1Pressed -= button1PressedAction;

    NotifyCommandExecuted();
  }
}
