using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ExpPickup : MonoBehaviour
{
    public int expValueChance;
    public int expValueToGive = 1;
    public float xxx = 1;

    [SerializeField] private float moveSpeed;
    [SerializeField] private float timeBetweenChecks = 0.2f;

    private bool movingToPlayer;
    private float checkCounter;
    private PlayerController player;

    private SpriteRenderer sprite;
    public float ScaleMultiplier = 2;


    private void Start()
    {
        xxx = expValueChance * Random.value; 

        sprite = GetComponentInChildren<SpriteRenderer>();

        player = PlayerHealthController.instance.GetComponent<PlayerController>();

        if (xxx >= 90)
        {
            gameObject.transform.localScale = Vector3.one*ScaleMultiplier * 1.3f;

            sprite.color = Color.magenta;
            expValueToGive = 50;
        }
        else if (xxx >= 70)
        {
            gameObject.transform.localScale = Vector3.one * ScaleMultiplier* 1.2f;

            sprite.color = Color.gray;
            expValueToGive = 30;
        }
        else if (xxx >= 50)
        {
            gameObject.transform.localScale = Vector3.one * ScaleMultiplier * 1.1f;

            sprite.color = Color.blue;
            expValueToGive = 20;
        }
        else if (xxx >= 30)
        {
            sprite.color = Color.red;
            expValueToGive = 10;
        }
        else if (xxx >= 10)
        {
            sprite.color = Color.green;
            expValueToGive = 5;
        }
        else
        {
            sprite.color = Color.white;
            expValueToGive = 1;
        }

    }
    private void Update()
    {
        if(movingToPlayer)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, moveSpeed*Time.deltaTime);
        }
        else
        {
            checkCounter -= Time.deltaTime;
            if(checkCounter <= 0)
            {
                checkCounter = timeBetweenChecks;

                if(Vector3.Distance(transform.position, player.transform.position) < player.pickupRange)
                {
                    movingToPlayer = true;
                    moveSpeed += player.moveSpeed;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            ExperienceLevelController.instance.GetExp(expValueToGive);

            Destroy(gameObject);
        }
    }
}
