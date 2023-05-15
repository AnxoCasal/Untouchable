using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public GameObject lvl2Btn;
    public GameObject lvl3Btn;
    public void goMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevels()
    {
        if (PlayerPrefs.GetInt("lvl1pass", 0) == 1)
        {
            lvl2Btn.SetActive(true);
        }
        else
        {
            lvl2Btn.SetActive(false);
        }

        if (PlayerPrefs.GetInt("lvl2pass", 0) == 1)
        {
            lvl3Btn.SetActive(true);
        }
        else
        {
            lvl3Btn.SetActive(false);
        }
    }
    public void Leve1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void Level2()
    {
        SceneManager.LoadScene("Level2");
    }
    public void Level3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
