using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash : MonoBehaviour
{
    //public GameObject flashHolder;
    //public GameObject flashPrefabs;
    //private Gun gun;

    //public float flashTime;

    private void Start()
    {
        //gun = GetComponent<Gun>();
        //Deactivate();
    }
    public void Activate()
    {
        //flashHolder.SetActive(true);
        //Instantiate(flashPrefabs, flashHolder.transform.position, 
        //                        flashHolder.transform.rotation);

        //Invoke("Deactivate", gun.nextShotTime);
    }

    void Deactivate()
    {
        //flashHolder.SetActive(false);
        //DestroyImmediate(flashPrefabs, true);

    }

    //private IEnumerator OnMuzzleFlashEffect()
    //{
    //    yield return new WaitForSeconds(flash)
    //}
}
