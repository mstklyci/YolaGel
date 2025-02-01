using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelectManager : MonoBehaviour
{
    [SerializeField] Button[] levelButtons;
    [SerializeField] GameObject[] lockIcons;
    AudioManager audioManager;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    private void Start()
    {
        LoadLevelStatus();
    }

    private void LoadLevelStatus()
    {
        int maxUnlockedLevel = PlayerPrefs.GetInt("MaxUnlockedLevel" , 1);

        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i + 1;

            if (levelIndex <= maxUnlockedLevel)
            {
                levelButtons[i].interactable = true;
                lockIcons[i].SetActive(false);
                levelButtons[i].onClick.AddListener(() => LoadLevel(levelIndex));
            }
            else
            {
                levelButtons[i].interactable = false;
                lockIcons[i].SetActive(true);
            }
        }
    }

    public void ResetProgress()
    {
        PlayerPrefs.SetInt("MaxUnlockedLevel", 1); // En düşük seviyeye sıfırla
        PlayerPrefs.Save(); // Değişiklikleri kaydet

        // Seviye durumunu yeniden yükleyin
        LoadLevelStatus();
    }

    private void LoadLevel(int levelIndex)
    {
        if (levelIndex != 1)
        {
            SceneManager.LoadScene(levelIndex + 1);
            audioManager.PlaySFX(audioManager.buttonClick);
        }
        Time.timeScale = 1;
    }
}