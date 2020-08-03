using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class CaptureSceneUIManager : MonoBehaviour
{
    [SerializeField] private CaptureSceneManager manager;

    [SerializeField] private GameObject successScreen;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private Text orbCountText;
    private void Awake()
    {
        Assert.IsNotNull(manager);
        Assert.IsNotNull(successScreen);
        Assert.IsNotNull(failScreen);
        Assert.IsNotNull(gameScreen);
    }

    void Update()
    {
        switch (manager.Status)
        {
            case CaptureSceneStatus.InProgress:
                handleInProgress();
                break;
            
            case CaptureSceneStatus.Successful:
                handleSuccess();
                break;
            
            case CaptureSceneStatus.Failed:
                handleFailure();
                break;
        }
    }

    private void handleInProgress()
    {
        updateVisibleScreen();
        orbCountText.text = manager.CurrentThrowAttempts.ToString();
    }
    
    private void handleSuccess()
    {
        updateVisibleScreen();
    }
    
    private void handleFailure()
    {
        updateVisibleScreen();
    }

    private void updateVisibleScreen()
    {
        successScreen.SetActive(manager.Status == CaptureSceneStatus.Successful);
        failScreen.SetActive(manager.Status == CaptureSceneStatus.Failed);
        gameScreen.SetActive(manager.Status == CaptureSceneStatus.InProgress);
    }
}
