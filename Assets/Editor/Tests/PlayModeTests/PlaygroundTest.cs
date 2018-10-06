using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.TestTools;
using NUnit.Framework;

public abstract class PlaygroundTest
{

    [OneTimeSetUp]
    public virtual void OneTimeSetup()
    {
        PlaymodeTestUtils.InstantiatePreFab("TesttileMap");
        GameObject player = PlaymodeTestUtils.InstantiatePreFab("Player");
        GameObject camera = PlaymodeTestUtils.InstantiatePreFab("TestCamera");
        camera.GetComponent<FollowGameObject>().follow = player.transform;
    }

}
