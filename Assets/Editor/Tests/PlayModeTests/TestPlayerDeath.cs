﻿using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Cinemachine;

public class TestPlayerDeath : PlaygroundTest {

    // A UnityTest behaves like a coroutine in PlayMode
    // and allows you to yield null to skip a frame in EditMode
    [UnityTest]
    public IEnumerator TestPlayerRespawn() {
        yield return null;
    }
}
