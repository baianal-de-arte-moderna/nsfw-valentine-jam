using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HistoryPrompt : MonoBehaviour
{
  [SerializeField]
  private GameObject panel;

  [SerializeField]
  private ScrollRect scrollView;

  [SerializeField]
  private GameObject entryPrefab;

  private float entryHeight;
  private List<GameObject> entries = new List<GameObject>();

  private void Start()
  {
    RectTransform entryRect = entryPrefab.transform as RectTransform;
    entryHeight = entryRect.rect.height;
  }

  public void Open(List<MessagePrompt.Message> messages)
  {
    RectTransform contentRect = scrollView.content.transform as RectTransform;
    contentRect.sizeDelta = new Vector2(contentRect.sizeDelta.x, messages.Count * entryHeight);

    for (int i = 0; i < messages.Count; ++i)
    {
      MessagePrompt.Message message = messages[i];

      // Configure position
      GameObject entry = Instantiate(entryPrefab, scrollView.content);
      RectTransform entryRect = entry.transform as RectTransform;
      entryRect.localPosition = new Vector2(0, -i * entryHeight);

      // Configure contents
      HistoryEntry historyEntry = entry.GetComponent<HistoryEntry>();
      historyEntry.Configure(message);

      entries.Add(entry);
    }

    panel.SetActive(true);
  }

  public void Close()
  {
    panel.SetActive(false);

    foreach (GameObject entry in entries)
    {
      Destroy(entry);
    }
    entries.Clear();
  }

  public bool IsOpen()
  {
    return panel.activeSelf;
  }
}
