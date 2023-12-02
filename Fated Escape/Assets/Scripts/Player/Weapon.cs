using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public AmmoManager ui;
    public string weaponType;

    public float fireRate = 4f;
    public GameObject cameraGameObject;
    public GameObject bulletEffect;
    private Animator animations;
    private InputManager inputs;
    private float reloadTime = 0;
    public float reloadAnimationTime = 2.5f;
    public int magazine = 30, ammo, mags = 3;

    private float readyToFire = 0;
    private int magazineTemp;
    public bool isReloading = false;

    public Vector3 normalPosition; // Normal position of the weapon
    public Vector3 aimPosition; // Position of the weapon when aiming
    public float positionSpeed = 2f; // Speed of position change

    private void Start()
    {
        ui = GameObject.FindGameObjectWithTag("PlayerUI").GetComponent<AmmoManager>();
        animations = gameObject.GetComponent<Animator>();
        inputs = gameObject.GetComponent<InputManager>();
        animations.SetInteger("Movement", 0);
        ammo = magazine * mags;
        magazineTemp = magazine;

        ui.setammo(magazine + "/" + ammo);
        ui.setWeaponToDisplay(0);
    }

    private void Update()
    {
        if (Time.time >= readyToFire)
        {
            animations.SetInteger("Fire", -1);
            // Determine if the player is idle, walking, or sprinting
            int movementValue = Input.GetKeyDown(KeyCode.LeftShift) ? 2 : (inputs.vertical == 0 && inputs.horizontal == 0) ? 0 : 1;
            animations.SetInteger("Movement", movementValue);
        }

        if (Input.GetMouseButton(0) && Time.time >= readyToFire && !isReloading && magazine > 0 && (weaponType == "SMG" || weaponType == "AR"))
        {
            readyToFire = Time.time + 1f / fireRate;
            fire();
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

        if (Input.GetMouseButton(1)) //ADS
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, aimPosition, positionSpeed * Time.deltaTime);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, normalPosition, positionSpeed * Time.deltaTime);
        }

    }

    private void LateUpdate()
    {
        ui.setammo(magazine + "/" + ammo);
    }

    private void fire()
    {
        RaycastHit hit;
        magazine--;

        float maxDeviationAngle = 2.0f;

        float deviationX = Random.Range(-maxDeviationAngle, maxDeviationAngle);
        float deviationY = Random.Range(-maxDeviationAngle, maxDeviationAngle);

        Vector3 deviation = new Vector3(deviationX, deviationY, 0);
        Quaternion rotationWithDeviation = Quaternion.Euler(deviation) * cameraGameObject.transform.rotation;
        Vector3 directionWithDeviation = rotationWithDeviation * Vector3.forward;

        if (Physics.Raycast(cameraGameObject.transform.position, directionWithDeviation, out hit))
        {
            Debug.DrawLine(transform.position, hit.point, Color.green, 1.0f);
            Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
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
            float angle = Random.Range(-maxSpreadAngle, maxSpreadAngle);
            float angleY = Random.Range(-maxSpreadAngle, maxSpreadAngle);

            Vector3 spreadDirection = Quaternion.Euler(angle, angleY, 0) * cameraGameObject.transform.forward;

            if (Physics.Raycast(cameraGameObject.transform.position, spreadDirection, out hit))
            {
                Debug.DrawLine(transform.position, hit.point, Color.red, 1.0f);
                Instantiate(bulletEffect, hit.point, Quaternion.LookRotation(hit.normal));
            }
        }
    }



}
