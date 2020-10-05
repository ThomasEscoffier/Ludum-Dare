using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    public int m_nextScene;

    public GameObject ui_main, ui_opt;

    bool m_isFullscreen;

    private void Awake()
    {
        Screen.fullScreen = true;
        QualitySettings.SetQualityLevel(1, m_isFullscreen);
        ui_opt.SetActive(false);
    }
    public void F_Start()
    {
        SceneManager.LoadScene(m_nextScene);
    }
    public void F_Quit()
    {
        Application.Quit();
    }
    public void F_Opt()
    {
        ui_main.SetActive(false);
        ui_opt.SetActive(true);
    }
    public void F_Back()
    {
        ui_opt.SetActive(false);
        ui_main.SetActive(true);
    }
    public void F_Fullscreen(bool setFullscreen)
    {
        m_isFullscreen = setFullscreen;
        Screen.fullScreen = setFullscreen;
    }
    public void F_HighQ()
    {
        QualitySettings.SetQualityLevel(3, m_isFullscreen);
    }
    public void F_LowQ()
    {
        QualitySettings.SetQualityLevel(1, m_isFullscreen);
    }
}
