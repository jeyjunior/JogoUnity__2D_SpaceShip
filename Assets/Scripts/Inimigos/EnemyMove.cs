using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

    void Update()
    {
        Move();
    }

    public void Move()
    {
        transform.Translate(Vector2.down * this.gameObject.GetComponent<EnemyBase>().speedMove * Time.deltaTime);
    }
}
