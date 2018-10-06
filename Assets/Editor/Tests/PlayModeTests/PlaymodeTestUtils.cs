using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlaymodeTestUtils {


    public static GameObject InstantiatePreFab(string name)
    {
        return UnityEngine.Object.Instantiate((GameObject)Resources.Load(name));
    }

}
