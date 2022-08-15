using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    //SpaceShip só tem spawns e moves todo o controle de atributos é trabalhado diretamente no GC

    //GC
    public GameController gameController;

    //SpaceShip moveControl
    public Slider valueMove;
    public Vector2 moveP;


    //Bullet Control
    public GameObject bullet;
    public Transform spawnPoint;

    //life and Exp Bar
    public Slider lifeBar;
    public Slider expBarSlider;


    void Awake()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        //Move Control
        valueMove = GameObject.Find("MoveSlider").GetComponent<Slider>();

        //LifeBar
        lifeBar.maxValue = gameController.maxLife;
        lifeBar.value = gameController.maxLife;

        //ExpBar
        expBarSlider.maxValue = gameController.maxExp;
        expBarSlider.value = gameController.currentExp;

        //Bullets Control and Spawn bullets 
        InvokeRepeating("SpawnBullets", 0, gameController.spawnBulletDelay);
    }

    void Update()
    {
        SpaceShipMove();
    }

    #region SpaceShipMove
    public void SpaceShipMove()
    {
        moveP = new Vector2(valueMove.value * 2.3f, -3);
        transform.position = moveP;
    }
    #endregion

    #region SpawnBullets
    void SpawnBullets()
    {
        spawnPoint = GameObject.Find("spawnShoot").GetComponent<Transform>();
        Instantiate(bullet, spawnPoint.position, Quaternion.identity);
    }
    #endregion

}
