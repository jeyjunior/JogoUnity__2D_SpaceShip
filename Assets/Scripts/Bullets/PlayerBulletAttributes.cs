using UnityEngine;

public class PlayerBulletAttributes : MonoBehaviour
{
    int crit;
    public int power;

    private void Start()
    {
        GameController gameController = GameObject.Find("GameController").GetComponent<GameController>();

        //Move 0 = Vector2.up
        this.gameObject.GetComponent<BulletMove>().directionMove = 0;
        this.gameObject.GetComponent<BulletMove>().speed = gameController.speedBulletMove;

        crit = Random.Range(1, 100);
        if (crit <= gameController.crit) power = gameController.power * 2;
        else power = gameController.power;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag == "Enemies" || tag == "Boss")
        {
            collision.GetComponent<EnemyBase>().TakeDamage(power);
            Destroy(this.gameObject);
        }
    }
}
