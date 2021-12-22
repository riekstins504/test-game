using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerNoDeathTest
{
    private Player player;
    
    [SetUp]
    public void SetUp()
    {
        player = new Player(5);
    }
    // A Test behaves as an ordinary method
    [Test]
    public void PlayerNoDeathTestSimplePasses()
    {
        // Use the Assert class to test conditions
        bool isDeath = player.TakeDamage(3);
        Assert.AreEqual(false, isDeath);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerNoDeathTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        bool isDeath = player.TakeDamage(3);
        Assert.AreEqual(false, isDeath);
        yield return null;
    }
}
