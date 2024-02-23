using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    // Singleton Design Pattern STARTS
    public static PlayerHealthController instance;

    private void Awake()
    {
        instance = this;
    }
    // Singleton Design Pattern ENDS

    private Slider healthSlider;

    public float currentHealth, maxHealth;

    public GameObject deathEffect;
    public float finalScreenCheck = 1.15f;

    private void Start()
    {
        maxHealth = PlayerStatController.instance.health[0].value;
        
        healthSlider = GetComponentInChildren<Slider>();

        currentHealth = maxHealth;

        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }


    public void TakeDamage(float damageToTake)
    {
        currentHealth -= damageToTake;

        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            LevelManager.instance.EndLevel();

            Instantiate(deathEffect, transform.position, transform.rotation);
            SFXManager.instance.PlaySFX(3);
            BGMusicManager.instance.PlayGameOverMusic();

        }
        else if(currentHealth >  maxHealth*finalScreenCheck)
        {
            UIController.instance.OpenFinalScreen();
            BGMusicManager.instance.PlayVictoryMusic();
        }
        healthSlider.value = currentHealth;
    }
}
