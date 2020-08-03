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
    private CaptureSceneStatus status = CaptureSceneStatus.InProgress;

    public int MaxThrowAttempts => maxThrowAttempts;

    public int CurrentThrowAttempts => currentThrowAttempts;

    public CaptureSceneStatus Status => status;
    
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
            if (status != CaptureSceneStatus.Successful)
                status = CaptureSceneStatus.Failed;
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

    public override void droidCollision(GameObject droid, Collision other)
    {
        status = CaptureSceneStatus.Successful;
    }
}
