using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBase : MonoBehaviour
{
    [Space(5)]
    [Header("Attributes Base")]
    public int life;
    public int poder;
    public float speedMove = 1;
    public int exp;
    public int level;

    [Space(5)]
    [Header("GameObjects")]
    public GameController gameController;
    public Slider lifeBar;

    private void AttributesValue(int valueMin, int valueMax)
    {
        //Attribute Value
        life = Mathf.RoundToInt(Random.Range(valueMin, valueMax)); ;
        poder = Mathf.RoundToInt(Random.Range(valueMin, valueMax));
        exp = Mathf.RoundToInt(Random.Range(valueMin, valueMax));

        speedMove = Random.Range(0.2f, 1);

        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.1f, 1), 0, 0);
    }




    private void Awake()
    {
        AttributesValue(1, 20);
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        lifeBar.maxValue = life;
        lifeBar.value = life;
    }

    
    private void Update()
    {
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
            lifeBar.value = life;
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
