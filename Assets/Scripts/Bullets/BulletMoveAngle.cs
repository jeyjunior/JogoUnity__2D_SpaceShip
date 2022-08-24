using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveAngle : MonoBehaviour
{

    //DifDirection
    public bool angleMove;

    [Tooltip("true = Vector2.left / false = Vector2.right")]
    public bool leftMove;
    public float angleSpeed = 0.3f;


    //Follow Player
    public bool follow;
    public Transform transformSpaceShip;

    private void Update()
    {
        if (angleMove) DifDirection();

        if (follow) FollowPlayer();
    }

    void DifDirection()
    {
        if (leftMove) transform.Translate(Vector2.left * angleSpeed * Time.deltaTime);
        else transform.Translate(Vector2.right * angleSpeed * Time.deltaTime);
    }

    void FollowPlayer()
    {
        transformSpaceShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        if(transform.position.x < transformSpaceShip.position.x)
        {
            transform.Translate(Vector2.right * 0.5f * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * 0.5f * Time.deltaTime);
        }
    }
}
