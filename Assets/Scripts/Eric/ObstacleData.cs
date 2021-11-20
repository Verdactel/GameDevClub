using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class ObstacleData
{
    [Header("Fine Toon")]
    public AnimationCurve m_xLocation;
    public AnimationCurve m_yLocation;
    public float lifetime;
    public float m_distanceFromPrevious;
    [Tooltip("Does the X speed scale with level speed")]
    public bool m_isXSpeedRelative;
    [Tooltip("Does the Y speed scale with level speed")]
    public bool m_isYSpeedRelative;
    public Vector2 m_scale;

    [Header("Fill")]
    public Sprite m_sprite;
}
