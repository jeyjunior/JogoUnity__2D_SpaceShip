using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    float speedMove = 1;


    private void Update()
    {
        Move();
    }
    public void Move()
    {
       transform.Translate(Vector2.down * speedMove * Time.deltaTime);
    }
}
