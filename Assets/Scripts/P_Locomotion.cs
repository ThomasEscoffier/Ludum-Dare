using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class P_Locomotion : MonoBehaviour
{
    public bool m_isGrounded;
    public bool isTalking = false;

    public float m_speed = 12f;
    public float m_jumpHeight = 3f;
    public float m_gravity = -9.81f;
    public float m_groundDistance = 0.5f;

    public Transform m_groundCheck;
    public LayerMask m_groundMask;

    Vector2 m_moveInput;
    Vector3 m_velocity;

    CharacterController m_characterController;

    void Start()
    {
        m_characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!VD.isActive)
        {
            isTalking = false;
        }
        else
        {
            isTalking = true;
        }

        if(!isTalking)
        {
            //check if is grounded

            m_isGrounded = Physics.CheckSphere(m_groundCheck.position, m_groundDistance, m_groundMask);

            if(m_isGrounded && m_velocity.y < 0)
            {
                m_velocity.y = -2f;
            }

            //inputs

            m_moveInput.x = Input.GetAxis("Horizontal");
            m_moveInput.y = Input.GetAxis("Vertical");

            //move

            Vector3 m_move = transform.right * m_moveInput.x + transform.forward * m_moveInput.y;

            m_characterController.Move(m_move * m_speed * Time.deltaTime);

            //jump

            if (Input.GetButtonDown("Jump") && m_isGrounded)
            {
                m_velocity.y = Mathf.Sqrt(m_jumpHeight * -2f * m_gravity);
            }

            //gravity

            m_velocity.y += m_gravity * Time.deltaTime;
            m_characterController.Move(m_velocity * Time.deltaTime);
        }
    }
}
