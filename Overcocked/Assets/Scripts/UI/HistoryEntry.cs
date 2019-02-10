using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryEntry : MonoBehaviour
{
  [SerializeField]
  private Text messageView;

  [SerializeField]
  private Text nameView;

  public void Configure(MessagePrompt.Message message)
  {
    nameView.text = message.Name;
    messageView.text = message.MessageText;
  }
}
