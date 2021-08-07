using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] GameObject m_player;
    public float m_dificulty = 1.0f;

    void Update()
    {
        m_dificulty += Time.deltaTime;
        
        
    }
}
