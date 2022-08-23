using UnityEngine;



public class EnemyController : MonoBehaviour
{
    [Space(5)]
    [Header("GameObjects")]
    public GameController gameController;

    //Sempre que spawnar um grupo de inimigos, sera registrado o total de inimigos na tela
    //Conforme esse número for zerando, uma outra onda de inimigos será spawnada
    public int enemysSpawned = 0;
    
    //Utilizar o spawn Sequence para determinar a sequencia dos grupos de inimigos que serão spawnandos
    int SpawnSequence;

    [Space(5)]
    [Header("Spawn Enemys Group")]
    public GameObject[] enemysGroup = new GameObject[1];
    public Vector2 spawnPosition;


    [Space(5)]
    public GameObject[] boss = new GameObject[1];
    public bool bigBossFight_;
    public bool bossSpawned;




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
                Instantiate(enemysGroup[Mathf.RoundToInt(Random.Range(0,4))], spawnPosition, Quaternion.identity);
            }
        }
        else
        {
            //Inimigos não pode existir com o chefão
            Destroy(GameObject.FindGameObjectWithTag("Enemies"));
            BossSpawn();
        }
    }
 
    void BossSpawn()
    {
        if (!bossSpawned)
        {
            Instantiate(boss[(int)Random.Range(0, boss.Length)], new Vector2(0,6), Quaternion.identity);
            bossSpawned = true;    
        }
    }

}
