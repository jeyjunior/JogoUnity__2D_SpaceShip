using System.Collections;
using UnityEngine;

enum BulletVariant { SingleBullet, MultipleOne, MultipleTwo, MultipleThree };


public class EnemySpawnBullet : MonoBehaviour
{

    public GameObject[] bullet;
    public GameObject[] spawnPoint;

    //bulletVariant: Qual o tiro e a sequencia que sera utilizada
    [Space(5)]
    [Header("Bullet Type and Behavior")]
    [SerializeField]
    BulletVariant bulletVariant = new BulletVariant();

    [Space(5)]
    [Header("Delay para Spawnar Bullet")]
    public int minRandomSpawn;
    public int maxRandomSpawn;

    int randomSpawn; //Delay do proximo tiro randomSpawn = (min, max)
    int bulletType; //bulletType = Random.Range(0, bullet.Length) - Spawna o tiro conforme o valor sorteado
    bool spawnThree = false; //spawnThree usada para controlar MultipleThree
    bool bulletSpawned = false; //Controlar se uma bullet foi ou não spawnada


    private void Start()
    {
        randomSpawn = 5;
        bulletType = Random.Range(0, bullet.Length);
    }

    private void Update()
    {

        if(!bulletSpawned)
        {
            switch (bulletVariant)
            {
                case BulletVariant.SingleBullet:
                    //Spawna apenas uma bullet em um unico spawnPoint
                    StartCoroutine(SpawnSingleBullet(bulletType));
                    break;
                case BulletVariant.MultipleOne:
                    //spawnpoint 0 = esquerda / 2 = direita / 1 = central
                    StartCoroutine(SpawnMultipleBulletVariantOne(new int[] {0,2,1}));
                    break;
                case BulletVariant.MultipleTwo:
                    StartCoroutine(SpawnMultipleBulletVariantTwo(Random.Range(0,2)));
                    break;
                case BulletVariant.MultipleThree:
                    StartCoroutine(SpawnMultipleBulletVariantThree());
                    break;
                default:
                    StartCoroutine(SpawnSingleBullet(bulletType));
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

    IEnumerator SpawnSingleBullet(int index)
    {
        yield return new WaitForSeconds(randomSpawn);

        InstantiateBullets(index,0);

        randomSpawn = Mathf.RoundToInt(Random.Range(1, 5));
        bulletSpawned = false;
    }

    IEnumerator SpawnMultipleBulletVariantOne(int[] indexSpawnBullet )
    {
        //primeiro array define qual modelo de bullet será usado
        //segundo array define qual o spawnpoint será utilizado
        int random = Random.Range(0, 2);
        yield return new WaitForSeconds(randomSpawn);

        if(random < 1)
        {
            //Spawna bullet dos spawnpoints laterais
            InstantiateBullets(bulletType, indexSpawnBullet[0]);
            InstantiateBullets(bulletType, indexSpawnBullet[1]);
        }
        else
        {
            //Spawna bullet do spawnpoint central
            InstantiateBullets(bulletType, indexSpawnBullet[2]);
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
                InstantiateBullets(bulletType, i);
                yield return new WaitForSeconds(1);
            }
            bulletSpawned = false;
        }
        else
        {
            //Spawna na sequencia esquerda, centro direita
            for (int i = 2; i >= 0; i--)
            {
                InstantiateBullets(bulletType, i);
                yield return new WaitForSeconds(1);
            }
            bulletSpawned = false;
        }

    }

    IEnumerator SpawnMultipleBulletVariantThree()
    {
        //Three é uma junção aleatoria da variação One and two
        yield return new WaitForSeconds(3);

        if (!spawnThree)
        {
            int random = (Random.Range(0, 2));
            spawnThree = true;

            if (random < 1)
            {
                StartCoroutine(SpawnMultipleBulletVariantOne(new int[] { 0, 2, 1 }));
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
