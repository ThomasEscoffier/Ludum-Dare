using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_LookAtPlayer : MonoBehaviour
{
    public LayerMask m_playerLayer;
    public Transform m_target;
    public Transform m_entity;

    public float m_checkRadius;
    public float m_rotSpeed;

    bool m_isVisible; 
    Quaternion m_originRotation;

    // Update is called once per frame
    private void Awake()
    {
        m_originRotation = Quaternion.identity;
    }
    void Update()
    {
        m_isVisible = Physics.CheckSphere(m_entity.position, m_checkRadius, m_playerLayer);

        if (m_isVisible)
        {
            Vector3 relativePos = m_target.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(relativePos);
            transform.localRotation = Quaternion.Lerp(transform.rotation, toRotation, m_rotSpeed * Time.deltaTime);
        } 
        else
        {
            transform.localRotation = Quaternion.Lerp(transform.rotation, m_originRotation, m_rotSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(m_entity.position, m_checkRadius);
    }
}
