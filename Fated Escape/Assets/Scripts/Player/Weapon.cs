using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    public float fireRate = 20f;
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
        animations = gameObject.GetComponent<Animator>();
        inputs = gameObject.GetComponent<InputManager>();
        animations.SetInteger("Movement", 0);
        ammo = magazine * mags;
        magazineTemp = magazine;
    }
    private void FixedUpdate()
    {
        if(Time.time >= readyToFire) 
        {
            animations.SetInteger("Fire", -1);
            animations.SetInteger("Movement", (inputs.vertical == 0 && inputs.horizontal == 0) ? 0 : 1);
        }
        if (Input.GetMouseButton(0) && Time.time >= readyToFire && !isReloading && magazine > 0) 
        {
            readyToFire = Time.time + 1f/fireRate;
            fire();
            animations.SetInteger("Fire", 2);
            animations.SetInteger("Movement", -1);
        }

        if (Input.GetKeyDown(KeyCode.O) && !isReloading) 
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
            ammo -= 30 + magazine;
            magazine = 30;
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
        if (Physics.Raycast(cameraGameObject.transform.position, cameraGameObject.transform.forward, out hit)) 
        {
            Debug.DrawLine(transform.position,hit.point);
            Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
}
