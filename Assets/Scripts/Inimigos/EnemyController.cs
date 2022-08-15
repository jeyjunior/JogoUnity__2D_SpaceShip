using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Space(5)]
    [Header("Control Spawns")]
    float delaySpawn = 5;
    int spawnedQuantity = 2;



    [Space(5)]
    [Header("Enemys list")]
    public GameObject[] enemy = new GameObject[1];

    [Space(5)]
    [Header("GameObjects")]
    public GameController gameController;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        InvokeRepeating("SlimeSpawn", 0, delaySpawn);
    }

    private void Update()
    {

    }

    private void SlimeSpawn()
    {
        for(int i = 0; i < spawnedQuantity; i++)
        {
            var position = new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(5, 6));
            Instantiate(enemy[0], position, Quaternion.identity);
            spawnedQuantity = Mathf.RoundToInt(Random.Range(3, 10));
        }
    }

}
