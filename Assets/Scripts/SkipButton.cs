using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SkipButton : MonoBehaviour
{
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    public void Level1Skip()
    {
        AudioManager.instance.musicSource.Pause();
        audioManager.PlaySFX(audioManager.buttonClick);
        SceneManager.LoadScene("1_Level");
        Time.timeScale = 1;
    }
    public void LastLevelSkip()
    {
        AudioManager.instance.musicSource.Pause();
        audioManager.PlaySFX(audioManager.buttonClick);
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1;
    }
}