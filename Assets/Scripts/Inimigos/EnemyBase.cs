using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int maxLife;
    public int poder;
    float speedMove = 1;

    public GameObject txtDmgReceive;


    private void Awake()
    {
        maxLife = 10;
        poder = Mathf.RoundToInt(Random.Range(1, 10));
    }

    
    private void Update()
    {
        Move();
        LifeControl();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            var bulletStatus = collision.gameObject.GetComponent<BulletBehaviors>();


            maxLife -= bulletStatus.power;

            //Spawn txt
            Instantiate(txtDmgReceive, transform.position, Quaternion.identity);
            txtDmgReceive.gameObject.GetComponent<TxtDmgReceive>().dmgValue = bulletStatus.power;
            txtDmgReceive.gameObject.GetComponent<TxtDmgReceive>().crit = bulletStatus.critic;
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("hitPlayer", 0, 0.3f);
        }

        if (collision.gameObject.name == "EndEnemies") Destroy(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CancelInvoke("hitPlayer");
    }

    void hitPlayer()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpaceShip>().DmgReceive(poder);
    }

    void LifeControl()
    {
        if (maxLife <= 0) Destroy(this.gameObject);
    }

    #region Movimentação
    public void Move()
    {
       transform.Translate(Vector2.down * speedMove * Time.deltaTime);
    }
    #endregion
}
