using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float m_speed = 20f;
    public float m_lifetime = 2f;
    public Rigidbody rb;
    void Start()
    {
        rb.velocity = transform.forward * m_speed * Time.deltaTime;
    }
}
