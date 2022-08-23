using UnityEngine;

public class SimpleAliensSpawn : MonoBehaviour
{

    [Space(5)]
    public GameObject[] enemys = new GameObject[1];

    [Space(5)]
    [Header("Control Spawn SimpleEnemies LEFT")]
    //Left
    float[] xPositionLeft = { -3, -3.7f, -4.4f, -5.1f, -5.8f, -6.5f };
    public int maxSpawnSimpleEnemyLeft = 6;
    public int spawnedSimpleEnemyLeft;

    [Space(5)]
    [Header("Control Spawn SimpleEnemies RIGHT")]
    //Left
    float[] xPositionRight = { 3, 3.7f, 4.4f, 5.1f, 5.8f, 6.5f };
    public int maxSpawnSimpleEnemyRight = 6;
    public int spawnedSimpleEnemyRight;

    void Start()
    {
        maxSpawnSimpleEnemyLeft = xPositionLeft.Length;
        spawnedSimpleEnemyLeft = 0;

        maxSpawnSimpleEnemyRight = xPositionRight.Length;
        spawnedSimpleEnemyRight = 0;

        SpawnSimpleEnemyLeft();
        SpawnSimpleEnemyRight();

        if (spawnedSimpleEnemyRight >= maxSpawnSimpleEnemyRight && spawnedSimpleEnemyLeft >= maxSpawnSimpleEnemyLeft)
        {
            GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>().enemysSpawned = (spawnedSimpleEnemyLeft + spawnedSimpleEnemyRight);
            Destroy(this.gameObject);
        }
    }

    void SpawnSimpleEnemyLeft()
    {
        //LEFT
        if (spawnedSimpleEnemyLeft < maxSpawnSimpleEnemyLeft)
        {
            for (int i = 0; i < maxSpawnSimpleEnemyLeft; i++)
            {
                spawnedSimpleEnemyLeft++;
                Vector2 position = new Vector2(xPositionLeft[i], 3);
                Instantiate(enemys[(int)Random.Range(0,2)], position, Quaternion.identity);
            }
        }
    }

    void SpawnSimpleEnemyRight()
    {
        if (spawnedSimpleEnemyRight < maxSpawnSimpleEnemyRight)
        {
            for (int i = 0; i < maxSpawnSimpleEnemyRight; i++)
            {
                spawnedSimpleEnemyRight++;
                Vector2 position = new Vector2(xPositionRight[i], 4);
                Instantiate(enemys[(int)Random.Range(0, 2)], position, Quaternion.identity);
            }
        }
    }
}
