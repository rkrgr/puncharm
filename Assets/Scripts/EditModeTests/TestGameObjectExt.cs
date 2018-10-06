using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using Helper.GameObjectExt;

public class TestGameObjectExt {

    [Test]
    public void TestIsInLayer() {
        GameObject testObject = new GameObject();
        testObject.layer = LayerMask.NameToLayer("Player");

        Assert.That(testObject.IsInLayer("Player"),Is.True);
        Assert.That(testObject.IsInLayer("Enemy"),Is.False);
    }
}
