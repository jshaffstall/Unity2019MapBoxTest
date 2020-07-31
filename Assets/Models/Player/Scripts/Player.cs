﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField] private int xp = 0;
	[SerializeField] private int requiredXp = 100;
	[SerializeField] private int levelBase = 100;
	[SerializeField] private List<GameObject> droids = new List<GameObject>();
	
	private int lvl = 1;
    
	public int Xp {
		get { return xp; }
	}

	public int RequiredXp {
		get { return requiredXp; }
	}

	public int LevelBase {
		get { return levelBase; }
	}

	public List<GameObject> Droids {
		get { return droids; }
	}

	public int Lvl {
		get { return lvl; }
	}
	
	public void AddXp(int xp) {
		this.xp += Mathf.Max(0, xp);
		InitLevelData();
	}

	public void AddDroid(GameObject droid) {
		if (droid)
    		droids.Add(droid);
	}

	private void InitLevelData() {
		lvl = (xp / levelBase) + 1;
		requiredXp = levelBase * lvl;
	}

}

