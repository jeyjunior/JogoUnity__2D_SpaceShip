
using UnityEngine;

public class EnemyMove : MonoBehaviour
{

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

    void Update()
    {
        Move();
        //DirectionControl();
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


}
