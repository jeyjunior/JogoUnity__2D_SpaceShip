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

    //Death
    public GameObject deathPanel;
    public bool dead;

    void Start()
    {
        spaceShip = GameObject.FindWithTag("Player").GetComponent<SpaceShip>();

        //Life
        currentLife = maxLife;
        LoadData();

        //DeathPanel
        deathPanel.SetActive(false);
    }


    void Update()
    {
        ExperienceControl();
        LifeBarControl();
    }

    #region Life and Death Control
    void LifeBarControl()
    {
        spaceShip.lifeBar.value = currentLife;
        if(currentLife <= 0)
        {
            dead = true;
            deathPanel.SetActive(true);
        }
        else
        {
            dead = false;
        }
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
            currentLife = maxLife;
            spaceShip.lifeBar.maxValue = maxLife;
        }

        spaceShip.expBarSlider.value = currentExp;
    }

    public void GainingExperience(int exp){
        currentExp += exp;
    }
    #endregion

    #region Load and Save
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

    public void LoadData()
    {
        //Load quando jogo começar
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
    #endregion

    #region Btn BackToMain and RestartGame
    public void BackToMain()
    {
        SaveData();
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }
    #endregion
}
