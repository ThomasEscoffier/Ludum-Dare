using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DebugFps : MonoBehaviour
{
    public Text ui_Debug;

    float m_deltaTime;

    void Update()
    {
        m_deltaTime += (Time.deltaTime - m_deltaTime) * 0.1f;
        float fps = 1.0f / m_deltaTime;

        ui_Debug.text = "FPS : " + Mathf.Ceil(fps).ToString();
    }
}
