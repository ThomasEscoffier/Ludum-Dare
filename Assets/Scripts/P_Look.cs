using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_Look : MonoBehaviour
{
   [Range(0, 300)]
    public float m_mouseSensitivity = 100f;
    public Transform p_body;

    Vector2 m_mouse;
    float m_xRotation = 0f;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        m_mouse.x = Input.GetAxis("Mouse X") * m_mouseSensitivity * Time.deltaTime;
        m_mouse.y = Input.GetAxis("Mouse Y") * m_mouseSensitivity * Time.deltaTime;

        m_xRotation -= m_mouse.y;
        m_xRotation = Mathf.Clamp(m_xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(m_xRotation, 0f, 0f);

        p_body.Rotate(Vector3.up * m_mouse.x);
    }
}
