using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowerUp : MonoBehaviour
{
    public InputActionReference UsePowerUpAction;
    public InputActionReference UseShield;

    public TextMeshProUGUI PowerUp;

    public float slowMultiplier = 0.3f;
    public float slowDuartion = 3.0f;
    public float cooldownTime = 10.0f;

    private bool isCooldown = false;
    private bool isSlowed = false;
    private Rigidbody rb;

    public GameObject Shield;
    public bool hasShield = false;
    public bool isInvicible = false;
    public bool isOnCooldown = false;

    public float cooldownShield = 20f;
    public float invicibilityDuration = 2.0f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        UsePowerUpAction.action.performed += ctx => TryActivePowerUp();
        UseShield.action.performed += ctx => ActiveShield();
    }

    void OnEnable()
    {
       UsePowerUpAction.action.Enable(); 
       UseShield.action.Enable();
    }

    void OnDisable()
    {
        UsePowerUpAction.action.Disable();
        UseShield.action.Disable();
    }

    void FixedUpdate()
    {
        if (isSlowed)
        {
            rb.velocity = rb.velocity * slowMultiplier;
        }

        if (isCooldown == true) 
        {
            PowerUp.text = "Slow (E) en Cooldown";
        }

        if (isSlowed)
        {
            PowerUp.text = "Slow (E) en slow";
        }

        if (isCooldown == false) 
        {
            PowerUp.text = "Slow (E) peut être utilisé";
        }
    }

    public void ActiveShield()
    {
        if (!isOnCooldown && !hasShield)
        {
            hasShield = true;
            if(Shield) Shield.SetActive(true);
        }
        else if (isOnCooldown == true)
        {
            Debug.Log("Cooldown");
        }
        else if (hasShield == true)
        {
            Debug.Log("Deja un shield");
        }
    }

    private void TryActivePowerUp()
    {
        if (!isCooldown)
        {
            StartCoroutine(SlowEffect()); 
        }
    }

    IEnumerator SlowEffect()
    {
        isSlowed = true;
        isCooldown = true;

        yield return new WaitForSeconds(slowDuartion);

        isSlowed = false;

        yield return new WaitForSeconds(cooldownTime);
        isCooldown = false;
    }

    public bool AbsorbDamage()
    {
        if (isInvicible) return true;
        if (hasShield)
        {
            hasShield = false;
            if (Shield) Shield.SetActive(false);
            StartCoroutine(Inviciblity());
            StartCoroutine(CooldownShield());

            return true;
        }

        return false;
    }

    public IEnumerator CooldownShield()
    {
        isOnCooldown = true;
        Debug.Log("Cooldown lance");
        yield return new WaitForSeconds (cooldownShield);
        isOnCooldown = false;
        Debug.Log("Shield pret !");
    }

    public IEnumerator Inviciblity()
    {
        isInvicible = true;
        Debug.Log("Invincible");
        yield return new WaitForSeconds(invicibilityDuration);
        isInvicible = false;
        Debug.Log("Fin Invincible");
    }
}
