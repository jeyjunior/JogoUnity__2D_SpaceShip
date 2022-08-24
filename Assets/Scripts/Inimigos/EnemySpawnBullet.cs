using System.Collections;
using UnityEngine;

public class EnemySpawnBullet : MonoBehaviour
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

    bool spawnThree = false;

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
                case 0:
                    //Spawna apenas uma bullet em um unico spawnPoint
                    StartCoroutine(SpawnSingleBullet());
                    break;
                case 1:
                    //spawnpoint 0 = esquerda / 2 = direita / 1 = central
                    StartCoroutine(SpawnMultipleBulletVariantOne(new int[] {0,0,0}, new int[] {0,2,1}));
                    break;
                case 2:
                    StartCoroutine(SpawnMultipleBulletVariantTwo(Random.Range(0,2)));
                    break;
                case 3:
                    StartCoroutine(SpawnMultipleBulletVariantThree());
                    break;
            }
            
            bulletSpawned = true;
        }
    }

    //eBulletA = Unica bala que se move em um unico eixo
    //eBulletB = Três balas, central grande, laterias menores, se movem em um unico eixo
    //eBulletC = Três balas, todas mesmo tamanho, se movem em um unico eixo
    //eBulletD = Três balas, todas mesmo tamanho, balas lateriais sem movem no eixo X e Y, (gerando um "modelo de cone"), central se move apenas no Y
    //eBulletE = Unica bala, se move na direção do player

    IEnumerator SpawnSingleBullet()
    {
        yield return new WaitForSeconds(randomSpawn);

        InstantiateBullets(0,0);

        randomSpawn = Mathf.RoundToInt(Random.Range(1, 5));
        bulletSpawned = false;
    }

    IEnumerator SpawnMultipleBulletVariantOne(int[] indexBullet, int[] indexSpawnBullet )
    {
        //primeiro array define qual modelo de bullet será usado
        //segundo array define qual o spawnpoint será utilizado
        int random = Random.Range(0, 2);
        yield return new WaitForSeconds(randomSpawn);

        if(random < 1)
        {
            //Spawna bullet dos spawnpoints laterais
            InstantiateBullets(indexBullet[0], indexSpawnBullet[0]);
            InstantiateBullets(indexBullet[1], indexSpawnBullet[1]);
        }
        else
        {
            //Spawna bullet do spawnpoint central
            InstantiateBullets(indexBullet[2], indexSpawnBullet[2]);
        }
        
        randomSpawn = Mathf.RoundToInt(Random.Range(minRandomSpawn, maxRandomSpawn));
        bulletSpawned = false;
    }

    IEnumerator SpawnMultipleBulletVariantTwo(int value)
    {
        
        if(value < 1)
        {
            //Spawna na sequencia direita, centro esquerda
            for (int i = 0; i < 3; i++)
            {
                InstantiateBullets(0, i);
                yield return new WaitForSeconds(1);
            }
            bulletSpawned = false;
        }
        else
        {
            //Spawna na sequencia esquerda, centro direita
            for (int i = 2; i >= 0; i--)
            {
                InstantiateBullets(0, i);
                yield return new WaitForSeconds(1);
            }
            bulletSpawned = false;
        }

    }

    IEnumerator SpawnMultipleBulletVariantThree()
    {
        //Three é uma junção aleatoria da variação One, Two and Four
        yield return new WaitForSeconds(3);

        if (!spawnThree)
        {
            int random = (Random.Range(0, 2));
            spawnThree = true;

            if (random < 1)
            {
                StartCoroutine(SpawnMultipleBulletVariantOne(new int[] { 0, 0, 0 }, new int[] { 0, 2, 1 }));
                yield return new WaitForSeconds(2);
                spawnThree = false;
            }
            else
            {
                StartCoroutine(SpawnMultipleBulletVariantTwo(Random.Range(0,2)));
                yield return new WaitForSeconds(2);
                spawnThree = false;
            }
        }

        bulletSpawned = false;
    }


    void InstantiateBullets(int indexBullet, int indexSpawnPoint)
    {
        Instantiate(bullet[indexBullet], spawnPoint[indexSpawnPoint].transform.position, Quaternion.identity);
    }

}
