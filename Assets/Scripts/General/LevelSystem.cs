using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem
{
    public event EventHandler OnExperienceChanged;
    public event EventHandler OnLevelChanged;

    private int level;
    private int experience;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
    }

    public void AddExperience(int amount)
    {
        experience += amount;

        while (experience >= GetExperienceToNextLevel(level))
        {
            level++;
            experience -= GetExperienceToNextLevel(level);
            if (OnLevelChanged != null) OnLevelChanged(this, EventArgs.Empty);
        }

        if (OnExperienceChanged != null) OnExperienceChanged(this, EventArgs.Empty);
    }

    public int GetLevelNumber()
    {
        return level;
    }

    public void SetLevelNumber(int amount)
    {
        level += amount;
    }

    public float GetExperienceNormalized()
    {
        return (float)experience / GetExperienceToNextLevel(level);
    }

    public int GetExperience()
    {
        return experience;
    }

    public void SetExperienceNumber(int amount)
    {
        experience += amount;
    }

    public int GetExperienceToNextLevel(int level)
    {
        return level + 1 * 10;
    }

    public int GetExperienceToPreviousLevel(int level)
    {
        if (level > 1)
        {
            return (level - 1) * 10;
        }
        return 0;
    }

}
