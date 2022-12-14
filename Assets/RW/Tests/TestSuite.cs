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
    
    [UnityTest]
    public IEnumerator SniperDestroysAsteroid()
    {
        // 1

        Ship player = game.GetShip();
        player.sniperShot = true;

        Spawner spawnAster = game.GetSpawner();
        spawnAster.asteroidHP = 100;


        GameObject asteroid = game.GetSpawner().SpawnAsteroid();
        asteroid.transform.position = Vector3.zero;
        GameObject laser = game.GetShip().SpawnLaser();
        laser.transform.position = Vector3.zero;
        yield return new WaitForSeconds(0.1f);
        // 2
        UnityEngine.Assertions.Assert.IsNull(asteroid);
    }
}
