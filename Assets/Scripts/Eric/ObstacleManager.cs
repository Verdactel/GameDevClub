using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [Header("Fine Toon")]
    public List<ObstacleGroup> m_obstacleGroups;

    private int m_obstacleIndex = 0;
    private float m_lastSpawnLocation = 0.0f;
    private List<GameObject> m_activeObjects = new List<GameObject>();

    private void Update()
    {
        
    }

    public void SpawnCheck(int levelIndex, float runLocation)
    {
        if (runLocation < m_lastSpawnLocation)
        {
            //!!!!! A method to destory all obstacles might be good here. For now I am making it where live obstacles objects just keep going if the level changes

            m_lastSpawnLocation = 0.0f;
        }

        if (runLocation - m_lastSpawnLocation >= m_obstacleGroups[levelIndex].m_obstacles[m_obstacleIndex].m_distanceFromPrevious)
        {
            m_lastSpawnLocation = runLocation;

            //Create Object
            GameObject obstacle = new GameObject();
            obstacle.transform.parent = this.transform;
            obstacle.name = "Obstacle " + (m_obstacleIndex - 1);
            m_activeObjects.Add(obstacle);
            //Apply image
            SpriteRenderer spriteR = obstacle.AddComponent<SpriteRenderer>();
            spriteR.sprite = m_obstacleGroups[levelIndex].m_obstacles[m_obstacleIndex].m_sprite;
            spriteR.transform.localScale *= m_obstacleGroups[levelIndex].m_obstacles[m_obstacleIndex].m_scale;
            //Make it an obstacle
            obstacle.AddComponent<ObstacleObject>();
            obstacle.GetComponent<ObstacleObject>().m_levelIndex = levelIndex;
            obstacle.GetComponent<ObstacleObject>().m_indexInGroup = m_obstacleIndex;

            //We've spawned one, so now look at the next one.
            m_obstacleIndex++;
        }
    }
}
