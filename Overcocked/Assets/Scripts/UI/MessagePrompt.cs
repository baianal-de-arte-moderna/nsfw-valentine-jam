// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePrompt : MonoBehaviour
{
  [SerializeField]
  private HistoryPrompt historyPrompt;

  [SerializeField]
  private GameObject messagePanelContainer;

  [SerializeField]
  private Text messageTextComponent;

  [SerializeField]
  private GameObject namePanelContainer;

  [SerializeField]
  private Text nameTextComponent;

  [SerializeField]
  public int textSpeed;

  public delegate void MessageFinishedAction(MessagePrompt messagePrompt);
  public event MessageFinishedAction OnMessageFinished;

  public class Message
  {
    public string Name;
    public string MessageText;
  };

  private int _frameCount;
  private int _cursor;

  private Message _currentMessage;
  public Message CurrentMessage
  {
    get
    {
      return _currentMessage;
    }
    set
    {
      _currentMessage = value;
      if (value != null)
      {
        if (gameObject != null)
        {
          gameObject.SetActive(true);
        }
        if (value.Name != "")
        {
          namePanelContainer.SetActive(true);
          nameTextComponent.text = value.Name;
        }
        else
        {
          namePanelContainer.SetActive(false);
        }
      }
      else
      {
        if (gameObject != null)
        {
          gameObject.SetActive(false);
        }
      }
      messageTextComponent.text = "";
      _frameCount = 0;
      _cursor = 0;
    }
  }

  private List<Message> history = new List<Message>();

  public void OnClick()
  {
    if (historyPrompt.IsOpen())
    {
      return;
    }

    if (_currentMessage != null &&
        _cursor < _currentMessage.MessageText.Length)
    {
      messageTextComponent.text = _currentMessage.MessageText;
      _cursor = _currentMessage.MessageText.Length;
    }
    else
    {
      history.Add(_currentMessage);
      OnMessageFinished?.Invoke(this);
    }
  }

  public void OnHistoryClick()
  {
    historyPrompt.Open(history);
  }

  void FixedUpdate()
  {
    if (_currentMessage != null
        && _cursor < _currentMessage.MessageText.Length
        && (_frameCount++ % textSpeed) == 0)
    {
      char nextChar = _currentMessage.MessageText[_cursor];
      do
      {
        messageTextComponent.text += nextChar;
        _cursor++;
      } while ((_cursor < _currentMessage.MessageText.Length) &&
          (nextChar = _currentMessage.MessageText[_cursor]) == ' ');
    }
  }
}
