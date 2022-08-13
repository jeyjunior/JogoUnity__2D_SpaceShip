using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int life;
    public int poder;
    float speedMove = 1;
    public int exp;

    public GameObject txtDmgReceive;
    public GameController gameController;

    private void Awake()
    {
        life = 10;
        poder = Mathf.RoundToInt(Random.Range(1, 10));
        exp = Mathf.RoundToInt(Random.Range(5, 10));

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
    }

    
    private void Update()
    {
        Move();
        DeathSystem();
    }

    
    #region Receiving and Dealing Damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            var bulletStatus = collision.gameObject.GetComponent<BulletBehaviors>();


            life -= bulletStatus.power;

            //Spawn txt
            Instantiate(txtDmgReceive, transform.position, Quaternion.identity);
            txtDmgReceive.gameObject.GetComponent<TxtDmgReceive>().dmgValue = bulletStatus.power;
            txtDmgReceive.gameObject.GetComponent<TxtDmgReceive>().crit = bulletStatus.critic;
        }

        //Sempre que colidir com o player, aciona hitPlayer
        if (collision.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("hitPlayer", 0, 0.3f);
        }

        if (collision.gameObject.name == "EndEnemies") Destroy(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        //Cancela o dano por tempo que o player irá sofrer
        CancelInvoke("hitPlayer");
    }


    void hitPlayer()
    {
        //hitPlayer aciona metodo DMGReceive que controla a perda de vida do player
        //Controle é realizado no GameController
        gameController.DmgReceive(poder);
    }
    #endregion


    //Separar movimentação em outro script
    #region Movimentação
    //Sistema de movimentação dos inimigos
    public void Move()
    {
       transform.Translate(Vector2.down * speedMove * Time.deltaTime);
    }
    #endregion

    //Sistema de morte add no triggerEnd2D
    #region Death System
    //Death system and exp delivery to the player
    public void DeathSystem()
    {
        if (life <= 0)
        {
            gameController.GainingExperience(exp);
            Destroy(this.gameObject);
        }
    }
    #endregion
}
