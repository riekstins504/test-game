using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class PlayerDeathTest
{
    private Player player;
    
    // A Test behaves as an ordinary method
    [SetUp]
    public void SetUp()
    {
        player = new Player(5);
    }
    
    [Test]
    public void PlayerDeathTestSimplePasses()
    {
        // Use the Assert class to test conditions
        bool isDeath = player.TakeDamage(6);
        Assert.AreEqual(true, isDeath);
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator PlayerDeathTestWithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        bool isDeath = player.TakeDamage(6);
        Assert.AreEqual(true, isDeath);
        
        yield return null;
    }
}
