using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    [SerializeField] private GameObject credit;
    [SerializeField] private GameObject control;

    void Start()
    {
        menu.SetActive(true);
        credit.SetActive(false);
        control.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SoundManager.instance.Play(SoundManager.SoundName.Click);
            menu.SetActive(true);
            credit.SetActive(false);
            control.SetActive(false);
        }
    }

    public void StartGame()
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        SoundManager.instance.Stop(SoundManager.SoundName.BGM1);
        SceneManager.LoadScene(1);
    }

    public void Credit()
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        menu.SetActive(false);
        credit.SetActive(true);
        control.SetActive(false);
    }

    public void Control()
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        menu.SetActive(false);
        credit.SetActive(false);
        control.SetActive(true);
    }
    
    public void ReturnToMenu()
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        SceneManager.LoadScene(0);
        menu.SetActive(true);
        credit.SetActive(false);
        control.SetActive(false);
    }

    public void QuitGame()
    {
        SoundManager.instance.Play(SoundManager.SoundName.Click);
        Application.Quit();
    }
}
