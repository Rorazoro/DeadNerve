
using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

    public GameObject enemyPrefab;
    public int maxNumberOfEnemies;
    public int currentNumberOfEnemies;
    public float spawnInterval;

    private float nextSpawnTime; 
    
    public override void OnStartServer()
    {
        nextSpawnTime = Time.time + spawnInterval;
        for (int i=0; i < maxNumberOfEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    private void Update() {
        if (nextSpawnTime < Time.time && currentNumberOfEnemies <= maxNumberOfEnemies) {
            SpawnEnemy();
        }
    }

    public void SpawnEnemy() {
        var spawnPosition = new Vector3(
            UnityEngine.Random.Range(-8.0f, 8.0f),
            2.0f,
            UnityEngine.Random.Range(-8.0f, 8.0f));

        var spawnRotation = Quaternion.Euler( 
            0.0f,
            UnityEngine.Random.Range(0,180), 
            0.0f);

        var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
        Enemy enemyObject = enemy.GetComponent<Enemy>();
        string netID = Guid.NewGuid().ToString();
        enemyObject.spawner = this;

        currentNumberOfEnemies++;
        GameManager.RegisterEnemy(netID, enemyObject);
        NetworkServer.Spawn(enemy);
    }
}