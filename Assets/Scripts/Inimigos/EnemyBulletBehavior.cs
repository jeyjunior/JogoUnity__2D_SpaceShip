using UnityEngine;

public class EnemyBulletBehavior : MonoBehaviour
{
    public float speed;
    public int power;

    private void Awake()
    {
        

    }



    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EndBullets") Destroy(this.gameObject);
    }

}
