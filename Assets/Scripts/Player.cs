using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_player_speed = 1.0f;
    public int m_player_damage = 1;

    public int m_health = 10;
    public bool m_isAlive = true;

    void Update()
    {
        //Move
        if (m_isAlive)
        {
            if (Input.GetKey(KeyCode.W))
            {
                this.gameObject.transform.position += (Vector3.forward * m_player_speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                this.gameObject.transform.position += (Vector3.left * m_player_speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.S))
            {
                this.gameObject.transform.position += (Vector3.back * m_player_speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.D))
            {
                this.gameObject.transform.position += (Vector3.right * m_player_speed * Time.deltaTime);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Owchy");

        m_health -= 1;
        if (m_health <= 0)
        {
            m_isAlive = false;
        }
    }
}
