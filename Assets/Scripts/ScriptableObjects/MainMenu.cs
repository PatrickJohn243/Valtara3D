using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public AudioSource tapButton;
    public AudioClip tapClip;
    public GameObject healthBar;
    public Camera cameraUI;
    public GameObject mainMenu;
    private void Awake()
    {
        healthBar.SetActive(false);
    }
    public void StartGame()
    {
        healthBar.SetActive(true);
        cameraUI.enabled = false;
        mainMenu.SetActive(false);
        PlayAudio();
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    void PlayAudio()
    {
        tapButton.clip = tapClip;
        tapButton.Play();
    }
}
