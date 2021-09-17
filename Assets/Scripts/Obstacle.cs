using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 10.0f;
    private Rigidbody rb;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    //Runs on a collision
    private void OnCollisionEnter(Collision other){
        Debug.Log("Hit Detected");
        ObjectDeath();
    }
    
    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < screenBounds.x * 2){
            ObjectDeath();
        }
    }

    //Death Method
    public void ObjectDeath(){
        Destroy(this.gameObject);
    }
}
