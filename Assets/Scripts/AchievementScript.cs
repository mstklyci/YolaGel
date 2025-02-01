using System;
using System.Collections;
using System.Collections.Generic;
using Madhur.InfoPopup;
using UnityEngine;

public class AchievementScript : ScriptableObject{
    public string _name;
    public string ShowText;
    public Sprite img;

    public virtual void AchievementCheckMain(bool levelDone){

    }
    public void AchievementCheck(bool levelDone){
        if (AchievementCheckAvchieved() == false){
            AchievementCheckMain(levelDone);
        }
    }
    public bool AchievementCheckAvchieved(){
        int value = PlayerPrefs.GetInt($"acv_{_name}",0);
        return value == 1;
    }
    public void Unlock(){
        PlayerPrefs.SetInt($"acv_{_name}",1);
        InfoPopupUtil.ShowInformation(ShowText,img);
    }
}