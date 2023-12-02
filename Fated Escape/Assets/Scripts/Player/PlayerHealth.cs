using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public Transform playerTransform;
    private float health;
    private float lerpTimer;
    [Header("Health Bar")]
    public float maxHealth = 100f;
    public float chipSpeed = 2f;
    public Image frontHealthBar;
    public Image backHealthBar;
    private float elapsedTime = 0.0f;


    [Header("Damage Overlay")]
    public Image overlay;
    public float duration;
    public float fadeSpeed;
    private float durationTimer;

    [Header("Misc")]
    public float invincibilitySeconds = 0.05f;


    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, 0);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealthUI();

        elapsedTime += Time.deltaTime;
        if (elapsedTime > 5f && health < maxHealth)
        {
            RestorHealth(2);
            elapsedTime = 4.9f;
        }
    }

    public void UpdateHealthUI()
    {
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillB, hFraction, percentComplete);
        }

        if (fillF < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillF, backHealthBar.fillAmount, percentComplete);
        }

        if (health < 30)
        {
            if (overlay.color.a < 1f)
            {
                float tempAlpha = overlay.color.a;
                tempAlpha += Time.deltaTime * fadeSpeed;
                overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
            }
        }
        else
        {
            // If health is 30 or more, fade out the damage overlay
            if (overlay.color.a > 0f)
            {
                durationTimer += Time.deltaTime;
                if (durationTimer > duration)
                {
                    float tempAlpha = overlay.color.a;
                    tempAlpha -= Time.deltaTime * fadeSpeed;
                    overlay.color = new Color(overlay.color.r, overlay.color.g, overlay.color.b, tempAlpha);
                }
            }
        }


    }

    static float spawnX = -653.59f, spawnY = -74.71f, spawnZ = -429.57f;
    Vector3 respawn = new Vector3(spawnX, spawnY, spawnZ);

    public void TakeDamage(float damage)
    {
        if (elapsedTime < invincibilitySeconds) return;
        health -= damage;
        if (health <= 0)
        {
            playerTransform.position = respawn;
            health = 100f;
        }
        elapsedTime = 0f;
        lerpTimer = 0f;
    }

    public void RestorHealth(float healAmount)
    {
        health += healAmount;
        lerpTimer = 0f;
    }
}
