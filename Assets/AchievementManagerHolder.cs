using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class AchievementObj{
    public AchievementScript achievementScript;
    public Image achievementImage;
}
public class AchievementManagerHolder : MonoBehaviour
{
    public List<AchievementObj> achievementScripts = new List<AchievementObj>();

    void Start()
    {
        if (AchievementManager.Instance.achievementScripts.Count == 0){
            foreach (var achievementScript in achievementScripts)
            {
                AchievementManager.Instance.achievementScripts.Add(achievementScript.achievementScript);
            }
        }
        AchievementManager.Instance.achievementManagerHolder = this;
        ImageViewUpdate();
    }

    void Update()
    {
        
    }
    public void ImageViewUpdate(){
        foreach (var achievementObj in achievementScripts)
        {
            if (achievementObj.achievementScript.AchievementCheckAvchieved()){
                achievementObj.achievementImage.GetComponent<Image>().color = Color.white;
            }
        }
    }
}
