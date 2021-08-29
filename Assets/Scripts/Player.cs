using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Fine Toon")]
    [SerializeField] float m_jumpStrength = 30.0f;
    [SerializeField] float m_horizontalSpeed = 12.0f;
    [SerializeField] float m_gravity = 0.2f;
    [Tooltip("-1 = running no jumping, 0 = running & jumping, 1 = glide when fall (while holding jump), 2 = flapy bird, 3 = flying")]
        [SerializeField] int m_isFlying = 0;
    [Tooltip("multiplyer for gravity to allow stronger jump when key held vs not held. 0.1 = fall 90% slower.")]
        [SerializeField] float m_gravityResist = 0.5f;
    [SerializeField] float m_glidingFallClamp = -2.0f;
    [SerializeField] float m_flapStrength = 20.0f;
    [SerializeField] float m_flySpeed = 12.0f;

    [SerializeField] int m_maxHealth = 100;

    [Header("Fill")]
    [SerializeField] float m_groundY = -7.95f;
    [SerializeField] Transform m_bottomPoint;
    

    [Header("\"Public Variables (Touch with cution)\"")]
    public bool m_isOnGround = true;

    public int m_health = 0;
    public int m_deathCount = 0;


    //
    private Vector2 m_velocity = new Vector2();

    private void Start()
    {
        m_health = m_maxHealth;
    }


    void Update()
    {
        #region Movement
        {
            //Gravity (and resistance to)
            if (m_isFlying < 3)
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    m_velocity.y -= m_gravity * m_gravityResist;

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
                    m_velocity.y -= m_gravity;
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
            //Fly
            if (m_isFlying == 3)
            {
                m_velocity.y = m_horizontalSpeed * Input.GetAxis("Vertical");
            }
            //Run
            m_velocity.x = m_horizontalSpeed * Input.GetAxis("Horizontal");


            //Apply
            this.gameObject.transform.position += (new Vector3(m_velocity.x, m_velocity.y, 0.0f) * Time.deltaTime);
        }
        #endregion

        #region Lock Position to Screen
        {
            //full length, not distance from center (20 = -10 to 10)
            //screen hight = 20 assuming no body messed with the camera
            float screenWidth = 20.0f / ((float)Screen.height / (float)Screen.width);

            Vector3 pos = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
            if (this.gameObject.transform.position.x < -(screenWidth / 2))
            {
                pos.x = -(screenWidth / 2);
            }
            else if (this.gameObject.transform.position.x > (screenWidth / 2))
            {
                pos.x = (screenWidth / 2);
            }
            if (this.gameObject.transform.position.y < -10)
            {
                pos.y = -10;
            }
            else if (this.gameObject.transform.position.y > 10)
            {
                pos.y = 10;
            }
            this.gameObject.transform.position = pos;
        }
        #endregion
    }
}
