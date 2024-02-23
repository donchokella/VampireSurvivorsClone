using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    public int coinAmountChance;
    public int coinAmount = 1;
    public float xxx = 1;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeBetweenChecks = 0.2f;

    private bool movingToPlayer;
    private float checkCounter;
    private PlayerController player;

    private SpriteRenderer sprite;

    public SpriteRenderer spriteBig; 

    private void Start()
    {
        xxx = coinAmountChance * Random.value;

        sprite = GetComponent<SpriteRenderer>();

        player = PlayerController.instance;

        if(xxx >= 20 )
        {
            sprite.sprite = spriteBig.sprite;
            coinAmount = 20;

        }
        else if(xxx >= 15 )
        {
            sprite.color = Color.green;
            coinAmount = 15;

        }
        else if(xxx >= 5)
        {
            sprite.color = Color.red;
            coinAmount = 5;

        }
        else
        {
            sprite.color = Color.white;
            coinAmount = 1;

        }


    }
    private void Update()
    {
        if (movingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if (checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;

                if (Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            CoinController.instance.AddCoins(coinAmount);

            Destroy(gameObject);
        }
    }
}
