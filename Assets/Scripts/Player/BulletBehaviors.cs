using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviors : MonoBehaviour
   
{
    private float speed;


    // Start is called before the first frame update
    void Start()
    {
        speed = GameObject.Find("GameController").GetComponent<GameController>().speedBulletMove;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "End") Destroy(this.gameObject);
    }

}
