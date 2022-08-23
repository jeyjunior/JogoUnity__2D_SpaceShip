using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeControl : MonoBehaviour
{
    [SerializeField]
    int timeCount;
    public int timeBossSpawn = 5;
    public bool bigBossFight;

    //Objetivos concluido
    public bool bigBossDead;


    private void Start()
    {
        timeCount = 0;
        StartCoroutine(ControllingSpawnTime());
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

                    this.gameObject.GetComponent<EnemyController>().bigBossFight_ = true;
                    timeCount = 0;
                    StopCoroutine(ControllingSpawnTime());
                }
            }
        }
    }
}
