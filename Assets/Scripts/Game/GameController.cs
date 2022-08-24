using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [Space(5)]
    [Header("Attributes Base")]
    public int power;
    public int crit;
    public int maxLife;
    public int currentLife;
    public float spawnBulletDelay;
    public float speedBulletMove;

    [Space(5)]
    [Header("Exp and Lvl Control")]
    public int maxExp;
    public int currentExp;
    public int lvlPlayer;
    public int attributesPoints;


    [Space(5)]
    [Header("SpaceShip")]
    public SpaceShip spaceShip;

    [Space(5)]
    [Header("Death")]
    public GameObject deathPanel;

    [Space(5)]
    [Header("Win")]
    public GameObject winPanel;
    public bool bossElimineted;

    [Space(5)]
    [Header("TimeControl")]
    public int timeCount = 0;

    public int unlockLevels = 1;
    public int levelActual = 0;
    void Start()
    {
        levelActual = PlayerPrefs.GetInt("levelActual");

        bossElimineted = false;
        EndLevel(false, false, 1, true);

        spaceShip = GameObject.FindWithTag("Player").GetComponent<SpaceShip>();

        //Life
        currentLife = maxLife;
        
        //DeathPanel
        deathPanel.SetActive(false);
        LoadData();
    }


    void Update()
    {
        ExperienceControl();
        LifeBarControl();

        if (bossElimineted) EndLevel(true, false, 0, false);
    }

    #region LifeBar
    void LifeBarControl()
    {
        spaceShip.lifeBar.value = currentLife;
        if (currentLife <= 0) EndLevel(false, true, 0, false);
    }
    #endregion

    #region Take DMG from enemies
    public void TakeDamage(int dmgReceive)
    {
        currentLife -= dmgReceive;
    }
    #endregion

    #region Experience and Level Control
    void ExperienceControl()
    {
        if (currentExp >= maxExp)
        {
            currentExp -= maxExp;
            maxExp += 20;
            lvlPlayer++;
            attributesPoints += 3; //pontos para usuario distribuir como quiser nos atributos
            spaceShip.expBarSlider.maxValue = maxExp; //atualiza exp max

            currentLife = maxLife;
            spaceShip.lifeBar.maxValue = maxLife;
        }

        spaceShip.expBarSlider.value = currentExp;
    }

    public void GainingExperience(int exp)
    {
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

        unlockLevels = PlayerPrefs.GetInt("unlockLevels");
    }
    #endregion

    #region Panels
    public void BackToMain()
    {
        SaveData();


        //libera proxima fase apenas quando o player jogar na ultima fase liberada
        if(levelActual >= unlockLevels && bossElimineted)
        {
            unlockLevels++;
            PlayerPrefs.SetInt("unlockLevels", unlockLevels);
        }

        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SaveData();
        SceneManager.LoadScene(1);
    }

    void EndLevel(bool winPanel_, bool deathPanel_, float timeScale, bool sliderMove)
    {
        winPanel.SetActive(winPanel_);
        deathPanel.SetActive(deathPanel_);
        Time.timeScale = timeScale;
        GameObject.FindGameObjectWithTag("Player").GetComponent<SpaceShip>().valueMove.interactable = sliderMove;
    }

   #endregion
}