using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyBase : MonoBehaviour
{
    [Space(5)]
    [Header("Attributes Base")]
    public int valueMin;
    public int valueMax;

    [Space(5)]
    public int life; //Vida total
    public int power; //dano causado no player (quando colide ou atira)
    public int exp; //exp para dropar

    int level; //

    [Space(5)]
    [Header("GameObjects")]
    GameController gameController;
    EnemyController enemyController;
    public Slider lifeBar;


    //Formular dos atributos: Player Level 
    private void AttributesValue()
    {
        level = PlayerPrefs.GetInt("levelActual") + 1; 

        life = (Mathf.RoundToInt(Random.Range(valueMin, valueMax))) * level;
        power = (Mathf.RoundToInt(Random.Range(valueMin, valueMax))) * level;
        exp = (Mathf.RoundToInt(Random.Range(valueMin, valueMax))) * level;
    }

    private void Awake()
    {
        AttributesValue();
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        enemyController = GameObject.FindGameObjectWithTag("EnemyController").GetComponent<EnemyController>();

        lifeBar.maxValue = life;
        lifeBar.value = life;
    }
 
    #region Receiving and Dealing Damage
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Bullet":
                Destroy(collision.gameObject);
                var bulletStatus = collision.gameObject.GetComponent<BulletBehaviors>();

                life -= bulletStatus.power;
                lifeBar.value = life;

                if (life <= 0)
                {
                    DeathSystem(true);
                }
                break;

            case "Player":
                InvokeRepeating("hitPlayer", 0, 0.3f);
                break;

            case "EndEnemies":
                DeathSystem(false);
                break;
        }

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
        gameController.DmgReceive(power);
    }
    #endregion
    #region Death System
    //Death system and exp delivery to the player
    public void DeathSystem(bool drops)
    {
        if (drops)
        {
            gameController.GainingExperience(exp);
        }
        enemyController.enemysSpawned--;
        Destroy(this.gameObject);
    }
    #endregion
}
