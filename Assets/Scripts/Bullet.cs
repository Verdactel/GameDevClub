using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject targetObj;
    private Transform m_target;

    public float m_speed = 20f;
    public float m_lifetime = 2f;

    private void Awake()
    {
        if (!targetObj) Debug.Log("No object selected");
        else m_target = targetObj.transform;
    }
    private void Update()
    {
        if (!targetObj) Debug.Log("No object selected");
        //MoveTo
        else 
        {
            Debug.Log("Moving towards " + m_target.position);
            float step = m_speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(transform.position, m_target.position, step);
        }
        
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
