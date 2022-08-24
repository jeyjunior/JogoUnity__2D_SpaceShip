using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartMove : MonoBehaviour
{
    public bool startMoving;
    public float speed = 1;

    private void Start()
    {
        startMoving = true;
    }

    private void FixedUpdate()
    {
        if (startMoving) InitialMoviment();
    }

    void InitialMoviment()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
        if (transform.position.y < 3) startMoving = false;
    }
}
