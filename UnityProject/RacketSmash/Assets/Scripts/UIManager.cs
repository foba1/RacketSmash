using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum Mode
{
    mole, crazy, survival, brick
}
public class UIManager : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject modeSelectPanel;
    public GameObject settingPanel;

    public void SetMainPanel(bool state)
    {
        mainPanel.SetActive(state);
    }

    public void SetModeSelectPanel(bool state)
    {
        modeSelectPanel.SetActive(state);
    }

    public void SetSettingPanel(bool state)
    {
        settingPanel.SetActive(state);
    }

    public void RestartGame(bool state)
    {
        Debug.Log("Restart clicked");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SelectMode(int index)
    {
        if (index == (int)Mode.mole)
        {
            SceneManager.LoadScene("Mole");
        }
        else if (index == (int)Mode.crazy)
        {
            SceneManager.LoadScene("Crazy");
        }
        else if (index == (int)Mode.survival)
        {
            SceneManager.LoadScene("Survival");
        }
        else if (index == (int)Mode.brick)
        {

        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
