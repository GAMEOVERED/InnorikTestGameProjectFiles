using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public GameObject worldUI, homeUI;

    public void hideUI()
    {
        worldUI.SetActive(false);
        homeUI.SetActive(false);
    }

    public void showUI()
    {
        TouchInputManager.instance.levelText.text = "Level " + TouchInputManager.instance.levelNo.ToString();
        worldUI.SetActive(true);
        homeUI.SetActive(true);
    }
}
