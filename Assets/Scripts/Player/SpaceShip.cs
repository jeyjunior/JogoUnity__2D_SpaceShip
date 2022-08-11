using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    //GC
    public GameController gameController;
    float spawnBulletDelay_;

    //SpaceShip
    public Slider valueMove;
    public Vector2 moveP;

    //Bullet
    public GameObject bullet;
    public Transform spawnPoint;


    void Start()
    {
        valueMove = GameObject.Find("MoveSlider").GetComponent<Slider>();
        spawnBulletDelay_ = GameObject.FindWithTag("GameController").GetComponent<GameController>().spawnBulletDelay;

        InvokeRepeating("SpawnBullets", 0, spawnBulletDelay_);
    }

    void Update()
    {
        SpaceShipMove();
    }

    public void SpaceShipMove()
    {
        moveP = new Vector2(valueMove.value * 3, -3);
        transform.position = moveP;
    }

    void SpawnBullets()
    {
        spawnPoint = GameObject.Find("spawnShoot").GetComponent<Transform>();
        Instantiate(bullet, spawnPoint.position, Quaternion.identity);
    }


}
