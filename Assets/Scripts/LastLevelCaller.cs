using UnityEngine;

public class LastLevelCaller : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPrefab;
    public GameObject particleEffectPrefab;
    public string[] tagsToDestroy;

    public bool hasActivated = false;
    public int lastLevelOpenLevel = 95;
    public GameObject levelUpinterface, enemySpawner;

    public EnemySpawner enemySpawnerScript;


    public void CheckLevelReached(int level)
    {
        levelUpinterface.SetActive(false);
        enemySpawner.SetActive(false);
        Time.timeScale = 1f;
        Invoke("SummonNewEnemy", 1f);

        DestroyAllObjectsExceptPlayer();
        hasActivated = true;
    }

    void DestroyAllObjectsExceptPlayer()
    {
        GameObject[] allObjects = FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj != player && !obj.CompareTag("Player"))
            {
                foreach (string tag in tagsToDestroy)
                {
                    if (obj.CompareTag(tag))
                    {
                        CreateParticleEffect(obj.transform.position);
                        Destroy(obj);

                    }
                }
            }
        }
    }

    void CreateParticleEffect(Vector3 position)
    {
        

        Instantiate(particleEffectPrefab, position, Quaternion.identity);


    }

    void SummonNewEnemy()
    {
        Instantiate(enemyPrefab, enemySpawnerScript.SelectSpawnPosition(), Quaternion.identity);

    }
}
