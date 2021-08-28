using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Touch")]
    [SerializeField] float m_jumpStrength = 5.0f;
    [SerializeField] float m_horizontalSpeed = 2.0f;
        [SerializeField] float m_gravity = 1.0f;

    [SerializeField] float m_groundY = -7.95f;
    [Tooltip("-1 = running no jumping, 0 = running & jumping, 1 = glide when fall (while holding jump), 2 = flapy bird, 3 = flying")]
        [SerializeField] int m_isFlying = 0;
    [Tooltip("multiplyer for gravity to allow stronger jump when key held vs not held. 0.1 = fall 90% slower.")]
        [SerializeField] float m_gravityResist = 0.7f;
    [SerializeField] float m_glidingFallClamp = 1.0f;
    [SerializeField] float m_flapStrength = 5.0f;
    [SerializeField] float m_flySpeed = 3.0f;

    [SerializeField] Transform m_bottomPoint;
    

    [Header("Don't Touch")]
    public bool m_isOnGround = true;

    public int m_health = 100;
    public int m_deathCount = 0;


    //
    private Vector2 m_velocity = new Vector2();

    void Update()
    {
        #region Movement
        {
            //Gravity (and resistance to)
            float gravity = 3.0f;
            if (m_isFlying < 3)
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    m_velocity.y -= gravity * m_gravityResist;

                    if (m_isFlying == 1)
                    {
                        if (m_velocity.y < m_glidingFallClamp)
                        {
                            m_velocity.y = m_glidingFallClamp;
                        }
                    }
                }
                else
                {
                    m_velocity.y -= gravity * Time.deltaTime;
                }

                
            }

            m_isOnGround = m_bottomPoint.position.y <= m_groundY;
            if (m_isOnGround && m_velocity.y < 0.0f)
            {
                m_velocity.y = 0.0f;
            }

            if (Input.GetAxis("Vertical") > 0)
            {
                //Jump
                if (m_isOnGround && m_isFlying >= 0 && Input.GetButtonDown("Vertical"))
                {

                    m_velocity.y = m_jumpStrength;

                }
                //Flap
                else if (m_isFlying == 2 && Input.GetButtonDown("Vertical"))
                {
                    m_velocity.y = m_flapStrength;
                }
                //Fly
                else if (m_isFlying == 3)
                {
                    m_velocity.y = m_flySpeed;
                }
            }
            //Run
            m_velocity.x = m_horizontalSpeed * Input.GetAxis("Horizontal");


            //Apply
            this.gameObject.transform.position += (new Vector3(m_velocity.x, m_velocity.y, 0.0f) * Time.deltaTime);
        }
        #endregion

        #region Lock Position
        {
            //screen hight = 10 assuming no body messed with the camera
            float screenWidth = 20.0f / ((float)Screen.height / (float)Screen.width);

            if (this.gameObject.transform.position < 0)
        }
        #endregion
    }
}
