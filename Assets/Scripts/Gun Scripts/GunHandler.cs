using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class GunHandler : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float range = 100f;
    [SerializeField] float timeBetweenShots = 0.3f;
    [SerializeField] int damageDeals = 25;
    [SerializeField] int magSize = 12;
    [SerializeField] TextMeshProUGUI magSizeText;
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitEffect;
    [SerializeField] float hitEffectDuration = 2f;        

    AmmoHandler ammoHandler;

    bool canShoot = true;

    int currentAmmo;

    void OnEnable() //fixes a bug where couldn't shoot weapon after switching back to it because Coroutine never made canShoot enabled
    {
        canShoot = true;

        //TODO - figure out how to make shotgun and sniper resume it's timeBetweenShots when switched to if it did fire before switching; possible solution: https://community.gamedev.tv/t/public-enum-private-class-lecture/107615/9
    }

    void Start()
    {
        ammoHandler = GetComponentInParent<AmmoHandler>();  //TODO - use for reloading 
        currentAmmo = magSize;        
    }

    void Update()
    {
        DisplayAmmo();
        
        if(Input.GetMouseButtonDown(0) && canShoot)
        {
            StartCoroutine(Shoot());
        }
    }

    void DisplayAmmo()
    {
        magSizeText.text = "/" + magSize.ToString();
        currentAmmoText.text = currentAmmo.ToString();
    }

    IEnumerator Shoot()
    {
        canShoot = false;   //to limit the amount of Coroutines when firing

        if (currentAmmo > 0)
        {
            PlayMuzzleFlash();
            ProcessRayCast();
            currentAmmo--;
        }

        yield return new WaitForSeconds(timeBetweenShots);

        canShoot = true;
    }

    void PlayMuzzleFlash()
    {
        muzzleFlash.Play();
    }

    void ProcessRayCast()
    {
        RaycastHit hit;

        if (Physics.Raycast(FPCamera.transform.position, FPCamera.transform.forward, out hit, range))
        {
            CreateHitImpact(hit);

            EnemyHealthHandler target = hit.transform.GetComponent<EnemyHealthHandler>();

            if (target == null) { return; }

            target.DecreaseEnemyHealth(damageDeals);
        }
        else
        {
            return; //protects from null
        }
    }

    void CreateHitImpact(RaycastHit hit)
    {
        GameObject impact = Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, hitEffectDuration);
    }
}
