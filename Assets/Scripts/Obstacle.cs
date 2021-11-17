using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    public AnimationCurve m_xLocation;
    public AnimationCurve m_yLocation;
    public float lifeTime = 0.0f;

    void Start()
    {
    }

    void Update()
    {
        ObjectDeath(lifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("Hit Detected with: " + other);
        ObjectDeath(0);
    }

    public void ObjectDeath(float delay)
    {
        Destroy(this.gameObject, delay);
    }
}
