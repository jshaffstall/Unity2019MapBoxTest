using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureSceneManager : PocketDroidsSceneManager
{
    public override void playerTapped(GameObject player)
    {
        print("playerTapped");
    }

    public override void droidTapped(GameObject droid)
    {
        print("droidTapped");
    }
}
