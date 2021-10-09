using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] bool m_reload;

    [Header("Fill")]
    [SerializeField] ParalaxEngine m_PE;

    public int m_currentLevelIndex = 0;
    public float m_OverallSpeedMultipler = 1.0f;


    void Update()
    {
        if (m_reload)
        {
            m_reload = false;
            
            m_PE.LoadNewSetting(m_currentLevelIndex);
        }

        m_PE.m_speedMult = m_OverallSpeedMultipler;
    }
}
