using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarCrashAchievement", menuName = "Achievement/Car Crash")]
public class CarCrashAchievement : AchievementScript
{
    public override void AchievementCheckMain(bool levelDone)
    {
        if (AchievementManager.Instance.carCrash == true){
            Unlock();
        }
    }
}
