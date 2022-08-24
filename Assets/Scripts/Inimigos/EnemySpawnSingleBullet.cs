using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnSingleBullet : MonoBehaviour
{

    public GameObject bullet;
    public GameObject spawnPoint;

    [Space(5)]
    [Header("Random Time To Spawn Bullets")]
    public int randomSpawn;


    public bool bulletSpawned = false;

    private void Start()
    {
        randomSpawn = 5;
    }

    private void Update()
    {

        if(!bulletSpawned)
        {
            StartCoroutine(SpawnSingleBullet());
            bulletSpawned = true;
        }
    }


    IEnumerator SpawnSingleBullet()
    {
        //SingleBullet spawna sempre do index 0 do bullet e spawnpoint
        yield return new WaitForSeconds(randomSpawn);

        Instantiate(bullet,
                    spawnPoint.transform.position,
                    Quaternion.identity);
        
        randomSpawn = Mathf.RoundToInt(Random.Range(1, 5));
        bulletSpawned = false;
    }
}
