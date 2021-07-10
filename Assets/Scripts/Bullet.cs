using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 m_target;

    public float m_speed = 20.0f;
    public float m_lifetime = 3.0f;


    private void Update()
    {
        float step = m_speed * Time.deltaTime;
        this.transform.position = Vector3.MoveTowards(transform.position, m_target, step);
        
        //this.gameObject.transform.position += new Vector3(m_speed * Time.deltaTime, 0f, m_speed * Time.deltaTime);

        if(m_lifetime >= 0.0f)
        {
            m_lifetime -= Time.deltaTime;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
