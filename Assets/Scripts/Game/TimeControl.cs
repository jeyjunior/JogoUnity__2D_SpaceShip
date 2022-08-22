using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeControl : MonoBehaviour
{
    public Text txtTimeCount;

    [SerializeField]
    int timeCount;
    public int timeBossSpawn = 20;
    public bool bigBossFight;

    //Objetivos concluido
    public bool bigBossDead;


    private void Start()
    {
        timeCount = 0;
        StartCoroutine(ControllingSpawnTime());
    }

    private void Update()
    {
        txtTimeCount.text = timeCount.ToString();
    }

    IEnumerator ControllingSpawnTime() {
        
        if (!bigBossFight)
        {
            for (int i = timeCount; i < timeBossSpawn; i++)
            {
                yield return new WaitForSecondsRealtime(1);
                timeCount++;

                if (timeCount == timeBossSpawn)
                {
                    bigBossFight = true;
                    timeCount = 0;
                    StopCoroutine(ControllingSpawnTime());
                }
            }
        }
    }

}
