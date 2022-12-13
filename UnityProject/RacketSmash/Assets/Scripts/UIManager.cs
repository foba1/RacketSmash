using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

enum Mode
{
    mole, crazy, survival, brick
}
public class UIManager : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject modeSelectPanel;
    [SerializeField] GameObject settingPanel;
    [SerializeField] GameObject scorePanel;

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

    public void SetScorePanel(bool state)
    {
        scorePanel.SetActive(state);
    }

    public void UpdateScore()
    {
        if (PlayerPrefs.HasKey("Mole"))
        {
            string scoreText = PlayerPrefs.GetString("Mole");
            scorePanel.transform.GetChild(3).GetComponent<Text>().text = "두더지잡기 : " + scoreText;
        }
        else scorePanel.transform.GetChild(3).GetComponent<Text>().text = "두더지잡기 : X";

        if (PlayerPrefs.HasKey("Crazy"))
        {
            string scoreText = PlayerPrefs.GetString("Crazy");
            scorePanel.transform.GetChild(4).GetComponent<Text>().text = "크레이지 : " + scoreText;
        }
        else scorePanel.transform.GetChild(4).GetComponent<Text>().text = "크레이지 : X";

        if (PlayerPrefs.HasKey("Survival"))
        {
            string scoreText = PlayerPrefs.GetString("Survival");
            scorePanel.transform.GetChild(5).GetComponent<Text>().text = "서바이벌 : " + scoreText;
        }
        else scorePanel.transform.GetChild(5).GetComponent<Text>().text = "서바이벌 : X";

        if (PlayerPrefs.HasKey("Brick"))
        {
            string scoreText = PlayerPrefs.GetString("Brick");
            scorePanel.transform.GetChild(6).GetComponent<Text>().text = "벽돌깨기 : " + scoreText;
        }
        else scorePanel.transform.GetChild(6).GetComponent<Text>().text = "벽돌깨기 : X";
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
            SceneManager.LoadScene("Brickout");
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ExitToMain()
    {
        SceneManager.LoadScene("Main");
    }
}
