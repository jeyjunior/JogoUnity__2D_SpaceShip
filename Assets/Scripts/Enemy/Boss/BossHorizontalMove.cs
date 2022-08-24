using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHorizontalMove : MonoBehaviour
{
    public bool loopMovingX = true;
    public float delta = 10;
    float speed = 1f;

    BossStartMove bossStartMove;

    private void Start()
    {
        bossStartMove = GetComponent<BossStartMove>();
    }
    void Update()
    {
        if (!bossStartMove.startMoving) HorizontalMoving();
    }

    void HorizontalMoving()
    {
        //transform.localPosition = new Vector2(SineMoving(), transform.position.y);
        if (loopMovingX)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
            if (transform.position.x >= 1.5f) loopMovingX = false;
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
            if (transform.position.x < -1.5f) loopMovingX = true;
        }
    }
}
