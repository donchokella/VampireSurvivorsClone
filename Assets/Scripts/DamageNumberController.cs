using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageNumberController : MonoBehaviour
{
    public static DamageNumberController instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private DamageNumber numberToSpawn;
    [SerializeField] private Transform numberCanvas;

    private List<DamageNumber> numberPool = new List<DamageNumber>();



    public void SpawnDamage(float damageAmount, Vector3 location)
    {
        int rounded = Mathf.RoundToInt(damageAmount);
        //DamageNumber newDamage = Instantiate(numberToSpawn, location, Quaternion.identity, numberCanvas);

        DamageNumber newDamage = GetFromPool();

        newDamage.Setup(rounded);
        newDamage.gameObject.SetActive(true);

        newDamage.transform.position = location;

    }

    public DamageNumber GetFromPool()
    {
        DamageNumber numberToOutout = null;

        if (numberPool.Count == 0)
        {
            numberToOutout = Instantiate(numberToSpawn, numberCanvas);
        }
        else
        {
            numberToOutout = numberPool[0];
            numberPool.RemoveAt(0);
        }
        return numberToOutout;
    }
    public void PlaceInPool(DamageNumber numberToPlace)
    {
        numberToPlace.gameObject.SetActive(false);

        numberPool.Add(numberToPlace);
    }


}
