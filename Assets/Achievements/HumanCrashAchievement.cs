using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Human Crash", menuName = "Achievement/Human Crash")]

public class HumanCrashAchievement : AchievementScript
{
    public override void AchievementCheckMain(bool levelDone)
    {
        if (AchievementManager.Instance.humanCrash == true){
            Unlock();
        }
    }
}
