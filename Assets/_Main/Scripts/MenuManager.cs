using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject homeUI, worldUI; 
    public AudioSource audioSource;
    public Sprite audioOn, audioOff;
    public Image sound;

    private bool isOn = true;
    public void GoBack()
    {
        homeUI.SetActive(false);
        worldUI.SetActive(false);
        TouchInputManager.instance.canOpen = true;
        TouchInputManager.instance.bookObject.GetComponent<Animator>().SetTrigger("CloseBook");
        TouchInputManager.instance.onScreenText.text = "Touch to OPEN";
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlaySound()
    {
        isOn = !isOn;
        if(isOn)
        {
            sound.sprite = audioOn;
        }
        else
        {
            sound.sprite = audioOff;
        }

        audioSource.enabled = isOn;
    }

}
