// vim: set ts=2 sts=2 sw=2 expandtab:
using UnityEngine;

public class OnClickHandler : MonoBehaviour
{
  void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      GetComponent<OnClickListener>().OnClick();
    }
  }
}
