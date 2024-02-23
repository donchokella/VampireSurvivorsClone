using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HamburgerPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(PlayerHealthController.instance.currentHealth < PlayerHealthController.instance.maxHealth)
            {
                PlayerHealthController.instance.TakeDamage(-20);
                SFXManager.instance.PlaySFXPitched(11);


                if (PlayerHealthController.instance.currentHealth > PlayerHealthController.instance.maxHealth)
                {
                    PlayerHealthController.instance.currentHealth = PlayerHealthController.instance.maxHealth;
                }
            }
            Destroy(gameObject);
        }
    }
}
