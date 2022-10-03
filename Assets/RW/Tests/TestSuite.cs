using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    private Game game;

    [SetUp]
    public void Setup()
    {
        GameObject gameGameObject = MonoBehaviour.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
        game = gameGameObject.GetComponent<Game>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.Destroy(game.gameObject);
    }

    [UnityTest]
    public IEnumerator LaserReload()
    {
        game.GetShip().shotCount = 10;
        game.GetShip().ShootLaser();

        yield return new WaitForSeconds(1.0f);
        Assert.AreEqual(false, game.GetShip().canShoot);
    }
    
    [UnityTest]
    public IEnumerator tripleShootTest()
    {
        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;

        bool isShootingTS = game.GetShip().isShootingTrippleShot;

        game.GetShip().ShootTrippleShot();
        
        yield return new WaitForSeconds(0.1f);

        isShootingTS = game.GetShip().isShootingTrippleShot;

        Assert.IsTrue(isShootingTS);
    }
}
