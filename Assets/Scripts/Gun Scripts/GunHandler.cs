using System.Collections;
using TMPro;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    [SerializeField] Camera FPCamera;
    [SerializeField] AmmoType ammoType;
    [SerializeField] float range = 100f;
    [SerializeField] float timeBetweenShots = 0.3f; 
    [SerializeField] int damageDeals = 25;
    [SerializeField] int magSize = 12;
    [SerializeField] float reloadSpeed = 1f;
    [SerializeField] TextMeshProUGUI magSizeText;
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] ParticleSystem muzzleFlash;    
    [SerializeField] GameObject hitEffect;
    [SerializeField] float hitEffectDuration = 2f;
    [SerializeField] AudioClip shotClip;
    [SerializeField] AudioClip reloadClip;

    AudioSource audioSource;

    AmmoHandler ammoHandler;

    Animator animator;

    bool isReloading = false;

    int currentAmmo;

    float nextTimeToFire = 0f;

    GameManager gameManager;
    
    //using instead of Start() because of an error to a null reference for the animator component in OnEnable() 
    void Awake()
    {
        ammoHandler = GetComponentInParent<AmmoHandler>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        currentAmmo = magSize;
    }

    //Fixes a bug where couldn't shoot weapon after switching to a different weapon
    void OnEnable() 
    {
        isReloading = false;
        animator.SetBool("Reloading", false);        
    }    

    void Update()
    {
        if(gameManager.isGamePaused()) { return; }
        
        DisplayAmmo();

        if (Input.GetKeyDown(KeyCode.CapsLock))
            ammoHandler.ShowAmmoInventory();

        if (isReloading) { return; } //to avoid Coroutine happening every frame

        //if a bullet has been shot and you have remaining ammo in inventory, then reload
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo != magSize)
        {
            if (ammoHandler.GetAmmoSlot(ammoType).ammoAmount > 0)
                StartCoroutine(Reload());      

            return;
        }    
        
        if(Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / timeBetweenShots;
            Shoot();
        }        
    }

    void DisplayAmmo()
    {
        magSizeText.text = "/" + magSize.ToString();
        currentAmmoText.text = currentAmmo.ToString();
    }

    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("Reloading", true);

        audioSource.PlayOneShot(reloadClip);

        yield return new WaitForSeconds(reloadSpeed);

        animator.SetBool("Reloading", false);

        UpdateAmmoInventory();

        isReloading = false;
    }

    void UpdateAmmoInventory()
    {
        //if there will be leftover ammo in the inventory after reloading, the current ammo will be the magSize; otherwise, use remaining ammo in inventory to reload
        if (currentAmmo + ammoHandler.GetAmmoSlot(ammoType).ammoAmount > magSize)
        {
            int ammoUsed = magSize - currentAmmo;
            ammoHandler.GetAmmoSlot(ammoType).ammoAmount -= ammoUsed;
            currentAmmo = magSize;
        }
        else
        {
            currentAmmo += ammoHandler.GetAmmoSlot(ammoType).ammoAmount;
            ammoHandler.GetAmmoSlot(ammoType).ammoAmount -= ammoHandler.GetAmmoSlot(ammoType).ammoAmount;
        }
    }

    void Shoot()
    {
        if (currentAmmo > 0)
        {
            PlayMuzzleFlash();
            PlayAudio();
            ProcessRayCast();
            currentAmmo--;                    
        }
        else if(currentAmmo <=0 && ammoHandler.GetAmmoSlot(ammoType).ammoAmount > 0)
        {
            StartCoroutine(Reload());
        }
    }

    void PlayMuzzleFlash() => muzzleFlash.Play();

    void PlayAudio() => audioSource.PlayOneShot(shotClip);

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

    public bool IsReloading() { return isReloading; }
}