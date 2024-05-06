using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab; //Prefab for enemytype 1
    public Transform spawnPoint;
    public float timeBetweenWaves = 15f; //a new wave every 10 seconds

    private Transform[] waypointTargets;

    private float countdown = 3f; //however long to take at beginning of level to start the first wave
                                  //3 seconds
    private float timeBetweenWaveEnemy = 1.3f; //use this to space out enemies that spawn in one wave, !!prevents stacking!!
    private int waveNumber = 0;

    public int maxWaveCount = 5; // 5 shall be default

    void Awake() {
        // Give a newly-spawned enemy its waypoints to follow
        waypointTargets = new Transform[transform.childCount];
        for(int i = 0; i < waypointTargets.Length; i++) {
            waypointTargets[i] = transform.GetChild(i);
        }

        // Set the maximum amount of waves to spawn depending on what level is loaded
        // If level 1, set maxWaveCount to 6
        if(SceneManager.GetActiveScene().buildIndex == GameManager.LEVEL_ONE_SCENE_INDEX) {
            maxWaveCount = 6;
        }
        // If level 2, set maxWaveCount to 10
        else if(SceneManager.GetActiveScene().buildIndex == GameManager.LEVEL_TWO_SCENE_INDEX) {
            maxWaveCount = 10;
        }
    }

    void Update() {
        if(waveNumber <= maxWaveCount) {
            if(countdown <= 0f && !(GameManager.GameIsOver)) {
                StartCoroutine(SpawnWave());
                //SpawnSingleEnemy();
                countdown = timeBetweenWaves;
            }

            countdown -= Time.deltaTime; //reduce countdown by 1 every second
        }
        else {
            GameManager.WavesDone = true;
        }
    }

    //Co-routine to space out spawning of enemies each wave
    IEnumerator SpawnWave() {
        waveNumber++;
        for (int i = 0; i < waveNumber; i ++) {
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

        // Refer to GameManager script to see scene indices for levels
        // If Level 1, set speed to 2.5f
        if(SceneManager.GetActiveScene().buildIndex == GameManager.LEVEL_ONE_SCENE_INDEX) {
            instance.GetComponent<EnemyMovement>().speed = 2.5f;
        }
        // If Level 2, set speed to 5f
        else if(SceneManager.GetActiveScene().buildIndex == GameManager.LEVEL_TWO_SCENE_INDEX) {
            instance.GetComponent<EnemyMovement>().speed = 5f;
        }
    }
}