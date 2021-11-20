using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Fill")]
    public ParalaxEngine m_PpralaxEngine;
    public ObstacleManager m_obstacleManager;

    [Header("Touch With Custion")]
    public int m_currentLevelIndex = 0;
    public float m_worldSpeedMultipler = 1.0f;

    [Header("Developer")]
    public bool m_reloadLevel;

    private float m_runDuration = 0.0f;
    private float m_levelLocation = 0.0f;


    void Update()
    {
        m_runDuration += Time.deltaTime;
        m_levelLocation += Time.deltaTime * m_worldSpeedMultipler;


        if (m_reloadLevel)
        {
            m_reloadLevel = false;

            m_PpralaxEngine.LoadNewScene(m_currentLevelIndex);
            m_obstacleManager.SpawnCheck(m_currentLevelIndex, m_levelLocation);
        }

        m_PpralaxEngine.m_speedMult = m_worldSpeedMultipler;
    }
}
