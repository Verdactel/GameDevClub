using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject objectPrefab;
    public float respawnTime = 1.0f;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(objectWave());
    }

    // Spawns the objects
    private void spawnObject(){
        GameObject spawns = Instantiate(objectPrefab) as GameObject;
        spawns.transform.position = new Vector2(screenBounds.x * -2, Random.Range(-screenBounds.y, screenBounds.y));
    }

    //Determines when to spawn the objects
    IEnumerator objectWave(){
        while(true){
        yield return new WaitForSeconds(respawnTime);
        spawnObject();
      } 
    }
}
