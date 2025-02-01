using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public int currentLevelIndex;
    public int maxUnlockedLevel;

    void Awake()
    {
        maxUnlockedLevel = PlayerPrefs.GetInt("MaxUnlockedLevel", maxUnlockedLevel);
    }

    public void CompleteLevel()
    {
        if (currentLevelIndex >= maxUnlockedLevel)
        {
            maxUnlockedLevel = currentLevelIndex + 1;
            PlayerPrefs.SetInt("MaxUnlockedLevel", maxUnlockedLevel);
            PlayerPrefs.Save();
        }
    }
}