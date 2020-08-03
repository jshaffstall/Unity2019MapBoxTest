using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldSceneManager : PocketDroidsSceneManager
{
    public override void droidTapped(GameObject droid)
    {
        List<GameObject> list = new List<GameObject> {droid};
        
        SceneTransitionManager.Instance.GoToScene(PocketDroidsConstants.SCENE_CAPTURE, list);        
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
