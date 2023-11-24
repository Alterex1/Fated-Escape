using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AmmoManager ui;

    public float fireRate = 4f;
    public GameObject cameraGameObject;
    public GameObject bulletEffect, hitEffect;
    private Animator animations;
    private InputManager inputs;
    private float reloadTime = 0;
    public float reloadAnimationTime = 2.5f;
    public int magazine, ammo;
    public int maxAmmo, magazineCap = 30, magCount = 3;

    public float damage = 10f;

    private float readyToFire = 0;
    private int magazineTemp;
    public bool isReloading = false;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<AmmoManager>();
        animations = gameObject.GetComponent<Animator>();
        inputs = gameObject.GetComponent<InputManager>();
        animations.SetInteger("Movement", 0);

        magazine = magazineCap;
        maxAmmo = magazineCap * magCount;
        ammo = maxAmmo;

        ui.setammo(magazine + "/" + ammo);
        ui.setWeaponToDisplay(0);
    }

    private void Update()
    {
        if (Time.time >= readyToFire)
        {
            animations.SetInteger("Fire", -1);
            animations.SetInteger("Movement", (inputs.vertical == 0 && inputs.horizontal == 0) ? 0 : 1);
        }

        if (Input.GetMouseButton(0) && Time.time >= readyToFire && !isReloading && magazine > 0)
        {
            readyToFire = Time.time + 1f / fireRate;
            fire();
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

    public void updateAmmo(int ammoDelta)
    {
        ammo = Mathf.Max(0, Mathf.Min(ammo + ammoDelta, maxAmmo));
    }

    private void LateUpdate()
    {
        ui.setammo(magazine + "/" + ammo);
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
