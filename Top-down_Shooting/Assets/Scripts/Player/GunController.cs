using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    //public static GunController instance;
    public InteractObject interactObject;
    Gun equippedGun;
    public Gun[] allGuns;
    public Transform weaponHold;
    //public Gun startingGun;

    public float maxAmmo;
    public float curAmmo;
    public bool hasAmmo;

    private void Start()
    {
        interactObject = GetComponent<InteractObject>();
        curAmmo = maxAmmo;
        hasAmmo = true;

        //if (startingGun != null)
        //{
        //    EquipGun(startingGun);
        //}
    }
    private void Update()
    {
        if (curAmmo > 0 && curAmmo <= maxAmmo)
        {
            hasAmmo = true;
        }
        else if (curAmmo > 0 && curAmmo > maxAmmo)
        {
            hasAmmo = true;
            curAmmo = maxAmmo;
        }
        else
        {
            hasAmmo = false;
            curAmmo = 0;
        }
    }

    //private void LateUpdate()
    //{
    //    if (curAmmo > 0 && curAmmo <= maxAmmo)
    //    {
    //        hasAmmo = true;
    //    }
    //    else if (curAmmo > 0 && curAmmo > maxAmmo)
    //    {
    //        hasAmmo = true;
    //        curAmmo = maxAmmo;
    //    }
    //    else
    //    {
    //        hasAmmo = false;
    //    }

    //}

    //public bool HasAmmo(float Ammo)
    //{
    //    if (Ammo > 0 && Ammo <= maxAmmo)
    //    {
    //        return true;
    //    }
    //    else if (Ammo > 0 && Ammo > maxAmmo)
    //    {
    //        return true;
    //        curAmmo = maxAmmo;
    //    }
    //    else
    //    {
    //        return false;
    //    }

    //}
    public void EquipGun(Gun guntoEquip)
    {
        if(equippedGun != null)
        {
            Destroy(equippedGun.gameObject);
        }
        equippedGun 
            = Instantiate(guntoEquip,weaponHold.position,weaponHold.rotation) as Gun;
        equippedGun.transform.parent = weaponHold;
    }

    public void EquipGun(int weaponIndex)
    {
        EquipGun(allGuns[weaponIndex]);
    }

    public void OnTriggerHold()
    {
        if (equippedGun != null && ShopManager.instance.enteredShop == false 
            && interactObject.useInteract == false)
        {
            equippedGun.OnTriggerHold();
        }
    }


    public void OnTriggerRelease()
    {
        if (equippedGun != null)
        {
            equippedGun.OnTriggerRelease();
        }

    }

    public float GunHeight
    {
        get
        {
            return weaponHold.position.y;
        }
    }

    public void Aim(Vector3 aimPoint)
    {
        if (equippedGun != null)
        {
            equippedGun.Aim(aimPoint);
        }
    }

    public void Reload()
    {
        
        if (equippedGun != null)
        {
            if(curAmmo >= equippedGun.projectilesPerMag)
            {
                equippedGun.Reload();
            }
            //curAmmo -= (equippedGun.projectilesPerMag - equippedGun.projectilesRemainingInMag);
        }

    }
}

