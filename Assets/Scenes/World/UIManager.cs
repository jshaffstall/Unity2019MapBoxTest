using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Text xpText;
    [SerializeField] private Text levelText;
    [SerializeField] private GameObject menu;

    private void Awake()
    {
        Assert.IsNotNull(xpText);
        Assert.IsNotNull(levelText);
        Assert.IsNotNull(menu);
    }

    public void updateLevel(int level)
    {
        levelText.text = level.ToString();
    }

    public void updateXP(int current, int required)
    {
        xpText.text = current.ToString() + " / " + required.ToString();
    }

    public void toggleMenu()
    {
        menu.SetActive(!menu.activeSelf);
    }
}
