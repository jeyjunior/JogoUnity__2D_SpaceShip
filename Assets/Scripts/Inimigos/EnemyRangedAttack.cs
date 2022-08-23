using UnityEngine;
using UnityEngine.UI;

public class EnemyRangedAttack : MonoBehaviour
{

    public GameObject bullet;
    public GameObject spawnPoint;

    EnemyBase enemyBase;

    [Space(5)]
    [Header("Random Time To Spawn Bullets")]
    public int randomSpawn;

    [Space(5)]
    [Header("Index bullets and SpawnPoint")]
    public int chooseBulletIndex;
    public int chooseSpawnPointIndex;

    private void Start()
    {
        randomSpawn = 5;
        InvokeRepeating("SpawnBullet", 0, randomSpawn);
    }

    void SpawnBullet()
    {
        Instantiate(bullet, spawnPoint.transform.position, Quaternion.identity);
        randomSpawn = Mathf.RoundToInt(Random.Range(1, 5));
    }

}
