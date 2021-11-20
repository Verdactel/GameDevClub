using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    public int m_levelIndex;
    public int m_indexInGroup;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(this.name + " hit " + collision.gameObject.name);
    }
}
