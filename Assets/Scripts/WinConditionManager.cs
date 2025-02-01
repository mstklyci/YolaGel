using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionManager : MonoBehaviour
{
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private CarWinCondition[] carWinConditions;

    private int carsReachedTarget = 0;
    private LevelManager levelManager;
    AudioManager audioManager;




    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    private void Start()
    {
        gameWinPanel.SetActive(false);
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void CarReachedTarget(string carID, string targetID)
    {
        foreach (CarWinCondition condition in carWinConditions)
        {
            if (condition.carID == carID && condition.targetID == targetID && !condition.hasReached)
            {
                condition.hasReached = true;
                carsReachedTarget++;
                CheckWinCondition();
                return;
            }
        }
    }

    private void CheckWinCondition()
    {
        if (carsReachedTarget == carWinConditions.Length)
        {
            gameWinPanel.SetActive(true);
            PlayerPrefs.SetInt("Tutorial", 1);
            audioManager.PlaySFX(audioManager.win);
            AchievementManager.Instance.currentLevel = levelManager.currentLevelIndex;
            AchievementManager.Instance.AchievementCheckLevelDone();
        }
        if (levelManager != null)
        {
            levelManager.CompleteLevel();
        }
    }
}

[System.Serializable]
public class CarWinCondition
{
    public string carID;
    public string targetID;
    public bool hasReached;
}