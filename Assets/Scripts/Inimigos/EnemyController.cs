using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Space(5)]
    [Header("Control Spawns")]
    [SerializeField]
    int maxSpawn = 20;
    public int spawned=0;


    [Space(5)]
    [Header("Enemys list")]
    public GameObject[] enemy = new GameObject[1];

    [Space(5)]
    [Header("GameObjects")]
    public GameController gameController;

    public float timeCount = 0;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    private void Update()
    {
        SlimeSpawn();
    }

    private void SlimeSpawn()
    {
        if(spawned < maxSpawn)
        {
            
            Vector2 position = new Vector2(Random.Range(-2f, 2f), 6);
            Instantiate(enemy[0], position, Quaternion.identity);
            //spawnedQuantity = Mathf.RoundToInt(Random.Range(3, 10));
            spawned++;
            //await Task.Delay(1000);
        }

    }

}
