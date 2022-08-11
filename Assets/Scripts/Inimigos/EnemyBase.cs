using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public int maxLife;
    float speedMove = 1;

    private void Awake()
    {
        maxLife = 10;
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
            maxLife -= collision.gameObject.GetComponent<BulletBehaviors>().power;
        }

        if (collision.gameObject.name == "EndEnemies") Destroy(this.gameObject);
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
