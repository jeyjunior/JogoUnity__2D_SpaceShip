using System.Collections;
using UnityEngine;



public class EnemyController : MonoBehaviour
{
    [Space(5)]
    [Header("GameObjects")]
    public GameController gameController;

    //Sempre que spawnar um grupo de inimigos, sera registrado o total de inimigos na tela
    //Conforme esse número for zerando, uma outra onda de inimigos será spawnada
    public int enemysSpawned = 0;
    
    [Space(5)]
    [Header("Spawn Enemys Group")]
    public GameObject[] enemysGroup = new GameObject[1];
    public Vector2 spawnPosition;


    [Space(5)]
    public GameObject[] boss;
    public bool bigBossFight_;
    public bool bossSpawned;

    [Space(5)]
    public GameObject[] asteroids;
    bool asteroidSpawned;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        spawnPosition = new Vector2(0, 0);
    }

    private void Update()
    {
        //Só pode spawnar os inimigos comuns se a luta com chefão for false
        if (!bigBossFight_)
        {
            if(enemysSpawned <= 0)
            {
                Instantiate(enemysGroup[0], spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            //Inimigos não pode existir com o chefão
            Destroy(GameObject.FindGameObjectWithTag("Enemies"));
            BossSpawn();
        }

        if(!asteroidSpawned) StartCoroutine(AsteroidSpawn());
    }
 
    void BossSpawn()
    {
        if (!bossSpawned)
        {
            Instantiate(boss[Mathf.RoundToInt(Random.Range(0, boss.Length))], new Vector2(0,6), Quaternion.identity);
            bossSpawned = true;    
        }
    }


    IEnumerator AsteroidSpawn()
    {
        asteroidSpawned = true;
        yield return new WaitForSeconds(Random.Range(1, 30));

        Instantiate(asteroids[0], new Vector2(Random.Range(-2.5f, 2.5f), 6), Quaternion.identity);

    }
}
