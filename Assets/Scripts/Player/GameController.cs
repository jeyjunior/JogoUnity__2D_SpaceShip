using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    //SpaceShip atributos
    public float speedBulletMove = 2;
    public float spawnBulletDelay = 2;


    void Start()
    {
        LoadStats();
    }


    void Update()
    {
        StatsCheck();
    }


    public void SaveStats()
    {
        PlayerPrefs.SetFloat("speedBullet", speedBulletMove);
        PlayerPrefs.SetFloat("spawnBullet", spawnBulletDelay);
    }

    public void LoadStats()
    {
        speedBulletMove = PlayerPrefs.GetFloat("speedBullet");
        spawnBulletDelay = PlayerPrefs.GetFloat("spawnBullet");
    }

    public void StatsCheck()
    {
        if (speedBulletMove <= 0){ 
            speedBulletMove = 1;
            PlayerPrefs.SetFloat("speedBullet", 1);
        }
        if (spawnBulletDelay <= 0) { 
            spawnBulletDelay = 2;
            PlayerPrefs.SetFloat("spawnBullet", 2);
        }
    }

}
