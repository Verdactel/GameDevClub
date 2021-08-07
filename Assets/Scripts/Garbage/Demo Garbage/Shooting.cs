using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject m_target;
    public GameObject m_proj;

    public float m_difficulty = 1.0f;

    private float m_shootTime = 1.0f;

    private void Update()
    {
        if (m_shootTime <= 0.0f)
        {
            Fire();
            m_shootTime = 3.0f / (m_difficulty);
        }
        m_shootTime -= Time.deltaTime;
        m_difficulty += Time.deltaTime * 2;
    }
    public void Fire()
    {
        Vector3 target = m_target.transform.position;
        target = new Vector3(target.x + Random.Range(-1.0f, 1.0f) * Mathf.Clamp((m_difficulty / 5.0f), 0.0f, 8.0f), 0.0f, target.z + Random.Range(-1.0f, 1.0f) * Mathf.Clamp((m_difficulty / 5.0f), 0.0f, 8.0f));
        target *= 100.0f;
        m_proj.GetComponent<Bullet>().m_target = target;
        Instantiate(m_proj);
    }
}
