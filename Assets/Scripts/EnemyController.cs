using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Rigidbody2D theRB;
    private Transform target;
    private float hitCounter;
    private float knockBackCounter;
    
    public float moveSpeed;
    [SerializeField] private float damage;
    [SerializeField] private float health = 5f;

    [SerializeField] private float hitWaitTime = 1f;
    [SerializeField] private float knockBackTime = 0.5f;
    [SerializeField] private int expToGiveChance = 1;
    [SerializeField]  private int coinToGiveChance = 1;

    public float expDropRate = 0.7f;
    public float coinDropRate = 0.4f;

    public TMP_Text enemyName;


    private void Start()
    {
        //target = FindObjectOfType<PlayerController>().transform; // Not the best way to do it i guess.
        target = PlayerHealthController.instance.transform; // This is a much more efficient way of doing this.
        
        theRB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (PlayerController.instance.gameObject.activeSelf)
        {
            if (knockBackCounter > 0)
            {
                knockBackCounter -= Time.deltaTime;

                if (moveSpeed > 0)
                {
                    moveSpeed = -moveSpeed * 2;
                }

                if (knockBackCounter <= 0)
                {
                    moveSpeed = Mathf.Abs(moveSpeed * 0.5f);
                }
            }

            theRB.velocity = (target.position - transform.position).normalized * moveSpeed;

            if (hitCounter > 0f)
            {
                hitCounter -= Time.deltaTime;
            }
        }
        else
        {
            theRB.velocity = (target.position - transform.position).normalized * -moveSpeed;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter <= 0) 
        {
            PlayerHealthController.instance.TakeDamage(damage);

            hitCounter = hitWaitTime;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && hitCounter <= 0)
        {
            PlayerHealthController.instance.TakeDamage(damage);

            hitCounter = hitWaitTime;
        }
    }


    public void TakeDamage(float damageToTake)
    {
        health -= damageToTake;

        if (health <= 0)
        {
            Destroy(gameObject);

           
            if(Random.value <= expDropRate)
            {
                ExperienceLevelController.instance.SpawnExp(transform.position, expToGiveChance);
            }

            if (Random.value <= coinDropRate)
            {
                CoinController.instance.DropCoin(transform.position, coinToGiveChance);
            }
            SFXManager.instance.PlaySFXPitched(0);
        }
        else
        {
            SFXManager.instance.PlaySFXPitched(1);
        }

        DamageNumberController.instance.SpawnDamage(damageToTake, transform.position);
    }
    public void TakeDamage(float damageToTake, bool shouldKnockBack)
    {
        TakeDamage(damageToTake);

        if(shouldKnockBack )
        {
            knockBackCounter = knockBackTime;
        }
    }
}
