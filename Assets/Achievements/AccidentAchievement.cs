using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Accident Achievement", menuName = "Achievement/Accident")]
public class AccidentAchievement : AchievementScript
{
    public int _minAccidentCount;
    public override void AchievementCheckMain(bool levelDone)
    {
        string key = "accident_"+SceneManager.GetActiveScene().buildIndex;
        if (PlayerPrefs.GetInt(key,0) >= _minAccidentCount){
            Unlock();
        }
    }
}
