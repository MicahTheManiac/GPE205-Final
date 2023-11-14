using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    // Health Variables
    public float currentHealth;
    public float maxHealth;

    // UI Variables
    public bool drawIndicator = false;
    public Image indicatorImg;

    // Start is called before the first frame update
    void Start()
    {
        // Set Health to Max
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // Do Nothing
    }

    // Take Damage
    public void TakeDamage(float amount, Pawn source)
    {
        // Do Damage
        currentHealth -= amount;

        // Clamp Health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Print to Console
        Debug.Log(source.name + " did " + amount + " dmg to " + gameObject.name);
        Debug.Log(gameObject.name + "'s health is " + currentHealth);

        // Check to see if Health <= 0
        if (currentHealth <= 0)
        {
            Die(source);
        }

        // Update Indicator
        CalculateImageFill();
    }

    // Die
    public void Die(Pawn source)
    {
        // If GameManager is Valid
        if (GameManager.instance != null)
        {
            // If LevelManager is Valid
            if (LevelManager.instance != null)
            {
                Pawn playerPawn = LevelManager.instance.playerPawn;

                // If our Source is the Player Pawn
                if (source = playerPawn)
                {
                    GameManager.instance.AddCredits(100);
                    LevelManager.instance.CheckForCredits();
                }
            }
        }
        Destroy(gameObject, 0.1f);
    }

    // Healing
    public void Heal(float amount, Pawn source)
    {
        // Do Healing
        currentHealth += amount;

        // Clamp Health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Print to Console
        Debug.Log(source.name + " did " + amount + " healing to " + gameObject.name);
        Debug.Log(gameObject.name + "'s health is " + currentHealth);

        // Update Indicator
        CalculateImageFill();
    }

    // Calculate Image Fill
    private void CalculateImageFill()
    {
        if (drawIndicator)
        {
            // Perform Calculation and Clamp for good measure
            float percentHealth = currentHealth / maxHealth;
            percentHealth = Mathf.Clamp01(percentHealth);

            // Set Fill
            indicatorImg.fillAmount = percentHealth;
        }
    }
}
