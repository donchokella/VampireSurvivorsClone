using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWeapon : Weapon
{
    [SerializeField] private float rotateSpeed;
    [SerializeField] private float timeBetweenSpawn;

    [SerializeField] private Transform holder, fireballToSpawn;
    private float spawnCounter;

    public EnemyDamager damager;

    private void Start()
    {
        fireballToSpawn = transform.GetChild(0);
        holder = transform.GetChild(1);

        SetStats();

        //UIController.instance.levelUpButtons[0].UpdateButtonDisplay(this);
        //UIController.instance.firstWeaponsSelectionButtons[0].UpdateButtonDisplay(this);
    }

    private void Update()
    {
        //holder.rotation = Quaternion.Euler(0f,0f,holder.rotation.eulerAngles.z+(rotateSpeed*Time.deltaTime));
        holder.rotation = Quaternion.Euler(0f, 0f,holder.rotation.eulerAngles.z+(rotateSpeed*Time.deltaTime * stats[weaponLevel].speed));

        spawnCounter -= Time.deltaTime;
        if (spawnCounter <= 0)
        {
            spawnCounter = timeBetweenSpawn;

            //Instantiate(fireballToSpawn, fireballToSpawn.position, fireballToSpawn.rotation, holder).gameObject.SetActive(true);
            for (int i = 0; i < stats[weaponLevel].amount; i++)
            {
                float rot = (360f / stats[weaponLevel].amount) * i;
                Instantiate(fireballToSpawn, fireballToSpawn.position,Quaternion.Euler(0, 0, rot), holder).gameObject.SetActive(true);

                SFXManager.instance.PlaySFXPitched(8);

            }

        }
        if(statsUpdated)
        {
            statsUpdated = false;

            SetStats();
        }
    }

    public void SetStats()
    {
        damager.damageAmount = stats[weaponLevel].damage;

        transform.localScale = Vector3.one * stats[weaponLevel].range;  // look like doesnt work

        timeBetweenSpawn = stats[weaponLevel].timeBetweenAttacks;

        damager.lifeTime = stats[weaponLevel].duration;

        spawnCounter = 0;
    }
}
