
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    #region oldmove
    /*
    //move to Down  = false; moveto up = true

    bool direction = false;
    [SerializeField]
    int amountSwitchDir = 5;
    [SerializeField]
    int dirSwitchCount = 0;




    private void Start()
    {
        amountSwitchDir = (int)Random.Range(5, 10);
    }

    private void FixedUpdate()
    {
        Move();
    }

    public void Move()
    {
        if(!direction)
        {
            transform.Translate(Vector2.down * this.gameObject.GetComponent<EnemyBase>().speedMove * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.up * this.gameObject.GetComponent<EnemyBase>().speedMove * Time.deltaTime);
        }
    }



    public void DirControl(bool value)
    {
        if(dirSwitchCount < amountSwitchDir)
        {
            direction = value;
            dirSwitchCount++;
        }
    }
    */
    #endregion

    float speed = 1;
    public bool loopMovingY = false;
    public bool startMoving = true;
    public float initialX;
    bool dirX; //false = right

    int maxCountLoop = 0, countLoop = 0;
    Vector2[] directionMove = { Vector2.up, Vector2.down, Vector2.right, Vector2.left}; 

    private void Start()
    {
        //Contagem dos loops na vertical antes de morrer
        maxCountLoop = (int)Random.Range(5, 8);
        countLoop = 0;

        startMoving = true;
        initialX = transform.position.x;
        if (initialX > 0) dirX = true;

    }

    private void FixedUpdate()
    {
        if(startMoving) InitialMovement();
        else
        {
            if(countLoop < maxCountLoop) VerticalMoving();
            else DeathMoving();
        }

    }


    void InitialMovement() {

        if (dirX)
        {
            DirectionMovement(3);
            if (transform.position.x <= (initialX - 4.7f)) startMoving = false;
        }
        else
        {
            DirectionMovement(2);
            if (transform.position.x >= (initialX + 4.7f)) startMoving = false;
        }
    }



    void VerticalMoving(){

        if (loopMovingY) {
            DirectionMovement(1);
            if (transform.position.y < 0)
            {
                loopMovingY = false;
                countLoop++;
            }
        }
        else
        {
            DirectionMovement(0);
            if (transform.position.y > 3) loopMovingY = true;
        }
    }

    void DeathMoving()
    {
        DirectionMovement(1);
    }

    void DirectionMovement(int value)
    {
        //0 - Vector2.up, 1 - Vector2.down, 2 - Vector2.right, 3 - Vector2.left
        transform.Translate(directionMove[value] * speed * Time.deltaTime);
    }

}
