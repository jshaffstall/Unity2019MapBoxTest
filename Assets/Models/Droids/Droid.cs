﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class Droid : MonoBehaviour
{
    [SerializeField] private float spawnRate = 0.1f;
    [SerializeField] private float catchRate = 0.1f;
    [SerializeField] private int attack = 0;
    [SerializeField] private int defense = 0;
    [SerializeField] private int hp = 10;
    [SerializeField] private AudioClip crySound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(audioSource);
        Assert.IsNotNull(crySound);
    }

    public float SpawnRate ()
    {
        return spawnRate;
    }

    public float CatchRate ()
    {
        return catchRate;
    }

    public int Attack ()
    {
        return attack;
    }

    public int Defense ()
    {
        return defense;
    }

    public int Hp ()
    {
        return hp;
    }

    public AudioClip CrySound()
    {
        return crySound;
    }
    
    private void OnMouseDown()
    {
        PocketDroidsSceneManager[] managers = FindObjectsOfType<PocketDroidsSceneManager>();

        audioSource.PlayOneShot(crySound);

        foreach (PocketDroidsSceneManager manager in managers)
            if (manager.gameObject.activeSelf)
                manager.droidTapped(this.gameObject);

    }

    public void DroidHit(GameObject other)
    {
        PocketDroidsSceneManager[] managers = FindObjectsOfType<PocketDroidsSceneManager>();

        foreach (PocketDroidsSceneManager manager in managers)
            if (manager.gameObject.activeSelf)
                manager.droidCollision(this.gameObject, other);
    }

    public void Load(DroidData data)
    {
        spawnRate = data.SpawnRate;
        catchRate = data.CatchRate;
        attack = data.Attack;
        defense = data.Defense;
        hp = data.Hp;
        crySound = (AudioClip)Resources.Load(data.CrySound, typeof(AudioClip));
    }
}
