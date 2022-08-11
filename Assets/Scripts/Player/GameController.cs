using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour
{
    public float speedBulletMove = 2;
    public float spawnBulletDelay = 2;

    // Start is called before the first frame update
    void Start()
    {
        LoadStats();
        if (SceneManager.GetActiveScene().name == "Level" || SceneManager.GetActiveScene().name == "Main")
        {
            //Carrega os upgrades para o player
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BtnMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void BtnLevel()
    {
        SceneManager.LoadScene("Level");
        //Salva os upgrades no player
        SaveStats();
    }



    public void SaveStats()
    {
        PlayerPrefs.SetFloat("speedBulletMove", speedBulletMove);
        PlayerPrefs.SetFloat("spawnBulletDelay", spawnBulletDelay);
    }

    public void LoadStats()
    {
        PlayerPrefs.GetFloat("speedBulletMove");
        PlayerPrefs.GetFloat("spawnBulletDelay");
    }

}
