using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class MainScreen : MonoBehaviour
{
    #region SpaceShip var attributes and objects
    [Space(5)]
    [Header("Panels GameObject")]
    public GameObject mainPanel;
    public GameObject statusPanel;
    public GameObject wavesPanel;

    [Space(5)]
    [Header("Attributes Base")]
    public int power = 3;
    public int crit = 0;
    public int maxLife = 300;
    public float spawnBulletDelay = 2.1f;
    public float speedBulletMove = 5;

    [Space(5)]
    [Header("Exp and Lvl Control")]
    public int maxExp = 30;
    public int currentExp = 0;
    public int lvlPlayer = 1;
    public int attributesPoints = 10;

    [Space(5)]
    [Header("Upgrades Amount")]
    public int powerUpAmount = 0;
    public int critUpAmount = 0;
    public int lifeUpAmount = 0;
    public int spawnUpAmount = 0;
    public int speedUpAmount = 0;

    [Space(5)]
    [Header("Attributes Componentes (Text)")]
    public Text txtQtdAttributesPoints;
    public Text txtStatsPower;
    public Text txtStatsCritical;
    public Text txtStatsLife;
    public Text txtStatsSpawnBullet;
    public Text txtStatsSpeedBullet;

    [Space(5)]
    [Header("Attributes Componentes (Sliders)")]
    public Slider sliderPower;
    public Slider sliderCritical;
    public Slider sliderLife;
    public Slider sliderSpawnBullet;
    public Slider sliderSpeedBullet;


    [Space(5)]
    [Header("Level Control")]
    public Button[] btnLevel;

    public int unlockLevels = 1;
    public int levelActual = 0;

    #endregion

    private void Start()
    {
        PanelsControl(true, false, false);
        SlidersMaxValue();
        FirstTime();
        LevelUnlock();

    }

    private void Update()
    {
        UpgradeAttributes();
    }

    #region Initial Attributes
    ///<summary> true = firstTime, false = NOT first time</summary>
    public void InitialAttributes(bool firstTime)
    {
        
        if (firstTime)
        {
            power = 3;
            crit = 0;
            maxLife = 300;
            spawnBulletDelay = 2.1f;
            speedBulletMove = 5;

            maxExp = 30;
            currentExp = 0;
            lvlPlayer = 1;
            attributesPoints = 10;

            powerUpAmount = 0; 
            critUpAmount = 0; 
            lifeUpAmount = 0; 
            spawnUpAmount = 0; 
            speedUpAmount = 0;
        }
        else
        {
            if (power > 3)
            {
                for (int i = powerUpAmount; i > 0; i--)
                {
                    attributesPoints++;
                    power -= 3;
                    powerUpAmount--;
                }
            }

            if (crit > 0)
            {
                for (int i = critUpAmount; i > 0; i--)
                {
                    attributesPoints++;
                    crit -= 1;
                    critUpAmount--;
                }
            }

            if (maxLife > 300)
            {
                for (int i = lifeUpAmount; i > 0; i--)
                {
                    attributesPoints++;
                    maxLife -= 27;
                    lifeUpAmount--;
                }
            }

            if (spawnBulletDelay < 2.1f)
            {
                for (int i = spawnUpAmount; i > 0; i--)
                {
                    attributesPoints++;
                    spawnBulletDelay += 0.02f;
                    spawnUpAmount--;
                }
            }

            if (speedBulletMove > 5)
            {
                for (int i = speedUpAmount; i > 0; i--)
                {
                    attributesPoints++;
                    speedBulletMove -= 0.2f;
                    speedUpAmount--;
                }
            }

            SaveData();
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

        //----------Attributes Upgrade Count
        powerUpAmount = PlayerPrefs.GetInt("powerUpAmount");
        critUpAmount = PlayerPrefs.GetInt("critUpAmount");
        lifeUpAmount = PlayerPrefs.GetInt("lifeUpAmount");
        spawnUpAmount = PlayerPrefs.GetInt("spawnUpAmount");
        speedUpAmount = PlayerPrefs.GetInt("speedUpAmount");

        //----------LevelUnlock
        //levelUnlock = PlayerPrefs.GetInt("levelUnlock");
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

        //----------Attributes Upgrade Count
        PlayerPrefs.SetInt("powerUpAmount", powerUpAmount);
        PlayerPrefs.SetInt("critUpAmount", critUpAmount);
        PlayerPrefs.SetInt("lifeUpAmount", lifeUpAmount);
        PlayerPrefs.SetInt("spawnUpAmount", spawnUpAmount);
        PlayerPrefs.SetInt("speedUpAmount", speedUpAmount);

        //----------LevelUnlock
        //PlayerPrefs.SetInt("levelUnlock", levelUnlock);
    }
    public void FirstTime() {

        //Se existir um save, load dados - senao, atribui os valores iniciais
        if (PlayerPrefs.HasKey("power") || PlayerPrefs.HasKey("maxLife"))
        {
            LoadData();
        }
        else
        {
            InitialAttributes(true);
            SaveData();
        }
    }
    #endregion

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
        sliderPower.value = powerUpAmount;
        sliderCritical.value = critUpAmount;
        sliderLife.value = lifeUpAmount;
        sliderSpawnBullet.value = spawnUpAmount;
        sliderSpeedBullet.value = speedUpAmount;
    }

    void SlidersMaxValue()
    {
        sliderPower.maxValue = 100;
        sliderCritical.maxValue = 100;
        sliderLife.maxValue = 100;
        sliderSpawnBullet.maxValue = 100; sliderSpawnBullet.minValue = 0;
        sliderSpeedBullet.maxValue = 100;
    }

    //----------------BTNs FOR UPGRADE (- +) IN PANEL ATTRIBUTES------------------

    //Power Upgrade - Every 1pt attribute add 3pt to power
    public void UpgradePower(bool value)
    {
        if (value && attributesPoints > 0 && powerUpAmount < 100)
        {
            //Up
            power += 3;
            attributesPoints--;
            powerUpAmount++;
        }

        if (!value && powerUpAmount > 0)
        {
            //Down
            power -= 3;
            attributesPoints++;
            powerUpAmount--;
        }

    }

    //Critical Upgrade - Every 1pt attribute add 1pt to critical
    public void UpgradeCritical(bool value)
    {
        if (value && attributesPoints > 0 && critUpAmount < 100)
        {
            //Up
            crit += 1;
            attributesPoints--;
            critUpAmount++;
        }

        if (!value && critUpAmount > 0)
        {
            //Down
            crit -= 1;
            attributesPoints++;
            critUpAmount--;
        }
    }

    //Life Upgrade - Every 1pt attribute add 27pt to life
    public void UpgradeLife(bool value)
    {
        if (value && attributesPoints > 0 && lifeUpAmount < 100)
        {
            //Up
            maxLife += 27;
            attributesPoints--;
            lifeUpAmount++;
        }

        if (!value && lifeUpAmount > 0)
        {
            //Down
            maxLife -= 27;
            attributesPoints++;
            lifeUpAmount--;
        }
    }

    //SpawnBulletDelay Upgrade - Every 5pts attribute, decrease 0.1f to SpawnBulletDelay (1pt = 0.02f)
    public void UpgradeSpawnBullet(bool value)
    {
        if (value && attributesPoints > 0 && spawnUpAmount < 100)
        {
            //Up
            spawnBulletDelay -= 0.02f;
            attributesPoints--;
            spawnUpAmount++;
        }

        if (!value && spawnUpAmount > 0)
        {
            //Down
            spawnBulletDelay += 0.02f;
            attributesPoints++;
            spawnUpAmount--;
        }
    }

    //SpeedBullet Upgrade - Every 1pt attribute add 0,2pt to Speed
    public void UpgradeSpeedBullet(bool value)
    {
        if (value && attributesPoints > 0 && speedUpAmount < 100)
        {
            //Up
            speedBulletMove += 0.2f;
            attributesPoints--;
            speedUpAmount++;
        }

        if (!value && speedUpAmount > 0)
        {
            //Down
            speedBulletMove -= 0.2f;
            attributesPoints++;
            speedUpAmount--;
        }
    }
    #endregion

    #region Btns MainPanel, StatusPanel and WavesPanel
    public void Panels(int value)
    {
        switch (value)
        {
            case 0:
                PanelsControl(true, false, false); //main
                break;
            case 1:
                PanelsControl(false, true, false); //status
                break;
            case 2:
                PanelsControl(false, false, true); //wave
                break;
            default:
                PanelsControl(true, false, false); //main
                break;
        }
        
    }

    /// <summary>  true = ativada, false = desativada  </summary>
    public void PanelsControl(bool valueA, bool valueB, bool valueC)
    {
        mainPanel.SetActive(valueA);
        statusPanel.SetActive(valueB);
        wavesPanel.SetActive(valueC);
    }
    public void DeleteSave()
    {
        PlayerPrefs.DeleteAll();
        InitialAttributes(true);
    }
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        SaveData();
    }
    public void BtnResetAttributes()
    {
        InitialAttributes(false);
        SaveData();
    }

    #endregion

    #region LevelUnlock

    void LevelUnlock()
    {
        Debug.Log(btnLevel.Length);
        for (int i = unlockLevels; i < btnLevel.Length; i++) { 
            btnLevel[i].interactable = false;

            //unlock levels, seu valor determina qual btn de "wave" fica habilitado/desabilitado
            //esse valor determina a fase atual do player
            //quando o player derrotar o chefao da wave atual o numero é atualizado permitindo o player acessar a proxima wave
        } 
    }


    public void LevelActual(int value)
    {
        levelActual = value;
    }
    #endregion
}

