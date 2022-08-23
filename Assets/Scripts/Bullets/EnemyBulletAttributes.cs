using UnityEngine;

public class EnemyBulletAttributes : MonoBehaviour
{
    public int power;

    public int minPower, maxPower;
    GameController gameController;

    private void Start()
    {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        power = Mathf.RoundToInt(Random.Range(minPower,maxPower));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameController.TakeDamage(power);
            Destroy(this.gameObject);
        }
    }

}
