using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : MonoBehaviour
{
    //GC
    public GameController gameController;

    //SpaceShip moveControl
    public Slider valueMove;
    public Vector2 moveP;

    //SpaceShip lifeStats
    public Slider lifeBar;
    public int maxLife_, remainingLife;

    //Bullet Control
    public GameObject bullet;
    public Transform spawnPoint;
    float spawnBulletDelay_;



    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();

        //Move Control
        valueMove = GameObject.Find("MoveSlider").GetComponent<Slider>();

        //LifeBar Control
        maxLife_ = gameController.maxLife;
        remainingLife = maxLife_;
        lifeBar = GetComponentInChildren<Slider>();
        lifeBar.maxValue = maxLife_;


        //Bullets Control and Spawn bullets 
        spawnBulletDelay_ = gameController.spawnBulletDelay;
        InvokeRepeating("SpawnBullets", 0, spawnBulletDelay_);
    }

    void Update()
    {
        SpaceShipMove(); 
        LifeBarControl();
    }

    void LifeBarControl() {
        lifeBar.value = remainingLife;
    }

    public void DmgReceive(int dmgReceive)
    {
        remainingLife -= dmgReceive;
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke("DmgReceive");
    }

    #region SpaceShipMove
    public void SpaceShipMove()
    {
        moveP = new Vector2(valueMove.value * 3, -3);
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
