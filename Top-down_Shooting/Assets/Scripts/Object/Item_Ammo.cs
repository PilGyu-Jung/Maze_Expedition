using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Ammo : MonoBehaviour, IItem
{
    public float ammo;
    int waveNum;

    GunController guncontroller;
    Gun equippedGun;
    Spawner spawner;

    private void Start()
    {
        guncontroller = FindObjectOfType<GunController>();
        spawner = FindObjectOfType<Spawner>();
        waveNum = spawner.currentWaveNumber;
        //ammo = Mathf.Round(guncontroller.maxAmmo * 0.33f);
        ammo = 60f;
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
        equippedGun = FindObjectOfType<Gun>();
        AudioManager.instance.PlaySound("AmmoConsume", transform.position);

        if (guncontroller != null )
        {
            if (guncontroller.curAmmo <= 0)
            {
                guncontroller.curAmmo = ammo - equippedGun.projectilesRemainingInMag;
                equippedGun.projectilesRemainingInMag = equippedGun.projectilesPerMag;
                guncontroller.hasAmmo = true;
            }
            else
                guncontroller.curAmmo += ammo;
        }
        Debug.Log("Åº¾àÀÌ " + ammo + "Áõ°¡!");

        Destroy(gameObject);
    }

}
