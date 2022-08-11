using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TxtDmgReceive : MonoBehaviour
{
    public bool crit;
    public int dmgValue;

    TextMesh textMesh;
    

    void Start()
    {
        textMesh = GetComponent<TextMesh>();

        if (crit)
        {
           textMesh.color = new Color(1, 0, 0);
           textMesh.text = dmgValue.ToString();
        }
        else
        {
            textMesh.text = dmgValue.ToString();
        }

        Destroy(this.gameObject, 1.2f);

    }

    
    void Update()
    {
        transform.Translate(Vector2.up * Random.Range(0.5f,2) * Time.deltaTime);
    }
}
