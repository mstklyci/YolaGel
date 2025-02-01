using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour
{
    [HideInInspector]
    public bool carCrash = false;
    [HideInInspector]
    public bool humanCrash = false;
    [HideInInspector]
    public int currentLevel = 0;

    [HideInInspector]
    public List<AchievementScript> achievementScripts = new List<AchievementScript>();

    [HideInInspector]
    public AchievementManagerHolder achievementManagerHolder;

    public static AchievementManager Instance;
    private void Awake() {
        if (Instance != null && Instance != this){
            Destroy(this.gameObject);
        }else {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneChange;
    }

    void Update()
    {
        
    }
    private void OnSceneChange(Scene scene,LoadSceneMode loadSceneMode) {
        humanCrash = false;
        carCrash = false;
        AchievementCheck();
    }
    public void AchievementCheck(){
        for (int i = 0; i < achievementScripts.Count; i++)
        {
            achievementScripts[i].AchievementCheck(false);
        }
    }
     public void AchievementCheckLevelDone(){
        for (int i = 0; i < achievementScripts.Count; i++)
        {
            achievementScripts[i].AchievementCheck(true);
        }
    }
    public void IncreaseTryCount(int sceneIndex){
        string key = "tryCount_"+sceneIndex;
        PlayerPrefs.SetInt(key,PlayerPrefs.GetInt(key,0) + 1);
    }
    public void IncreaseAccident(bool carCrashNew,bool humanCrashNew){
        if (this.carCrash == false && this.humanCrash == false){
            string key = "accident_"+SceneManager.GetActiveScene().buildIndex;
            PlayerPrefs.SetInt(key,PlayerPrefs.GetInt(key,0) + 1);
        }
        this.carCrash = carCrashNew ? true : this.carCrash;
        this.humanCrash = humanCrashNew ? true : this.humanCrash;
    }
}
