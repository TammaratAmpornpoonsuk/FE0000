using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour
{
    [SerializeField] private Light dLight;
    [SerializeField] private Image[] hearts;
    [SerializeField] private Image[] leftBossHearts;
    [SerializeField] private Image[] midBossHearts;
    [SerializeField] private Image[] rightBossHearts;

    [SerializeField] private TMP_Text resumeText;
    [SerializeField] private TMP_Text backText;
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private TMP_Text returnToMenuText;
    
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject leftBoss;
    [SerializeField] private GameObject midBoss;
    [SerializeField] private GameObject rightBoss;
    
    [SerializeField] private Transform playerTransform;
    
    [SerializeField] private Material leftBossMaterial;
    [SerializeField] private Material midBossMaterial;
    [SerializeField] private Material rightBossMaterial;
    
    [SerializeField] private float live;
    [SerializeField] private float leftBossLive;
    [SerializeField] private float midBossLive;
    [SerializeField] private float rightBossLive;

    private Vector3 leftBossPos;
    private Vector3 midBossPos;
    private Vector3 rightBossPos;
    
    private bool isDead;
    private bool pause;
    
    void Start()
    {
        SoundManager.instance.Play(SoundManager.SoundName.BGM2);
        dLight = dLight.GetComponent<Light>();
        for (int i = 0; i < leftBossHearts.Length; i++)
        {
            if (i < leftBossLive)
            {
                leftBossHearts[i].enabled = true;
            }
            else
            {
                leftBossHearts[i].enabled = false;
            }
        }
        for (int i = 0; i < midBossHearts.Length; i++)
        {
            if (i < midBossLive)
            {
                midBossHearts[i].enabled = true;
            }
            else
            {
                midBossHearts[i].enabled = false;
            }
        }
        for (int i = 0; i < rightBossHearts.Length; i++)
        {
            if (i < rightBossLive)
            {
                rightBossHearts[i].enabled = true;
            }
            else
            {
                rightBossHearts[i].enabled = false;
            }
        }
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < live)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        
        Cursor.lockState = CursorLockMode.Locked;
        Instantiate(player, playerTransform.position, playerTransform.rotation);

        leftBossPos = leftBoss.transform.position;
        midBossPos = midBoss.transform.position;
        rightBossPos = rightBoss.transform.position;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pause = !pause;
            Pause();
        }

        if (GameObject.FindGameObjectWithTag("GameOver") == null)
        {
            SoundManager.instance.Stop(SoundManager.SoundName.BGM2);
            gameOverText.alignment = TextAlignmentOptions.Center;
            gameOverText.fontSize = 86;
            gameOverText.text = "Game Over";
            returnToMenuText.alignment = TextAlignmentOptions.Center;
            returnToMenuText.fontSize = 86;
            returnToMenuText.text = "Return";
            Cursor.lockState = CursorLockMode.None;
            StopAllCoroutines();
        }

        if (GameObject.FindGameObjectWithTag("Enemy") == null)
        {
            Destroy(GameObject.FindGameObjectWithTag("GameOver"));
            StopAllCoroutines();
        }
    }

    void Pause()
    {
        if (pause)
        {
            SoundManager.instance.Pause(SoundManager.SoundName.BGM2);
            resumeText.text = "RESUME";
            backText.text = "BACK";
            dLight.color = new Color(0.5f, 0.5f, 0.5f, 1);
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            SoundManager.instance.UnPause(SoundManager.SoundName.BGM2);
            resumeText.text = null;
            backText.text = null;
            dLight.color = Color.white;
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void ResumeButton()
    {
        if (pause)
        {
            SoundManager.instance.Play(SoundManager.SoundName.Click);
            pause = false;
            Pause();
        }
    }

    public void BackButton()
    {
        if (pause)
        {
            SoundManager.instance.Play(SoundManager.SoundName.Click);
            SoundManager.instance.Stop(SoundManager.SoundName.BGM2);
            pause = false;
            Pause();
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void Attack(string nameBoss)
    {
        if (nameBoss == "left")
        {
            leftBossLive -= 1;
            for (int i = 0; i < leftBossHearts.Length; i++)
            {
                if (i < leftBossLive)
                {
                    leftBossHearts[i].enabled = true;
                }
                else
                {
                    leftBossHearts[i].enabled = false;
                }
            }
            if (leftBossLive <= 0)
            {
                SoundManager.instance.Play(SoundManager.SoundName.BossDead);
                leftBossMaterial.color = new Color(0.3f, 0.3f, 0.3f, 1f);
                leftBoss.tag = "Untagged";
                Destroy(leftBoss.GetComponent<EnemyController>());
                leftBoss.transform.position = leftBossPos;
            }
        }

        if (nameBoss == "mid")
        {
            midBossLive -= 1;
            for (int i = 0; i < midBossHearts.Length; i++)
            {
                if (i < midBossLive)
                {
                    midBossHearts[i].enabled = true;
                }
                else
                {
                    midBossHearts[i].enabled = false;
                }
            }
            if (midBossLive <= 0)
            {
                SoundManager.instance.Play(SoundManager.SoundName.BossDead);
                midBossMaterial.color = new Color(0.3f, 0.3f, 0.3f, 1);
                midBoss.tag = "Untagged";
                Destroy(midBoss.GetComponent<EnemyController>());
                midBoss.transform.position = midBossPos;
            }
        }

        if (nameBoss == "right")
        {
            rightBossLive -= 1;
            for (int i = 0; i < rightBossHearts.Length; i++)
            {
                if (i < rightBossLive)
                {
                    rightBossHearts[i].enabled = true;
                }
                else
                {
                    rightBossHearts[i].enabled = false;
                }
            }
            if (rightBossLive <= 0)
            {
                SoundManager.instance.Play(SoundManager.SoundName.BossDead);
                rightBossMaterial.color = new Color(0.3f, 0.3f, 0.3f, 1);
                rightBoss.tag = "Untagged";
                Destroy(rightBoss.GetComponent<EnemyController>());
                rightBoss.transform.position = rightBossPos;
            }
        }
    }
    
    public void Live()
    {
        live -= 1;
        SoundManager.instance.Play(SoundManager.SoundName.Dead);
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < live)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
        
        isDead = true;
        
        if (isDead && live > 0)
        {
            StartCoroutine(WaitForSpawn());
            isDead = false;
        }
        if (live <= 0)
        {
            gameOverText.alignment = TextAlignmentOptions.Center;
            gameOverText.fontSize = 96;
            gameOverText.text = "Game Over";
            Destroy(GameObject.FindGameObjectWithTag("GameOver"));
            StopAllCoroutines();
        }
    }

    public void GetHeart()
    {
        live += 1;
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < live)
            {
                hearts[i].enabled = true;
            }
        }
        if (live > 5)
        {
            live = 5;
        }
    }

    IEnumerator WaitForSpawn()
    {
        yield return new WaitForSeconds(2);
        Instantiate(player, playerTransform.position, playerTransform.rotation);
        SoundManager.instance.Play(SoundManager.SoundName.Born);
    }
}
