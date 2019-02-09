// vim: set ts=2 sts=2 sw=2 expandtab:
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessagePrompt : MonoBehaviour
{
  public string _initialMessage;
  public Text _textComponent;
  public int _textSpeed;

  private string _message;
  private int _frameCount;
  private int _cursor;
  public string Message {
    get {
      return _message;
    }
    set {
      _message = value;
      _textComponent.text = "";
      _frameCount = 0;
      _cursor = 0;
    }
  }

  public void FastForward() {
    _textComponent.text = _message;
    _cursor = _message.Length;
  }

  // Start is called before the first frame update
  void Start()
  {
    Message = this._initialMessage;
  }

  // Update is called once per frame
  void Update() {
  }

  // Update is called once per frame
  void FixedUpdate()
  {
    if (_cursor < _message.Length && (_frameCount++ % _textSpeed) == 0) {
      char nextChar = _message.ToCharArray()[_cursor];
      do {
        _textComponent.text += nextChar;
        _cursor++;
      } while ((_cursor < _message.Length) &&
          (nextChar = _message.ToCharArray()[_cursor]) == ' ');
    }
  }
}
