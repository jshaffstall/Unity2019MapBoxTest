using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureSceneManager : PocketDroidsSceneManager
{
    [SerializeField] private int maxThrowAttempts = 3;
    [SerializeField] private GameObject orb;
    [SerializeField] private Vector3 spawnPoint;
    
    private int currentThrowAttempts;

    public int MaxThrowAttempts => maxThrowAttempts;

    public int CurrentThrowAttempts => currentThrowAttempts;

    private void Start()
    {
        CalculateMaxThrows();
        currentThrowAttempts = maxThrowAttempts;
    }

    private void CalculateMaxThrows()
    {
        maxThrowAttempts += GameManager.Instance.CurrentPlayer.Lvl / 5;
    }

    public void OrbDestroyed()
    {
        currentThrowAttempts--;

        if (currentThrowAttempts <= 0)
        {
            
        }
        else
        {
            Instantiate(orb, spawnPoint, Quaternion.identity);
        }
    }

    public override void playerTapped(GameObject player)
    {
        print("playerTapped");
    }

    public override void droidTapped(GameObject droid)
    {
        print("droidTapped");
    }
}
