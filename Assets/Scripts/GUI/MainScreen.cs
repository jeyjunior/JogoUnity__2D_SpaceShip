using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainScreen : MonoBehaviour
{
    public GameObject mainPanel, statusPanel;

    //SpaceShip attributes
    public float speedBulletMove = 5, spawnBulletDelay = 2.1f;
    int spawnBulletDelayCountUpgrade = 0;

    //DMG Control
    public int power = 3, crit = 0;

    //Exp Control
    public int maxExp = 30, currentExp = 0, lvlPlayer = 1, attributesPoints = 10;

    //Life Control
    public int maxLife = 300;

    //Attributes Components
    public Text txtQtdAttributesPoints, txtStatsPower, txtStatsCritical, txtStatsLife, txtStatsSpawnBullet, txtStatsSpeedBullet;
    public Slider sliderPower, sliderCritical, sliderLife, sliderSpawnBullet, sliderSpeedBullet;

    private void Start()
    {
        PanelsControl(true, false);
        SlidersMaxValue();
        FirstTime();   
    }

    private void Update()
    {
        UpgradeAttributes();
    }


    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    #region Upgrade Attributes
    void UpgradeAttributes()
    {
        txtQtdAttributesPoints.text = attributesPoints.ToString();

        //Text attributes
        txtStatsPower.text = power.ToString();
        txtStatsCritical.text = crit.ToString();
        txtStatsLife.text = maxLife.ToString();
        txtStatsSpawnBullet.text = spawnBulletDelay.ToString("F2");
        txtStatsSpeedBullet.text = speedBulletMove.ToString("F2"); //CONFIRMAR VALORES

        //Slider attributes
        sliderPower.value = power;
        sliderCritical.value = crit;
        sliderLife.value = maxLife;
        sliderSpawnBullet.value = spawnBulletDelayCountUpgrade;
        sliderSpeedBullet.value = speedBulletMove; //CONFIRMAR VALORES
    }

    void SlidersMaxValue()
    {
        sliderPower.maxValue = 300;
        sliderCritical.maxValue = 100;
        sliderLife.maxValue = 3000;
        sliderSpawnBullet.maxValue = 100; sliderSpawnBullet.minValue = 0;
        sliderSpeedBullet.maxValue = 25;
    }
    
    //Power Upgrade
    //Acada 1pt atributo add 3pt de power
    public void UpgradePower(bool value)
    {
        if(value && attributesPoints > 0 && power <= 300)
        {
            power += 3;
            attributesPoints--;
        }

        if (!value && power >= 6)
        {
            power -= 3;
            attributesPoints++;
        }

    }

    //Critical Upgrade
    //Acada 1pt atributo add 1pt de critical
    public void UpgradeCritical(bool value)
    {
        if (value && attributesPoints > 0 && crit < 100)
        {
            crit += 1;
            attributesPoints--;
        }

        if (!value && crit >= 1)
        {
            crit -= 1;
            attributesPoints++;
        }
    }

    //Life Upgrade
    //Acada 1pt atributo add 27pt de vida
    public void UpgradeLife(bool value)
    {
        if (value && attributesPoints > 0 && maxLife <= 2973)
        {
            maxLife += 27;
            attributesPoints--;
        }

        if (!value && maxLife >= 327)
        {
            maxLife -= 27;
            attributesPoints++;
        }
    }


    //SpawnBulletDelay Upgrade
    //Acada 5pts atributo diminui 0.1f de SpawnBulletDelay (1pt = 0.02f)
    public void UpgradeSpawnBullet(bool value)
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

    //SpeedBullet Upgrade
    //Acada 1pt atributo add 0,2pt de Speed
    public void UpgradeSpeedBullet(bool value)
    {
        if (value && attributesPoints > 0 && speedBulletMove <= 25)
        {
            speedBulletMove += 0.2f;
            attributesPoints--;
        }

        if (!value && speedBulletMove >= 5.02f)
        {
            speedBulletMove -= 0.2f;
            attributesPoints++;
        }
    }

    #endregion

    #region Save and Load Data
    public void LoadData()
    {
        speedBulletMove = PlayerPrefs.GetFloat("speedBulletMove");
        spawnBulletDelay = PlayerPrefs.GetFloat("spawnBulletDelay");

        power = PlayerPrefs.GetInt("power");
        crit = PlayerPrefs.GetInt("crit");

        maxExp = PlayerPrefs.GetInt("maxExp");
        currentExp = PlayerPrefs.GetInt("currentExp");
        lvlPlayer = PlayerPrefs.GetInt("lvlPlayer");
        attributesPoints = PlayerPrefs.GetInt("attributesPoints");

        maxLife = PlayerPrefs.GetInt("maxLife");
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

    public void FirstTime() {
        if (PlayerPrefs.HasKey("power") || PlayerPrefs.HasKey("maxLife"))
        {
            LoadData();
        };
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

    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
    }
    #endregion
}
