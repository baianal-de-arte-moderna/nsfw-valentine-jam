// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePrompt : MonoBehaviour
{
  public delegate void MessageFinishedAction();
  public event MessageFinishedAction OnMessageFinished;

  public class Message
  {
    public string Name;
    public string MessageText;
  };

  public GameObject _namePanelContainer;
  public Text _nameTextComponent;
  public GameObject _messagePanelContainer;
  public Text _messageTextComponent;

  public int _textSpeed;

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
        if (value.Name != "")
        {
          _namePanelContainer.SetActive(true);
          _nameTextComponent.text = value.Name;
        }
        else
        {
          _namePanelContainer.SetActive(false);
        }
      }
      _messageTextComponent.text = "";
      _frameCount = 0;
      _cursor = 0;
    }
  }

  public void FastForward()
  {
    if (_cursor < _currentMessage.MessageText.Length)
    {
      _messageTextComponent.text = _currentMessage.MessageText;
      _cursor = _currentMessage.MessageText.Length;
    }
  }

  // Update is called once per frame
  void Update()
  {
    if (_cursor >= _currentMessage.MessageText.Length
        && OnMessageFinished != null)
    {
      OnMessageFinished();
      OnMessageFinished = null;
    }
  }

  void FixedUpdate()
  {
    if (_currentMessage != null
        && _cursor < _currentMessage.MessageText.Length
        && (_frameCount++ % _textSpeed) == 0)
    {
      char nextChar = _currentMessage.MessageText.ToCharArray()[_cursor];
      do
      {
        _messageTextComponent.text += nextChar;
        _cursor++;
      } while ((_cursor < _currentMessage.MessageText.Length) &&
          (nextChar = _currentMessage.MessageText.ToCharArray()[_cursor]) == ' ');
    }
  }
}
