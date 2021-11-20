using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class ParalaxLayer
{
    [Header("Fine Toon")]
    public bool m_isActive = true;
    public Sprite m_sprite;
    public bool m_doesTile = false;
    public bool m_isForeground = false;
    public Vector2 m_scale;
    [Tooltip("Y off set moves it off camera, but X off set will only move the starting position keeping it spread all the way across the screen")]
    public Vector2 m_positionOffSet;
    public float m_gap;
    public float m_scrollSpeed;
}
