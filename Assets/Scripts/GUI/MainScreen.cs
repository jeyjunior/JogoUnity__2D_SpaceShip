using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainScreen : MonoBehaviour
{
    public GameObject mainPanel, statusPanel;

    //FirstTime
    int firstTime = 0;

    //SpaceShip attributes
    public float speedBulletMove, spawnBulletDelay;
    int spawnBulletDelayCountUpgrade = 0;

    //DMG Control
    public int power, crit;

    //Exp Control
    public int maxExp, currentExp, lvlPlayer, attributesPoints;

    //Life Control
    public int maxLife;

    //Attributes Components
    public Text txtQtdAttributesPoints, txtPower, txtSpawnBulletDelay;
    public Slider sliderPower, sliderSpawnBulletDelay;

    private void Start()
    {
        PanelsControl(true, false);
        FirstTime();
        LoadData();
        SlidersMaxValue();
    }

    private void Update()
    {
        UpgradeAttributes();
    }

    void UpgradeAttributes()
    {
        txtQtdAttributesPoints.text = attributesPoints.ToString();
        txtPower.text = power.ToString();
        txtSpawnBulletDelay.text = spawnBulletDelay.ToString("F2");

        sliderPower.value = power;
        sliderSpawnBulletDelay.value = spawnBulletDelayCountUpgrade;
    }

    void SlidersMaxValue()
    {
        sliderPower.maxValue = 300;
        sliderSpawnBulletDelay.maxValue = 100; sliderSpawnBulletDelay.minValue = 0;
    }
    
    //Power Upgrade
    //Acada 1pt atributo add 3pt de power
    public void PowerUpgrade(bool value)
    {
        if(value && attributesPoints > 0 && power < 300)
        {
            power += 3;
            attributesPoints--;
        }

        if (!value && power >= 3)
        {
            power -= 3;
            attributesPoints++;
        }

    }

    //SpawnBulletDelay Upgrade
    //Acada 5pts atributo diminui 0.1f de SpawnBulletDelay (1pt = 0.02f)
    public void SpawnBulletDelayUpgrade(bool value)
    {
        if (value && attributesPoints > 0 && spawnBulletDelay > 0.12f)
        {
            spawnBulletDelay -= 0.02f;
            attributesPoints--;
            spawnBulletDelayCountUpgrade++;
        }

        if (!value && spawnBulletDelay <= 2.08f)
        {
            spawnBulletDelay += 0.02f;
            attributesPoints++;
            spawnBulletDelayCountUpgrade--;
        }

    }








    void FirstTime()
    {
        if (firstTime == 0)
        {
            speedBulletMove = 2; spawnBulletDelay = 2.1f;
            power = 3; crit = 0;
            maxExp = 30; currentExp = 0; lvlPlayer = 1; attributesPoints = 10;
            maxLife = 300;
            firstTime = 1;
            PlayerPrefs.SetInt("firstTime", firstTime);
        }
    }

    #region Save and Load Data
    public void LoadData()
    {
        //Load maxExp; currentExp; lvlPlayer; attributesPoints, firstTime;
        PlayerPrefs.GetInt("firstTime");

        PlayerPrefs.GetInt("maxExp");
        PlayerPrefs.GetInt("currentExp");
        PlayerPrefs.GetInt("lvlPlayer");
        PlayerPrefs.GetInt("attributesPoints");
    }

    public void SaveData()
    {
        PlayerPrefs.SetFloat("speedBulletMove", speedBulletMove);
        PlayerPrefs.SetFloat("spawnBulletDelay", spawnBulletDelay);

        PlayerPrefs.SetInt("power", power);
        PlayerPrefs.SetInt("crit", crit);

        PlayerPrefs.SetInt("maxExp", maxExp);
        PlayerPrefs.SetInt("currentExp", currentExp);
        PlayerPrefs.SetInt("lvlPlayer", lvlPlayer);
        PlayerPrefs.SetInt("attributesPoints", attributesPoints);

        PlayerPrefs.SetInt("maxLife", maxLife);
    }
    #endregion

    #region Btns MainPanel and StatusPanel
    public void MainPanel()
    {
        PanelsControl(true, false);
    }

    public void StatusPanel()
    {
        PanelsControl(false, true);
    }


    public void PanelsControl(bool valueA, bool valueB)
    {
        mainPanel.SetActive(valueA);
        statusPanel.SetActive(valueB);
    }

    #endregion
}
