using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //SpaceShip attributes
    public float speedBulletMove, spawnBulletDelay;

    //DMG Control
    public int power, crit;

    //Exp Control
    public int maxExp, currentExp, lvlPlayer, attributesPoints;

    //Life Control
    public int maxLife, currentLife;

    //SpaceShip
    public SpaceShip spaceShip;

    void Start()
    {
        spaceShip = GameObject.FindWithTag("Player").GetComponent<SpaceShip>();

        //Life
        currentLife = maxLife;
    }


    void Update()
    {
        StatsCheck();
        ExperienceControl();
        LifeBarControl();
    }

    #region Life Control
    void LifeBarControl()
    {
        spaceShip.lifeBar.value = currentLife;
    }
    #endregion

    #region Take DMG from enemies
    public void DmgReceive(int dmgReceive)
    {
        currentLife -= dmgReceive;
    }
    #endregion

    #region Experience and Level Control
    void ExperienceControl()
    {
        if(currentExp >= maxExp)
        {
            currentExp -= maxExp;
            maxExp *= 2;
            lvlPlayer++;
            attributesPoints += 3; //pontos para usuario distribuir como quiser nos atributos
            spaceShip.expBarSlider.maxValue = maxExp; //atualiza exp max
            
            maxLife += 10; //Ganha 10 de vida ganha lvl upado
            spaceShip.lifeBar.maxValue = maxLife;
        }

        spaceShip.expBarSlider.value = currentExp;
    }

    public void GainingExperience(int exp){
        currentExp += exp;
    }
    #endregion

    #region Saves ARRUMAR
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
