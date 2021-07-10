using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject m_proj;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }
    public void Fire()
    {
        Instantiate(m_proj);
    }
}
