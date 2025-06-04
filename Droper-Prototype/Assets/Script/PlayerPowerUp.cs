using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPowerUp : MonoBehaviour
{
    public InputActionReference UsePowerUpAction;

    public TextMeshProUGUI PowerUp;

    public float slowMultiplier = 0.3f;
    public float slowDuartion = 3.0f;
    public float cooldownTime = 10.0f;

    private bool isCooldown = false;
    private bool isSlowed = false;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        UsePowerUpAction.action.performed += ctx => TryActivePowerUp();
    }

    void OnEnable()
    {
       UsePowerUpAction.action.Enable(); 
    }

    void OnDisable()
    {
        UsePowerUpAction.action.Disable();
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
            PowerUp.text = "Slow (E) peut �tre utilis�";
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
}
