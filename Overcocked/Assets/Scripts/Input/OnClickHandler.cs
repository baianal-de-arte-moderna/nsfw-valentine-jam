// vim: set ts=2 sts=2 sw=2 expandtab:
using UnityEngine;

public class OnClickHandler : MonoBehaviour
{
  public delegate void MouckClickAction();
  public event MouckClickAction OnMouseClicked;

  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      OnMouseClicked?.Invoke();
    }
  }
}
