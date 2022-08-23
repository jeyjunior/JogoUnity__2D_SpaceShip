using UnityEngine;

public class BulletMove : MonoBehaviour
   
{
    public float speed;
    public int directionMove; //0 up - 1 down
    Vector2[] v2Direction = { Vector2.up, Vector2.down };


    void Start ()
    {
        if (this.gameObject.CompareTag("EnemyBullet"))
        {
            speed = 3; directionMove = 1;
        }
        Destroy(this.gameObject, 5);
    }

    void Update()
    {
        transform.Translate(v2Direction[directionMove] * speed * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EndBullets") Destroy(this.gameObject);
    }

}
