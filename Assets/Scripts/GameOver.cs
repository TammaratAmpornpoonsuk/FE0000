using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public void ReturnToMenu()
    {
        if (GameObject.FindGameObjectWithTag("GameOver") == null)
        {
            SoundManager.instance.Play(SoundManager.SoundName.Click);
            SoundManager.instance.Stop(SoundManager.SoundName.BGM2);
            SceneManager.LoadScene(0);
        }
    }
}
