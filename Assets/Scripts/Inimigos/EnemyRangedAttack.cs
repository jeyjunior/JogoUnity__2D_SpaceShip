using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemyRangedAttack : MonoBehaviour
{

    public GameObject bullet;
    public GameObject spawnPoint;

    EnemyBase enemyBase;

    [Space(5)]
    [Header("Random Time To Spawn Bullets")]
    public int randomSpawn;

    [Space(5)]
    [Header("Index bullets and SpawnPoint")]
    public int chooseBulletIndex;
    public int chooseSpawnPointIndex;

    public bool bulletSpawned = false;

    private void Start()
    {
        randomSpawn = 5;
    }

    private void Update()
    {
        if(!bulletSpawned)
        {
            bulletSpawned = true;
            StartCoroutine(SpawnBullet());
        }
    }


    IEnumerator SpawnBullet()
    {
        yield return new WaitForSeconds(randomSpawn);

        Instantiate(bullet,
                    spawnPoint.transform.position,
                    Quaternion.identity);
        
        randomSpawn = Mathf.RoundToInt(Random.Range(1, 5));
        bulletSpawned = false;
    }
}
