using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] private List<GameObject> droidPrefabs = new List<GameObject>();
    
    private Player currentPlayer;

    public List<GameObject> DroidPrefabs => droidPrefabs;
    
    public Player CurrentPlayer
    {
        get
        {
            if (currentPlayer == null)
                currentPlayer = gameObject.AddComponent<Player>();
            
            return currentPlayer;
        }
    }

    

}
