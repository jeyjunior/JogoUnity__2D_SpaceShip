using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMoveAngle : MonoBehaviour
{

    [Tooltip("true = Vector2.left / false = Vector2.right")]
    public bool leftMove;
    public float angleSpeed = 0.8f;



    private void Update()
    {
        DifDirection();
    }

    void DifDirection()
    {
        if (leftMove) transform.Translate(Vector2.left * angleSpeed * Time.deltaTime);
        else transform.Translate(Vector2.right * angleSpeed * Time.deltaTime);
    }


}
