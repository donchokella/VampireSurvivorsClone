using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageNumber : MonoBehaviour
{
    [SerializeField] private TMP_Text damageText;
    [SerializeField] private float lifeTime;
    [SerializeField] private float floatSpeed = 1f;

    private float lifeCounter;

    private void Update()
    {
        if(lifeCounter > 0)
        {
            lifeCounter -= Time.deltaTime;

            if(lifeCounter <= 0)
            { 
                //Destroy(gameObject);
                DamageNumberController.instance.PlaceInPool(this);
            }
        }
        transform.position += Vector3.up * floatSpeed * Time.deltaTime;
    }
        
    public void Setup(int damageDisplay)
    {
        lifeCounter = lifeTime;

        damageText.text = damageDisplay.ToString();
    }
}
