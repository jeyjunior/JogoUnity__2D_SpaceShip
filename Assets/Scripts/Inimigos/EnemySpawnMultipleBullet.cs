using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawnMultipleBullet : MonoBehaviour
{

    public GameObject[] bullet;
    public GameObject[] spawnPoint;

    [Space(5)]
    [Header("Random Time To Spawn Bullets")]
    public int minRandomSpawn;
    public int maxRandomSpawn;
    int randomSpawn;

    [Space(5)]
    [Header("Check if bullet is spawned")]
    public bool bulletSpawned = false;

    //variant define qual a variação da sequencia dos spawns multiplos
    //ex.: 3 bullets diferentes para 1 spawn point ou 3 spawnpoints e 1 bullet
    [Space(5)]
    [Header("Choose the sequence of spawnBullets")]
    [Tooltip("1 = VariantOne Method")]
    public int variant = 1;

    public bool bossOne, bossTwo, bossTree;


    private void Start()
    {
        randomSpawn = 5;
    }

    private void Update()
    {

        if(!bulletSpawned)
        {
            switch (variant)
            {
                case 1:
                    StartCoroutine(SpawnMultipleBulletVariantOne());
                    break;
                case 2:
                    StartCoroutine(SpawnMultipleBulletVariantTwo());
                    break;
                case 3:
                    StartCoroutine(SpawnMultipleBulletVariantThree());
                    break;
            }
            
            bulletSpawned = true;
        }
    }


    IEnumerator SpawnMultipleBulletVariantOne()
    {
        int random = Random.Range(0, 2);
        //SingleBullet spawna sempre do index 0 do bullet e spawnpoint
        yield return new WaitForSeconds(randomSpawn);

        if(random < 1)
        {
            //Spawn bullets ao mesmo tempo no spawnpoint da direita e esquerda
            Instantiate(bullet[0], spawnPoint[0].transform.position, Quaternion.identity);
            Instantiate(bullet[0], spawnPoint[2].transform.position, Quaternion.identity);
        }
        else
        {
            //Spawna bullet do spawnpoint central
            Instantiate(bullet[0], spawnPoint[1].transform.position, Quaternion.identity);
        }
        
        randomSpawn = Mathf.RoundToInt(Random.Range(minRandomSpawn, maxRandomSpawn));
        bulletSpawned = false;
    }

    IEnumerator SpawnMultipleBulletVariantTwo()
    {
        
        for(int i = 0; i < 3; i++)
        {
            Instantiate(bullet[0], spawnPoint[i].transform.position, Quaternion.identity);
            Debug.Log(i);
            yield return new WaitForSeconds(1);
        }
        bulletSpawned = false;
    }

    IEnumerator SpawnMultipleBulletVariantThree()
    {
        //Three é uma junção aleatoria da variação One e Two

        float random = Random.Range(0, 2);
        float randomTime = Random.Range(2, 10);
        Debug.Log($"Random: {random}");
        Debug.Log($"RandomTime: {randomTime}");

        if ( random < 1)
        {
            for(int i = 0; i < randomTime; i++)
            {
                StartCoroutine(SpawnMultipleBulletVariantTwo());
                yield return new WaitForSeconds(2);
            }
        }
        else
        {
            for (int i = 0; i < randomTime; i++)
            {
                StartCoroutine(SpawnMultipleBulletVariantOne());
                yield return new WaitForSeconds(2);
            }
        }

        
    }
}
