using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //SpaceShip attributes
    public float speedBulletMove = 2;
    public float spawnBulletDelay = 2;
    public int power = 3;
    public int crit = 50;

    public int maxLife = 300;


    //Exp Control
    public int maxExp = 30, currentExp = 0;
    public int lvlPlayer = 1;
    public Slider expBarSlider;
    public int attributesPoints = 0;

    void Start()
    {
        expBarSlider.maxValue = maxExp;
        expBarSlider.value = currentExp;
    }


    void Update()
    {
        StatsCheck();
        ExperienceControl();
    }



    #region Experience Control
    void ExperienceControl()
    {
        if(currentExp >= maxExp)
        {
            currentExp -= maxExp;
            maxExp *= 2;
            lvlPlayer++;
            attributesPoints += 3;
            expBarSlider.maxValue = maxExp;
        }

        expBarSlider.value = currentExp;
    }

    public void GainingExperience(int exp){
        currentExp += exp;
    }
    #endregion

    #region Saves
    public void SaveStats()
    {
        PlayerPrefs.SetFloat("speedBullet", speedBulletMove);
        PlayerPrefs.SetFloat("spawnBullet", spawnBulletDelay);
        PlayerPrefs.SetInt("power", power);
    }

    public void LoadStats()
    {
        speedBulletMove = PlayerPrefs.GetFloat("speedBullet");
        spawnBulletDelay = PlayerPrefs.GetFloat("spawnBullet");
        power = PlayerPrefs.GetInt("Power");
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
        if (power <= 0) {
            power = 3;
            PlayerPrefs.SetInt("power", 3);
        }
    }
    #endregion

}
