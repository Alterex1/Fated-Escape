using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour
{
    // UI management
    public GameObject crosshair;
    public AmmoManager ui;
    public string weaponType;

    // Animation and movement
    public GameObject cameraGameObject;
    public GameObject bulletEffect, hitEffect;
    private Animator animations;
    private InputManager inputs;

    // Gun related variables
    public float fireRate = 4f, reloadTime = 0f, reloadAnimationTime = 2.5f;
    public int magazine, ammo, maxAmmo, magazineCap = 30;
    public int mags = 3;
    public int magazineTemp;

    public Vector3 normalPosition; // Normal position of the weapon
    public Vector3 aimPosition; // Position of the weapon when aiming
    public float positionSpeed = 2f; // Speed of position change

    public List<AudioClip> gunfireSounds, reloadSounds, levelupSounds;
    public AudioSource gunSource;

    // Reloading
    private float readyToFire = 0;
    public bool isReloading = false;

    // Gun stats
    public float damage = 10f, damageUpgrade = 5f;

    private void Start()
    {
        // Get components
        ui = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<AmmoManager>();
        animations = gameObject.GetComponent<Animator>();
        inputs = gameObject.GetComponent<InputManager>();

        // Start animations
        animations.SetInteger("Movement", 0);

        ammo = magazine * mags;
        maxAmmo = ammo;
        magazineTemp = magazine;

        // Update UI
        ui.setammo(magazine + "/" + ammo);
        ui.setWeaponToDisplay(0);
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

        if (Input.GetMouseButton(0) && Time.time >= readyToFire && !isReloading && magazine > 0 && (weaponType == "AR" || weaponType == "SMG"))
        {
            fire();
            PlayFire();
            readyToFire = Time.time + 1f / fireRate;
            animations.SetInteger("Fire", 2);
            animations.SetInteger("Movement", -1);
        }

         if (Input.GetMouseButtonDown(0) && Time.time >= readyToFire && !isReloading && magazine > 0 && weaponType == "Shotgun")
        {
            readyToFire = Time.time + 1f / fireRate;
            fireShotgun();
            animations.SetInteger("Fire", 2);
            animations.SetInteger("Movement", -1);
        }

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammo > 0 && magazine < magazineTemp)
        {
            reloadTime = reloadAnimationTime;
            animations.SetInteger("Reload", 1);
            isReloading = true;
        }

        if (isReloading && reloadTime <= 1 && weaponType == "AR")
        {
            reloadTime = 0;
            animations.SetInteger("Reload", -1);
            isReloading = false;
            ammo = ammo - 30 + magazine;
            magazine = magazineTemp;
            if (ammo < 0)
            {
                magazine += ammo;
                ammo = 0;
                ui.setammo(magazine + "/" + ammo);
            }
        }
        else
        {
            reloadTime -= Time.deltaTime;
        }

        if (isReloading && reloadTime <= 1 && weaponType == "Shotgun")
        {
            reloadTime = 0;
            animations.SetInteger("Reload", -1);
            isReloading = false;
            ammo = ammo - 6 + magazine;
            magazine = magazineTemp;
            if (ammo < 0)
            {
                magazine += ammo;
                ammo = 0;
                ui.setammo(magazine + "/" + ammo);
            }
        }
        else
        {
            reloadTime -= Time.deltaTime;
        }

        if (isReloading && reloadTime <= 1 && weaponType == "SMG")
        {
            reloadTime = 0;
            animations.SetInteger("Reload", -1);
            isReloading = false;
            ammo = ammo - 45 + magazine;
            magazine = magazineTemp;
            if (ammo < 0)
            {
                magazine += ammo;
                ammo = 0;
                ui.setammo(magazine + "/" + ammo);
            }
        }
        else
        {
            reloadTime -= Time.deltaTime;
        }

        if (Input.GetMouseButton(1)) // Right mouse button
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, positionSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, normalPosition, positionSpeed * Time.deltaTime);
        }
    }

    private void PlaySound(List<AudioClip> availableSounds)
    {
        gunSource.pitch = Random.Range(0.95f, 1.05f);
        gunSource.PlayOneShot(availableSounds[Random.Range(0, availableSounds.Count)], Random.Range(0.65f, 0.85f));
    }

    // Sound helper functions
    public void PlayReload()
    {
        PlaySound(reloadSounds);
    }

    public void PlayFire()
    {
        PlaySound(gunfireSounds);
    }

    public void PlayLevelUp()
    {
        PlaySound(levelupSounds);
    }

    private void LateUpdate()
    {
        ui.setammo(magazine + "/" + ammo);
    }

    public void updateAmmo(int ammoDelta)
    {
        ammo = Mathf.Max(0, Mathf.Min(ammo + ammoDelta, maxAmmo));
    }

    // Gun upgrade stats
    private int kills = 0, currentThreshold = 0;

    public int[] killThreshold =
    {
        5,
        10,
        20,
        30
    };

    public void addKill()
    {
        kills++;
        if (currentThreshold >= killThreshold.Length) return;
        if (kills == killThreshold[currentThreshold])
        {
            Debug.Log("Damage upgraded by " + damageUpgrade);
            StartCoroutine(LevelUpCoroutine());
        }
    }

    private IEnumerator LevelUpCoroutine()
    {
        yield return new WaitForSeconds(0.75f);
        PlayLevelUp();
        damage += damageUpgrade;
        currentThreshold++;
    }

    private void fire()
    {
        RaycastHit hit;
        magazine--;

        if (Physics.Raycast(cameraGameObject.transform.position, cameraGameObject.transform.forward, out hit))
        {
            EnemyManager enemyManager = hit.transform.GetComponent<EnemyManager>();
            BossStateMachine bossState = hit.transform.GetComponent<BossStateMachine>();

            if (bossState != null)
            {
                bossState.health -= damage;
            }
            else if (enemyManager != null)
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

    private void fireShotgun()
    {
        RaycastHit hit;
        magazine--;

        int pellets = 10;
        float maxSpreadAngle = 5.0f;

        for (int i = 0; i < pellets; i++)
        {
            // Randomize the spread for each pellet within a cone
            float angle = Random.Range(-maxSpreadAngle, maxSpreadAngle);
            float angleY = Random.Range(-maxSpreadAngle, maxSpreadAngle);

            // Calculate the spread direction with the forward direction as the central axis
            Vector3 spreadDirection = Quaternion.Euler(angle, angleY, 0) * cameraGameObject.transform.forward;

            if (Physics.Raycast(cameraGameObject.transform.position, spreadDirection, out hit))
            {
                // Debug.DrawLine(transform.position, hit.point, Color.red, 1.0f);
                Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }
}
