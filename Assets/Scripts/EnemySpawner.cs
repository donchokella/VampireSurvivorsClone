using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{

    public GameObject foodSupplyToSpawn;
    private float foodSupplySpawnCounter;
    [SerializeField] private float timeToSpawnFoodSupply = 20f;

    public int checkPerFrame;

    private float spawnCounter;
    private Transform minSpawn, maxSpawn;
    private float despawnDistance;
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    private int enemyToCheck;

    [SerializeField] private float timeToSpawn;


    public List<WaveInfo> waves;

    private int currentWave;
    private float waveCounter;

    public List<string> enemyNames = new List<string>();

    private void Start()
    {
        for(int i = 0; i < InputManager.instance.playerInputs.Count; i++)
        {
            enemyNames.Add(InputManager.instance.playerInputs[i]);
        }

        spawnCounter = timeToSpawn;

        minSpawn = transform.GetChild(0);
        maxSpawn = transform.GetChild(1);

        despawnDistance = Vector3.Distance(PlayerHealthController.instance.transform.position, minSpawn.position) + 4f;

        currentWave = -1;
        GoToNextWave();
    }

    private void Update()
    {

        foodSupplySpawnCounter -= Time.deltaTime;

        if (foodSupplySpawnCounter <= 0) 
        {
            Instantiate(foodSupplyToSpawn, SelectSpawnPosition(), transform.rotation);

            foodSupplySpawnCounter = timeToSpawnFoodSupply;
        }
        

        if(PlayerHealthController.instance.gameObject.activeSelf)
        {
            if(currentWave < waves.Count)
            {
                waveCounter -= Time.deltaTime;
                if(waveCounter <= 0) 
                {
                    GoToNextWave();
                }

                spawnCounter -= Time.deltaTime;
                if (spawnCounter <= 0)
                {
                    spawnCounter = waves[currentWave].timeBetweenSpawns;

                    GameObject newEnemy = Instantiate(waves[currentWave].enemyToSpawn, SelectSpawnPosition(), Quaternion.identity);

                    Transform name = newEnemy.transform.GetChild(0);
                    Transform sprite = newEnemy.transform.GetChild(1);
                   
                    if (enemyNames[currentWave % enemyNames.Count] == "") 
                    {
                        //Debug.Log("current wave: " + currentWave);
                        //Debug.Log("current name: " + (currentWave % enemyNames.Count));
                        name.gameObject.SetActive(false);
                        sprite.gameObject.SetActive(true);

                    }
                    else
                    {
                        name.gameObject.SetActive(true);
                        sprite.gameObject.SetActive(false);
                        name.GetComponent<TMP_Text>().text = enemyNames[currentWave % enemyNames.Count];

                    }

                    spawnedEnemies.Add(newEnemy);
                }
            }
        }

        int checkTarget = enemyToCheck + checkPerFrame;

        while (enemyToCheck < checkTarget)
        {
            if(enemyToCheck < spawnedEnemies.Count)
            {
                if(spawnedEnemies[enemyToCheck] != null)
                {
                    if(Vector3.Distance(transform.position, spawnedEnemies[enemyToCheck].transform.position) > despawnDistance)
                    {
                        Destroy(spawnedEnemies[enemyToCheck]);

                        spawnedEnemies.RemoveAt(enemyToCheck);
                        checkTarget--;
                    }
                    else
                    {
                        enemyToCheck++;
                    }
                }
                else
                {
                    spawnedEnemies.RemoveAt(enemyToCheck);
                    checkTarget--;
                }
            }
            else
            {
                enemyToCheck = 0;
                checkTarget = 0;
            }
        }
    }

    public Vector3 SelectSpawnPosition()
    {
        Vector3 spawnPoint = Vector3.zero;
        bool spawnVerticalEdge = Random.Range(0f, 1f) > 0.5f; // It's a simple way of randomly deciding something true or false.

        if(spawnVerticalEdge)
        {
            if(Random.Range(0f,1f) > 0.5f)
            {
                spawnPoint.x = minSpawn.position.x;
            }
            else
            {
                spawnPoint.x = maxSpawn.position.x;
            }

            spawnPoint.y = Random.Range(minSpawn.position.y, maxSpawn.position.y);
        }
        else
        {
            spawnPoint.x = Random.Range(minSpawn.position.x, maxSpawn.position.x);

            if (Random.Range(0f, 1f) > 0.5f)
            {
                spawnPoint.y = minSpawn.position.y;
            }
            else
            {
                spawnPoint.y = maxSpawn.position.y;
            }
        }
        return spawnPoint;
    }

    public void GoToNextWave()
    {
        currentWave++;
        if(currentWave >= waves.Count)
        {
            currentWave = waves.Count - 1;
        }

        waveCounter = waves[currentWave].waveLength;
        spawnCounter = waves[currentWave].timeBetweenSpawns;
    }
}



[System.Serializable]

public class WaveInfo
{
    public GameObject enemyToSpawn;
    public float waveLength = 10f;
    public float timeBetweenSpawns = 1f;
}
