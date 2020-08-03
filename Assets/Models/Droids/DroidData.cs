using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DroidData 
{
    private float catchRate = 0.1f;
    private int attack = 0;
    private int defense = 0;
    private int hp = 10;
    private string crySound;
    
    private float spawnRate = 0.1f;

    public float SpawnRate => spawnRate;

    public float CatchRate => catchRate;

    public int Attack => attack;

    public int Defense => defense;

    public int Hp => hp;

    public string CrySound => crySound;

    public DroidData(Droid droid)
    {
        spawnRate = droid.SpawnRate();
        attack = droid.Attack();
        defense = droid.Defense();
        hp = droid.Hp();
        crySound = droid.CrySound().name;
    }
}
