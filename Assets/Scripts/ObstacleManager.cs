using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Fill")]
    public List<Obstacle> objectsToThrow;
    public float respawnTime = 1.0f;

    void Start()
    {
    }
}
