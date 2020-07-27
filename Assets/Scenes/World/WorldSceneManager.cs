using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : PocketDroidsSceneManager
{
    public override void droidTapped(GameObject droid)
    {
        SceneManager.LoadScene(PocketDroidsConstants.SCENE_CAPTURE, LoadSceneMode.Additive);
    }

    public override void playerTapped(GameObject player)
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
