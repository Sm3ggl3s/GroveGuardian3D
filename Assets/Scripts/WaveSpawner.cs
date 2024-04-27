using UnityEngine;
using System.Collections;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab; //Prefab for enemytype 1
    public Transform spawnPoint;
    public float timeBetweenWaves = 10f; //a new wave every 10 seconds

    private Transform[] waypointTargets;

    private float countdown = 3f; //however long to take at beginning of level to start the first wave
                                  //3 seconds
    private float timeBetweenWaveEnemy = 0.3f; //use this to space out enemies that spawn in one wave, !!prevents stacking!!
    private int waveNumber = 0;

    void Awake() {
        waypointTargets = new Transform[transform.childCount];
        for (int i = 0; i < waypointTargets.Length; i++) {
            waypointTargets[i] = transform.GetChild(i);
        }
    }

    void Update() {
        if(countdown <= 0f) {
            //StartCoroutine(SpawnWave());
            SpawnSingleEnemy();
            countdown = timeBetweenWaves;
        }

        countdown -= Time.deltaTime; //reduce countdown by 1 every second
    }

    //Co-routine to space out spawning of enemies each wave
    IEnumerator SpawnWave() {
        waveNumber++;
        for (int i = 0; i < waveNumber; i++) {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenWaveEnemy);
        }
    }

    //method to spawn just one enemy per wave
    void SpawnSingleEnemy() {
        // use this statement to just spawn one enemy per wave
        SpawnEnemy();
    }

    void SpawnEnemy() {
        var instance = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        instance.GetComponent<EnemyMovement>().WaypointTargets = waypointTargets;
    }
}