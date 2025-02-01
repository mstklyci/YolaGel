using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelAchievement", menuName = "Achievement/Level")]
public class LevelAchievement : AchievementScript
{
    public int _targetLevel;

    public override void AchievementCheckMain(bool levelDone)
    {
        if (AchievementManager.Instance.currentLevel >= _targetLevel){
            Unlock();
        }
    }
}
