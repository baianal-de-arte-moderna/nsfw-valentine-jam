using UnityEngine;
using UnityEngine.UI;

public class ChoicePrompt : MonoBehaviour
{
  public GameObject panel;
  public Text panelText;
  public Text button0Text;
  public Text button1Text;

  public delegate void ButtonPressedAction();
  public event ButtonPressedAction OnButton0Pressed;
  public event ButtonPressedAction OnButton1Pressed;

  public void PressButton0()
  {
    OnButton0Pressed();
  }

  public void PressButton1()
  {
    OnButton1Pressed();
  }
}
