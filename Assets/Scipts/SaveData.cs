using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public class SaveData
{
    public int highScore = 0;
    public List<achievement> achievements = new List<achievement>();
    // Start is called before the first frame update
    public void addAchievement(achievement unlock)
    {
        this.achievements.Add(unlock);
    }
    public List<achievement> getAchievements()
    {
        return achievements;
    }
    public void setHighScore(int newHighScore)
    {
        this.highScore = newHighScore;
    }
    public void clearAchievements()
    {
        this.achievements = new List<achievement>();
    }
    public int getHighScore()
    {
        return this.highScore;
    }


}
