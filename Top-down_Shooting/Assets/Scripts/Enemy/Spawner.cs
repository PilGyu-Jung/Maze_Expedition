using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Wave = Defines.Define.Wave;

public class Spawner : MonoBehaviour
{
    public bool devMode;

    public Wave[] waves;
    public Enemy enemyA;
    public Enemy enemyB;

    public LivingEntity playerEntity;
    Transform playerT;

    public Wave currentWave;
    public int currentWaveNumber;

    int enemiesRemainingToSpawn;
    int enemiesRemainingAlive;
    int randomChance;
    int weight;

    float nextSpawnTime;

    MapGenerator map;

    float timeBetweenCampingChecks = 2;
    float campThresholdDistance = 1.5f;
    float nextCampCheckTime;
    Vector3 campPositionOld;
    bool isCamping;

    public bool isDisabled;

    public event System.Action<int> OnNewWave;

    void Start()
    {
        playerEntity = FindObjectOfType<Player>();
        playerT = playerEntity.transform;

        nextCampCheckTime = timeBetweenCampingChecks + Time.time;
        campPositionOld = playerT.position;
        playerEntity.OnDeath += OnPlayerDeath;

        map = FindObjectOfType<MapGenerator>();
        NextWave();
    }

    void Update()
    {
        weight = currentWave.spawnRarity_A + currentWave.spawnRarity_B + 1;
        randomChance = Random.Range(0, weight);

        if (!isDisabled)
        {
            if (Time.time > nextCampCheckTime)
            {
                nextCampCheckTime = Time.time + timeBetweenCampingChecks;

                isCamping = (Vector3.Distance(playerT.position, campPositionOld) < campThresholdDistance);
                campPositionOld = playerT.position;
            }

            if ((enemiesRemainingToSpawn > 0 || currentWave.infinite) && Time.time > nextSpawnTime)
            {
                enemiesRemainingToSpawn--;
                nextSpawnTime = Time.time + currentWave.timeBetweenSpawns;

                StartCoroutine("SpawnEnemy_randomRarity");
            }
        }

        if (devMode)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                StopCoroutine("SpawnEnemy_randomRarity");
                foreach (Enemy enemy in FindObjectsOfType<Enemy>())
                {
                    GameObject.Destroy(enemy.gameObject);
                }
                NextWave();
            }
        }
    }

    IEnumerator SpawnEnemy_randomRarity()
    {
        float spawnDelay = 1;
        float tileFlashSpeed = 4;
        Enemy spawnedEnemyA;
        Enemy spawnedEnemyB;

        Transform spawnTile = map.GetRandomOpenTile();
        if (isCamping)
        {
            spawnTile = map.GetTileFromPosition(playerT.position);
        }
        Material tileMat = spawnTile.GetComponent<Renderer>().material;
        Color initialColour = Color.white;
        Color flashColour = Color.red;
        float spawnTimer = 0;

        while (spawnTimer < spawnDelay)
        {

            tileMat.color = Color.Lerp(initialColour, flashColour, Mathf.PingPong(spawnTimer * tileFlashSpeed, 1));

            spawnTimer += Time.deltaTime;
            yield return null;
        }
        if (currentWave.spawnRarity_A > randomChance)
        {
            spawnedEnemyA = Instantiate(enemyA, spawnTile.position + Vector3.up, Quaternion.identity) as Enemy;
            spawnedEnemyA.OnDeath += OnEnemyDeath;
            spawnedEnemyA.SetCharacteristics(currentWave.moveSpeedA, currentWave.hitsToKillPlayer, currentWave.enemyHealthA, currentWave.skinColour);
        }
        else if (randomChance >= currentWave.spawnRarity_A)
        {
            spawnedEnemyB = Instantiate(enemyB, spawnTile.position + Vector3.up, Quaternion.identity) as Enemy;
            spawnedEnemyB.OnDeath += OnEnemyDeath;
            spawnedEnemyB.SetCharacteristics(currentWave.moveSpeedA, currentWave.hitsToKillPlayer, currentWave.enemyHealthA, currentWave.skinColour);

        }
        //spawnedEnemy.OnDeath += OnEnemyDeath;
        //spawnedEnemy.SetCharacteristics(currentWave.moveSpeed, currentWave.hitsToKillPlayer, currentWave.enemyHealth, currentWave.skinColour);
    }

    void OnPlayerDeath()
    {
        isDisabled = true;
    }

    void OnEnemyDeath()
    {
        enemiesRemainingAlive--;

        if (enemiesRemainingAlive == 0)
        {
            NextWave();
        }
    }

    void ResetPlayerPosition()
    {
        playerT.position
            = map.GetTileFromPosition(Vector3.zero).position + Vector3.up * 3;
    }

    void NextWave()
    {
        if (currentWaveNumber > 0)
        {
            AudioManager.instance.PlaySound2D("Level Complete");
        }
        currentWaveNumber++;

        if (currentWaveNumber - 1 < waves.Length)
        {
            currentWave = waves[currentWaveNumber - 1];

            enemiesRemainingToSpawn = currentWave.enemyCount;
            enemiesRemainingAlive = enemiesRemainingToSpawn;

            if (OnNewWave != null)
            {
                OnNewWave(currentWaveNumber);
            }
            ResetPlayerPosition();
        }
    }

    //[System.Serializable]
    //public class Wave
    //{
    //	public bool infinite;
    //	public int enemyCount;
    //	public float timeBetweenSpawns;

    //	public float moveSpeed;
    //	public int hitsToKillPlayer;
    //	public float enemyHealth;
    //	public Color skinColour;
    //}

}
