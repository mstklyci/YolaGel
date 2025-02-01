using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "FirstTryAchievement", menuName = "Achievement/First Try")]
public class FirstTryAchievement : AchievementScript
{
    public override void AchievementCheckMain(bool levelDone)
    {
        string key = "tryCount_"+SceneManager.GetActiveScene().buildIndex;
        if (levelDone){
            if (PlayerPrefs.GetInt(key,0) == 0){
                Unlock();
            }
        } 
    }
}
