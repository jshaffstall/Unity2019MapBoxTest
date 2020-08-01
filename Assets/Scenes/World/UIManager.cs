﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text xpText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject menu;
    [SerializeField] private AudioClip menuButtonSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Assert.IsNotNull(xpText);
        Assert.IsNotNull(levelText);
        Assert.IsNotNull(menu);
        Assert.IsNotNull(menuButtonSound);
        Assert.IsNotNull(audioSource);
    }

    public void Update()
    {
        updateLevel();
        updateXP();
    }

    public void updateLevel()
    {
        levelText.text = GameManager.Instance().CurrentPlayer.Lvl.ToString();
    }

    public void updateXP()
    {
        xpText.text = GameManager.Instance().CurrentPlayer.Xp.ToString() + " / " + GameManager.Instance().CurrentPlayer.RequiredXp.ToString();
    }

    public void MenuButtonClick ()
    {
        audioSource.PlayOneShot(menuButtonSound);
        //toggleMenu();
    }

    private void toggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
}
