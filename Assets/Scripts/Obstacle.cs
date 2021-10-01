using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public float speed = 10.0f;
    public float existTime = 4.0f;
    private Rigidbody rb;
    private Vector2 screenBounds;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));

    }

    // Update is called once per frame
    void Update()
    {
        WaitAndDestroy(existTime);
    }

    // Delay to death function
    private void WaitAndDestroy(float delay)
    {
        ObjectDeath(delay);
    }

    //Runs on a collision
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit Detected");
        ObjectDeath(0);
    }

    //Death Method
    public void ObjectDeath(float delay)
    {
        Destroy(this.gameObject, delay);
    }
}
