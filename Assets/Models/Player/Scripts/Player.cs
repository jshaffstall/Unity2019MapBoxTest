using System;
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
	private string path;
    
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

	private void Start()
	{
		path = Application.persistentDataPath + "/player.dat";
		Load();
	}

	public void AddXp(int xp) {
		this.xp += Mathf.Max(0, xp);
		InitLevelData();
		Save();
	}

	public void AddDroid(GameObject droid) {
		if (droid)
		{
			droids.Add(droid);
			Save();
		}
	}

	private void InitLevelData() {
		lvl = (xp / levelBase) + 1;
		requiredXp = levelBase * lvl;
	}

	private void Save()
	{
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(path);
		PlayerData data = new PlayerData(this);
		bf.Serialize(file, data);
		file.Close();
	}

	private void Load()
	{
		if (File.Exists(path))
		{
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(path, FileMode.Open);
			PlayerData data = (PlayerData) bf.Deserialize(file);
			file.Close();

			xp = data.Xp;
			requiredXp = data.RequiredXp;
			levelBase = data.LevelBase;
			lvl = data.Level;
			
			/*
			 This needs work.  Right now we aren't storing the name of the
			 droid captured, so we don't know which one to create when we
			 load it again.  So for now the list of droids captured
			 won't get loaded when the game starts.  
			 */
			droids.Clear();

			foreach (DroidData droidData in data.Droids)
			{
				GameObject gameObject = null;

				foreach (GameObject droidObject in GameManager.Instance.DroidPrefabs)
				{
					if (droidData.DroidType == droidObject.name)
					{
						
					}
				}
				
				//droid.Load(droidData);
				//droids.Add(droid);
			}
		}
		else
		{
			InitLevelData();
		}
	}
}

