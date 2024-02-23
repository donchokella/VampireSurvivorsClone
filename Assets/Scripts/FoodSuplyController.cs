using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FoodSuplyController : MonoBehaviour
{
    [SerializeField] private float health = 15f;
    [SerializeField] private float healthDropRate = 0.5f;
    [SerializeField] private GameObject healthPickup;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(true)
        {
            TakeDamage();
        }
    }
    public void TakeDamage()
    {
        health -= 5;

        if (health <= 0)
        {
            Destroy(gameObject);
     
            if (Random.value <= healthDropRate)
            {
                SpawnHealth();
            }

            SFXManager.instance.PlaySFXPitched(0);
        }
        else
        {
            SFXManager.instance.PlaySFXPitched(1);
        }
    }

    public void SpawnHealth()
    {
        Instantiate(healthPickup, transform.position, Quaternion.identity);
    }
}
