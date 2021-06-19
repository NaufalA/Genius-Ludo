using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : MonoBehaviour
{
  public Text InfoText;

  public void ShowMessage(string message)
  {
    InfoText.text = message;
  }
}
