using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviors : MonoBehaviour
   
{
    float speed;
    int crit;
    public bool critic; //Apenas para controlar a cor do TxtDmg caso seja true
    public int power;
    
    GameController gc;

    // Start is called before the first frame update
    void Awake()
    {
        gc = GameObject.Find("GameController").GetComponent<GameController>();

        //Calculo de critico
        speed = gc.speedBulletMove;
        crit = Random.Range(1, 100);

        if (crit <= gc.crit) {  
            power = gc.power * 2;
            critic = true;
        }
        else { 
            power = gc.power;
            critic = false;
        }
        Destroy(this.gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "EndBullets") Destroy(this.gameObject);
    }

}
