using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
public class AchievementData
{
    public string ClassName;
    public string Explain;
    public string Obtain;
}

[Serializable]
public class AchievementContainer
{
    public AchievementData[] Achievements;
}

public class AchievementManager
{
    public List<AchievementClass> Achievements;

    public AchievementManager()
    {
        Achievements = new List<AchievementClass>();
        TextAsset jsonData = Resources.Load<TextAsset>("GameData/AchievementDataKorean");
        if (jsonData == null)
            Debug.Log("Achi json 파싱 실패!!");
        AchievementContainer container = JsonUtility.FromJson<AchievementContainer>(jsonData.text);
        foreach (var jsonAchievement in container.Achievements)
        {
            Type achievementType = Type.GetType(jsonAchievement.ClassName);

            if (achievementType != null)
            {
                AchievementClass achievement = (AchievementClass)Activator.CreateInstance(achievementType);
                achievement.Explain = jsonAchievement.Explain;
                achievement.Obtain = jsonAchievement.Obtain;

                Achievements.Add(achievement);
            }
        }
    }

    public void UpdateAchievements()
    {
        for(int i = 0; i < Achievements.Count; i++) 
        {
            if (!UserInfo.instance.UserDataSet.BAchievements[i] && Achievements[i].IsComplete())
            {
                UserInfo.instance.UserDataSet.BAchievements[i] = true;
                Achievements[i].EarnRewards();
            }
        }
    }
}