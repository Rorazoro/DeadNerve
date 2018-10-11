
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;

public class EnemySpawner : NetworkBehaviour {

    public GameObject enemyPrefab;
    public int numberOfEnemies;
   
    private Timer spawnTimer;
    
    public override void OnStartServer()
    {
        spawnTimer = new Timer(3000);
        spawnTimer.Elapsed += OnTimedEvent;
        spawnTimer.AutoReset = true;
        spawnTimer.Enabled = true;
    }

    private static void OnTimedEvent(Object source, ElapsedEventArgs e)
    {

    }

    public void SpawnEnemies(bool spawnAll) {
        for (int i=0; i < numberOfEnemies; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(-8.0f, 8.0f),
                2.0f,
                Random.Range(-8.0f, 8.0f));

            var spawnRotation = Quaternion.Euler( 
                0.0f, 
                Random.Range(0,180), 
                0.0f);

            var enemy = (GameObject)Instantiate(enemyPrefab, spawnPosition, spawnRotation);
            string ID = i.ToString();
		    Enemy enemyObject = enemy.GetComponent<Enemy>();

		    GameManager.RegisterEnemy(ID, enemyObject);
            NetworkServer.Spawn(enemy);
        }
    }
}