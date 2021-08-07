using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] public class ParalaxLayer
{
    public Sprite m_sprite;
    public bool m_doesTile = false;
    public bool m_isForeground = false;
    public Vector2 m_scale;
    public Vector2 m_positionOffSet; // Y off set moves it off camera, but X off set will only move the starting position keeping it spread all the way across the screen
    public float m_gap; // gap between images. Good for foreground
    public float m_scrollSpeed;
}
