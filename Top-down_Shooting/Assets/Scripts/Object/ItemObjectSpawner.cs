using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Wave = Defines.Define.Wave;

public class ItemObjectSpawner : MonoBehaviour
{
    public bool stopSpawn_Heal;
    public bool stopSpawn_Ammo;

    //public GameObject spawnlocation;
    public bool isRemain;

    public Spawner spawner;
    public GameObject item_health;
    public GameObject item_Ammo;
    Wave[] waves;
    public Wave currentWave;

    public float randSpawnTime_heal;
    public float nextSpawnTime_heal;
    public float randSpawnTime_ammo;
    public float nextSpawnTime_ammo;


    //public Vector3 randLocation;
    public float randDistance;

    MapGenerator map;

    

    private void Awake()
    {
        GameObject s = GameObject.Find("Spawner");
        spawner = s.GetComponent<Spawner>();
        waves = spawner.waves;
        currentWave = spawner.currentWave;
        randSpawnTime_heal = Random.Range(currentWave.minSpawnTime_Heal, currentWave.maxSpawnTime_Heal);
        randSpawnTime_ammo = Random.Range(currentWave.minSpawnTime_Ammo, currentWave.maxSpawnTime_Ammo);

    }


    // Start is called before the first frame update
    void Start()
    {
        map = FindObjectOfType<MapGenerator>();
        isRemain = false;
        nextSpawnTime_heal = randSpawnTime_heal;
        nextSpawnTime_ammo = randSpawnTime_ammo;
        StopAllCoroutines();
        //stopSpawn_Ammo = true;
        //stopSpawn_Heal = true;
    }

    // Update is called once per frame
    void Update()
    {
        currentWave = spawner.currentWave;

        randSpawnTime_heal = Random.Range(currentWave.minSpawnTime_Heal, currentWave.maxSpawnTime_Heal);
        randSpawnTime_ammo = Random.Range(currentWave.minSpawnTime_Ammo, currentWave.maxSpawnTime_Ammo);


        if ( (currentWave.count_ItemHealth > 0 && Time.time > nextSpawnTime_heal))
        {
            nextSpawnTime_heal = nextSpawnTime_heal + randSpawnTime_heal;
            stopSpawn_Heal = false;
            if (!stopSpawn_Heal)
            {
                StartCoroutine("SpawnHealItem");
            }

            else
            {
                stopSpawn_Heal = true;
                StopCoroutine("SpawnHealItem");
            }
            currentWave.count_ItemHealth--;
        }
        else
            StopCoroutine("SpawnHealItem");

        if ((currentWave.count_ItemAmmo > 0 && Time.time > nextSpawnTime_ammo))
        {
            nextSpawnTime_ammo = nextSpawnTime_ammo + randSpawnTime_ammo;
            stopSpawn_Ammo = false;
            if (!stopSpawn_Ammo)
            {
                StartCoroutine("SpawnAmmoItem");
            }

            else
            {
                StopCoroutine("SpawnAmmoItem");
            }
            currentWave.count_ItemAmmo--;
        }
        else
            StopCoroutine("SpawnAmmoItem");


    }

    IEnumerator SpawnHealItem()
    {
        Transform spawnTile = map.GetRandomOpenTile();
        //Debug.Log(spawnTile.position.x+"," +spawnTile.position.z);
        //Debug.Log("Spawn Heal!");
        Instantiate(item_health, spawnTile.position + Vector3.up,
                            Quaternion.identity);

        yield return new WaitForSeconds(nextSpawnTime_heal);
    }
    IEnumerator SpawnAmmoItem()
    {
        Transform spawnTile = map.GetRandomOpenTile();
        //Debug.Log(spawnTile.position.x + "," + spawnTile.position.z);
        //Debug.Log("Spawn Ammo!");

        Instantiate(item_Ammo, spawnTile.position + Vector3.up,
                            Quaternion.identity);

        yield return new WaitForSeconds(nextSpawnTime_ammo);
    }

    //IEnumerator SpawnItem()
    //{
    //    while (!stopSpawn_Heal)
    //    {
    //        Instantiate(item_health, randLocation, spawnlocation.transform.rotation);
    //        yield return new WaitForSeconds(nextSpawnTime_heal);
    //    }
    //}

    //void NextWave()
    //{
    //    if(spawner.currentWaveNumber - 1 < waves.Length)
    //    {
    //        HealsRemainingToSpawn = currentWave.Item_HealthCount;
    //        randSpawnTime
    //            = Random.Range(currentWave.minSpawnTime_Heal,
    //                            currentWave.maxSpawnTime_Heal);

    //    }
}

    //IEnumerator SpawnAmmoItem()
    //{
    //    Transform spawnTile = map.GetRandomOpenTile();

    //    IItem spawnedItem = (Instantiate)
    //}

