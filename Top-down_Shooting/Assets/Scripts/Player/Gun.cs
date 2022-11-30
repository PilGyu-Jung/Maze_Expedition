using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GunController gunController;
    public PlayerAnimator playerAnimator;
    public enum FireMode { Auto, Burst, Single};
    public FireMode fireMode;

    public Transform[] projectileSpawn;
    public Projectile projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
    public int burstCount;
    public float projectilesPerMag;
    public float reloadTime = .3f;

    [Header("Recoil")]
    public Vector2 kickMinMax = new Vector2(.05f, .2f);
    public Vector2 recoilAngleMinMax = new Vector2(3, 5);
    public float recoilMoveSettleTime = .1f;
    public float recoilRotationSettleTime = .1f;

    [Header("Effect")]
    public Transform shell;
    public Transform shellEjection;
    public AudioClip shootAudio;
    public AudioClip reloadAudio;

    bool triggerReleaseSinceLastShot;
    int shotsRemainingInBurst;
    public float projectilesRemainingInMag;
    public bool isReloading;

    Vector3 recoilSmoothDampVelocity;
    float recoilRotSmoothDampVelocity;
    float recoilAngle;

    [Header("Muzzle Flash")]
    [SerializeField]
    private GameObject muzzleFlashEffect;
    private void OnEnable()
    {
        muzzleFlashEffect.SetActive(false);
    }
    private void Start()
    {
        gunController = FindObjectOfType<GunController>();
        playerAnimator = FindObjectOfType<PlayerAnimator>();
        shotsRemainingInBurst = burstCount;
        projectilesRemainingInMag = projectilesPerMag;
    }

    private void LateUpdate()
    {

        // animate recoil
        transform.localPosition 
            = Vector3.SmoothDamp(transform.localPosition, Vector3.zero, 
                        ref recoilSmoothDampVelocity, recoilMoveSettleTime);
        recoilAngle 
            = Mathf.SmoothDamp(recoilAngle, 0, ref recoilRotSmoothDampVelocity, 
                                                recoilRotationSettleTime);
        transform.localEulerAngles 
            = transform.localEulerAngles + Vector3.left * recoilAngle;

        if(!isReloading && projectilesRemainingInMag == 0)
        {
            Reload();
        }
        if(projectilesRemainingInMag < 0)
        {
            projectilesRemainingInMag = 0;
        }
    }
    private float nextShotTime;

    void Shoot()
    {
        if(!isReloading && Time.time > nextShotTime && projectilesRemainingInMag > 0 )
        {
            if(fireMode == FireMode.Burst)
            {
                playerAnimator.OnShootAnim((int)FireMode.Burst);
                if(shotsRemainingInBurst == 0)
                {
                    return;
                }
                shotsRemainingInBurst--;
            }
            else if(fireMode== FireMode.Single)
            {
                playerAnimator.OnShootAnim((int)FireMode.Single);
                if (!triggerReleaseSinceLastShot)
                {
                    return;
                }
            }
            else
            {
                playerAnimator.OnShootAnim((int)FireMode.Auto);
            }
            for(int i = 0; i < projectileSpawn.Length; i++)
            {
                if(projectilesRemainingInMag == 0)
                {
                    break;
                }
                projectilesRemainingInMag--;
                nextShotTime = Time.time + msBetweenShots / 1000;
                Projectile newProjectile 
                    = Instantiate(projectile, projectileSpawn[i].position,
                                projectileSpawn[i].rotation) as Projectile;
                newProjectile.SetSpeed(muzzleVelocity);
            }
            Instantiate(shell, shellEjection.position, shellEjection.rotation);
            StartCoroutine("OnMuzzleFlashEffect");
            transform.localPosition -= Vector3.forward * Random.Range(kickMinMax.x,kickMinMax.y);
            recoilAngle += Random.Range(recoilAngleMinMax.x, recoilAngleMinMax.y);
            recoilAngle = Mathf.Clamp(recoilAngle, 0, 30);

            AudioManager.instance.PlaySound(shootAudio, transform.position);
        }

    }

    private IEnumerator OnMuzzleFlashEffect()
    {
        muzzleFlashEffect.SetActive(true);

        yield return new WaitForSeconds(0.03f);

        muzzleFlashEffect.SetActive(false);

    }

    public void OnTriggerHold()
    {
        Shoot();
        triggerReleaseSinceLastShot = false;
    }

    public void OnTriggerRelease()
    {
        triggerReleaseSinceLastShot = true;
        shotsRemainingInBurst = burstCount;

    }

    public void Aim(Vector3 aimPoint)
    {
        if (!isReloading)
        {
            transform.LookAt(aimPoint);
        }
    }

    public void Reload()
    {
        
        if(gunController.hasAmmo && !isReloading && (projectilesRemainingInMag != projectilesPerMag))
        {
            if(projectilesRemainingInMag > gunController.curAmmo)
            {
                gunController.curAmmo = projectilesRemainingInMag;
            }
            else
                gunController.curAmmo -= (projectilesPerMag - projectilesRemainingInMag);

            StartCoroutine("AnimateReload");
            playerAnimator.OnReloadAnim();
            AudioManager.instance.PlaySound(reloadAudio,transform.position);
        }
    }

    IEnumerator AnimateReload()
    {
        isReloading = true;
        yield return new WaitForSeconds(.2f);

        float reloadSpeed = 1f / reloadTime;
        float percent = 0;
        Vector3 initialRot = transform.localEulerAngles;
        float maxReloadAngle = 30;

        while(percent < 1)
        {
            percent += Time.deltaTime * reloadSpeed;
            float interpolation = (-Mathf.Pow(percent, 2) + percent) * 4;
            float reloadAngle = Mathf.Lerp(0, maxReloadAngle, interpolation);
            transform.localEulerAngles = initialRot + Vector3.left * reloadAngle;

            yield return null;
        }
        isReloading = false;
        if(gunController.curAmmo < projectilesPerMag)
        {
            projectilesRemainingInMag = gunController.curAmmo;
        }
        else
            projectilesRemainingInMag = projectilesPerMag;
    }
}
