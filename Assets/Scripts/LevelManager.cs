using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("Developer")]
    public bool m_reloadScene;

    [Header("Fill")]
    public ParalaxEngine m_PpralaxEngine;
    public ObstacleManager m_obstacleManager;

    [Header("Touch With Custion")]
    public int m_currentLevelIndex = 0;
    public float m_worldSpeedMultipler = 1.0f;

    private float m_runDuration = 0.0f;
    private float m_levelLocation = 0.0f;


    void Update()
    {
        #region Other
        {
            m_runDuration += Time.deltaTime;
            m_levelLocation += Time.deltaTime * m_worldSpeedMultipler;
        }
        #endregion

        #region Paralax Engine
        {
            if (m_reloadScene)
            {
                m_reloadScene = false;

                m_PpralaxEngine.LoadNewScene(m_currentLevelIndex);
            }

            m_PpralaxEngine.m_speedMult = m_worldSpeedMultipler;
        }
        #endregion
    }
}
