
using UnityEngine;

public class BulletFollowPlayer : MonoBehaviour
{

    public Transform transformSpaceShip;
    public float speed = 0.5f;


    private void Update()
    {

        FollowPlayer();
    }

    void FollowPlayer()
    {
        transformSpaceShip = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        if (transform.position.x < transformSpaceShip.position.x)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
}
