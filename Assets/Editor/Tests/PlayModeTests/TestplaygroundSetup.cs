using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.TestTools;
using NUnit.Framework;

public class TestplaygroundSetup : IPrebuildSetup
{
    public Tilemap Tilemap { get; private set; }

    private Tile tile;

    public void Setup()
    {

    }
}
