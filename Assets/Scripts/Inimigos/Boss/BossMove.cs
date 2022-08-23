using System.Collections;
using UnityEngine;

public class BossMove : MonoBehaviour
{

    bool startMoving;

    public bool loopMovingX = true;
    public float delta = 10;
    float speed = 1f;

    void Start()
    {
        startMoving = true;
        //StartCoroutine(StopMoving());

    }

    private void FixedUpdate()
    {


        if (!startMoving) HorizontalMoving();
        else StartMoving();
        
    }

    #region StartMoving
    void StartMoving()
    {
        if (startMoving)
        {
            transform.Translate(Vector2.down * 1 * Time.deltaTime);
            if (transform.position.y < 3)
            {
                startMoving = false;
                loopMovingX = true;

            }
        }
    }
    /*
    IEnumerator StopMoving()
    {
        yield return new WaitForSecondsRealtime(3);
        startMoving = false;
        xMoving = true;
    }
    */
    #endregion

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

    /*
    float SineMoving() {

        return delta * Mathf.Sin(speed * Time.time);
    }
    */
}

