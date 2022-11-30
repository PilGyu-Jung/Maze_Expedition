using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Health : MonoBehaviour, IItem
{
    Spawner spawner;
    int waveNum;
    public float heal = 3;

    private void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        waveNum = spawner.currentWaveNumber;

    }
    private void Update()
    {
        if (waveNum < spawner.currentWaveNumber)
        {
            Destroy(gameObject);
            waveNum++;
        }
        else 
            return;
    }
    public void Use(GameObject target)
    {
        var livingEntity = target.GetComponent<LivingEntity>();

        if (livingEntity != null)
        {
            
            livingEntity.RestoreHealth(heal);
        }
        Debug.Log("체력이 " + heal + "증가!");
        AudioManager.instance.PlaySound("HealConsume", transform.position);

        Destroy(gameObject);
    }
}
