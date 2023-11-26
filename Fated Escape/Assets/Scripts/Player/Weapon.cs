using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    // UI management
    public GameObject crosshair;
    public AmmoManager ammoManager;

    // Animation and movement
    public GameObject cameraGameObject;
    public GameObject bulletEffect, hitEffect;
    private Animator animations;
    private InputManager inputs;

    // Gun related variables
    public float fireRate = 4f, reloadTime = 0f, reloadAnimationTime = 2.5f;
    public int magazine, ammo, maxAmmo, magazineCap = 30, magCount = 3;

    public List<AudioClip> gunfireSounds, reloadSounds;
    public AudioSource gunSource;

    // Reloading
    private float readyToFire = 0;
    public bool isReloading = false;

    // Gun stats
    private int kills = 0;
    public float damage = 10f, damageUpgrade = 5f;
    public int[] killThreshold =
    {
        5,
        10,
        20,
        30
    };

    private void Start()
    {
        // Get components
        ammoManager = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<AmmoManager>();
        animations = gameObject.GetComponent<Animator>();
        inputs = gameObject.GetComponent<InputManager>();

        // Start animations
        animations.SetInteger("Movement", 0);

        // Determine capacity of current gun
        magazine = magazineCap;
        maxAmmo = magazineCap * magCount;
        ammo = maxAmmo;

        // Update UI
        ammoManager.setammo(magazine + "/" + ammo);
        ammoManager.setWeaponToDisplay(0);
    }

    private void Update()
    {
        // Detect if aiming at enemy
        RaycastHit hit;
        if (Physics.Raycast(cameraGameObject.transform.position, cameraGameObject.transform.forward, out hit) && hit.transform.tag == "Enemy")
            crosshair.GetComponent<Image>().color = new Color(156f / 255f, 14f / 255f, 33f / 255f);
        else
            crosshair.GetComponent<Image>().color = new Color(1f, 1f, 1f);

        if (Time.time >= readyToFire)
        {
            animations.SetInteger("Fire", -1);
            animations.SetInteger("Movement", (inputs.vertical == 0 && inputs.horizontal == 0) ? 0 : 1);
        }

        if (Input.GetMouseButton(0) && Time.time >= readyToFire && !isReloading && magazine > 0)
        {
            fire();
            PlayFire();
            readyToFire = Time.time + 1f / fireRate;
            animations.SetInteger("Fire", 2);
            animations.SetInteger("Movement", -1);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammo > 0 && magazine < magazineCap)
        {
            reloadTime = reloadAnimationTime;
            animations.SetInteger("Reload", 1);
            isReloading = true;
        }

        if (isReloading && reloadTime <= 1)
        {
            reloadTime = 0;
            animations.SetInteger("Reload", -1);
            isReloading = false;

            int delta = Mathf.Min(magazineCap - magazine, ammo);
            magazine += delta;
            updateAmmo(-delta);
        }
        else
        {
            reloadTime -= Time.deltaTime;
        }
    }

    private void PlaySound(List<AudioClip> availableSounds)
    {
        gunSource.pitch = Random.Range(0.95f, 1.05f);
        gunSource.PlayOneShot(availableSounds[Random.Range(0, availableSounds.Count)], Random.Range(0.65f, 0.85f));
    }

    public void PlayReload()
    {
        PlaySound(reloadSounds);
    }

    public void PlayFire()
    {
        PlaySound(gunfireSounds);
    }

    private void LateUpdate()
    {
        ammoManager.setammo(magazine + "\n  | " + ammo);
    }

    public void updateAmmo(int ammoDelta)
    {
        ammo = Mathf.Max(0, Mathf.Min(ammo + ammoDelta, maxAmmo));
    }

    public void addKill()
    {
        kills++;
        foreach (int threshold in killThreshold)
        {
            if (kills == threshold)
            {
                Debug.Log("Damage upgraded by " + damageUpgrade);
                damage += damageUpgrade;
            }
        }
    }

    private void fire()
    {
        RaycastHit hit;
        magazine--;

        if (Physics.Raycast(cameraGameObject.transform.position, cameraGameObject.transform.forward, out hit))
        {
            EnemyManager enemyManager = hit.transform.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                enemyManager.takeDamage(damage);
                Instantiate(hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
            else
            {
                Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
