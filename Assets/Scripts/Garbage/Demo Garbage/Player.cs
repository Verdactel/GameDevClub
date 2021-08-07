using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float m_player_speed = 1.0f;
    public int m_player_damage = 1;

    public int m_maxHealth = 10;
    public int m_health = 10;
    public bool m_isAlive = true;
    public bool m_isSaving = false;

    public int m_dsMaxCount = 5;
    public float m_dsTime = 2f;
    private int m_dsCurrentCount = 0;

    [SerializeField]
    public GameObject m_ui;
    public TimerBar timerBar;
    private void Start()
    {
        timerBar.SetMaxTime(m_dsTime);
        timerBar.SetTime(m_dsTime);
    }
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
        if(!m_isAlive && m_isSaving)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_dsCurrentCount++;
            }
            
            if(m_dsCurrentCount >= m_dsMaxCount)
            {
                m_isAlive = true;
                m_health = m_maxHealth;

                m_isSaving = false;
                m_dsTime = 2f;
                timerBar.SetTime(m_dsTime);

                m_dsCurrentCount = 0;
                m_ui.SetActive(false);
            }

            if(m_dsTime >= 0f)
            {
                m_dsTime -= Time.deltaTime;
                timerBar.SetTime(m_dsTime);
            }
            else
            {
                m_isSaving = false;
                Debug.Log("Defeated");
                m_ui.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Owchy");

        m_health -= 1;
        if (m_health <= 0)
        {
            m_isAlive = false;
            DeathSave();
        }

        Destroy(other.gameObject);
    }
    private void DeathSave()
    {
        m_isSaving = true;
        m_ui.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("No more owchy");
    }
}
