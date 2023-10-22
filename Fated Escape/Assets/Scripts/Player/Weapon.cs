using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AmmoManager ui;

    public float fireRate = 4f;
    public GameObject cameraGameObject;
    public GameObject bulletEffect;
    private Animator animations;
    private InputManager inputs;
    private float reloadTime = 0;
    public float reloadAnimationTime = 2.5f;
    public int magazine = 30, ammo, mags = 3;

    private float readyToFire;
    private int magazineTemp;
    public bool isReloading = false;

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("Player").GetComponent<AmmoManager>();
        animations = gameObject.GetComponent<Animator>();
        inputs = gameObject.GetComponent<InputManager>();
        animations.SetInteger("Movement", 0);
        ammo = magazine * mags;
        magazineTemp = magazine;

        ui.setammo(magazine + "/" + ammo);
        ui.setWeaponToDisplay(0);
    }
    private void FixedUpdate()
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

        if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammo > 0)
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
            ammo = ammo - 30 + magazine;
            magazine = magazineTemp;
            if (ammo < 0)
            {
                magazine += ammo;
                ammo = 0;
                ui.setammo(magazine + "/" + ammo);
            }
            ui.setammo(magazine + "/" + ammo);
        }
        else
        {
            reloadTime -= Time.deltaTime;
        }
    }

    private void fire()
    {
        RaycastHit hit;
        magazine--;
        ui.setammo(magazine + "/" + ammo);
        if (Physics.Raycast(cameraGameObject.transform.position, cameraGameObject.transform.forward, out hit))
        {
            Debug.DrawLine(transform.position, hit.point);
            Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
