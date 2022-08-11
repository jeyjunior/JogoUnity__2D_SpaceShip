using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public GameObject[] enemy = new GameObject[1];

    public float delaySpawn = 1;
    public int spawnedQuantity = 10;


    void Start()
    {
        InvokeRepeating("SlimeSpawn", 0, delaySpawn);
    }

    private void SlimeSpawn()
    {
        for(int i = 0; i < spawnedQuantity; i++)
        {
            var position = new Vector2(Random.Range(-2.5f, 2.5f), Random.Range(5, 6));
            Instantiate(enemy[0], position, Quaternion.identity);
        }
    }

}
