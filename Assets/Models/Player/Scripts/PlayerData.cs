using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
    private int xp;
    private int requiredXp;
    private int levelBase;
    private int level;
    private List<DroidData> droids;

    public int Xp => xp;

    public int RequiredXp => requiredXp;

    public int LevelBase => levelBase;

    public int Level => level;

    public List<DroidData> Droids => droids;

    public PlayerData(Player player)
    {
        xp = player.Xp;
        requiredXp = player.RequiredXp;
        levelBase = player.LevelBase;
        level = player.Lvl;

        droids = new List<DroidData>();

        foreach (GameObject droidObject in player.Droids)
        {
            Droid droid = droidObject.GetComponent<Droid>();

            if (droid != null)
            {
                DroidData data = new DroidData(droid);
                droids.Add(data);
            }
        }
        
    }
}
