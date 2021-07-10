using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float m_speed = 20f;
    public float m_lifetime = 2f;

    private void Update()
    {
        this.gameObject.transform.position += new Vector3(m_speed * Time.deltaTime, 0f, m_speed * Time.deltaTime);

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
