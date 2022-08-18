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
    GameController gameController;
    EnemyController enemyController;
    public Slider lifeBar;

    private void AttributesValue(int valueMin, int valueMax)
    {
        //Attribute Value
        life = Mathf.RoundToInt(Random.Range(valueMin, valueMax)); ;
        poder = Mathf.RoundToInt(Random.Range(valueMin, valueMax));
        exp = Mathf.RoundToInt(Random.Range(valueMin, valueMax));

        SpeedMove(0.1f, 0.8f);

        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0.1f, 1), 0, 0);
    }


    public void SpeedMove(float min, float max) => speedMove = Random.Range(min, max);


    private void Awake()
    {
        AttributesValue(1, 20);
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

                gameController.SaveData();
                break;

            case "Player":
                InvokeRepeating("hitPlayer", 0, 0.3f);
                break;

            case "EndEnemies":
                DeathSystem(false);
                break;
            case "SwitchDirToDown":
                this.gameObject.GetComponent<EnemyMove>().DirControl(false);
                break;
            case "SwitchDirToUp":
                this.gameObject.GetComponent<EnemyMove>().DirControl(true);
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
        gameController.DmgReceive(poder);
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
        enemyController.spawned--;
        Destroy(this.gameObject);
    }
    #endregion
}
