using UnityEngine;

public class BulletMove : MonoBehaviour
   
{
    public float speed;

    [Tooltip("0 = Vector2.up / 1 = Vector2.down")]
    public int directionMove; 

    Vector2[] v2Direction = { Vector2.up, Vector2.down };


    void Start ()
    {
        directionMove = 1;


        if (this.gameObject.CompareTag("Asteroid")) speed = 1;
        else { 
            speed = 3; 
            Destroy(this.gameObject, 5);
        }

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
