using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour
{
    [Header("Fine Toon")]
    public float m_jumpStrength = 30.0f;
    public float m_horizontalSpeed = 12.0f;
    public float m_gravity = 0.2f;
    [Tooltip("-1 = running no jumping, 0 = running & jumping, 1 = glide when fall (while holding jump), 2 = flapy bird, 3 = flying")]
    public int m_isFlying = 0;
    [Tooltip("multiplyer for gravity to allow stronger jump when key held vs not held. 0.1 = fall 90% slower.")]
    public float m_gravityResist = 0.5f;
    public float m_glidingFallClamp = -2.0f;//if in fly state 1, "glide" by not falling faster than x speed
    public float m_flapStrength = 20.0f;//jump strength but for already in the air with fly state 2
    public float m_flySpeed = 16.0f;//vertical speed if in fly state 3

    public int m_maxHealth = 100;

    [Header("Fill")]
    public float m_groundY = -7.95f; //the ground is currently just a number. this is on the assumption that there are no platforms in the game
    public Transform m_bottomPoint; //players feet. what point should be held at the ground
    public Slider m_healthBarSlider;

    [Header("Touch with cution")]
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
            //Gravity (and resistance to). there is no gravity if in fly state 3
            if (m_isFlying < 3)
            {
                if (Input.GetAxis("Vertical") > 0)
                {
                    //higher jump when key held
                    m_velocity.y -= m_gravity * m_gravityResist;

                    //glid clamp
                    if (m_isFlying == 1)
                    {
                        if (m_velocity.y < m_glidingFallClamp)
                        {
                            m_velocity.y = m_glidingFallClamp;
                        }
                    }
                }
                //If you aren't flying, and you arent trying to stay in the air, then just fall forhead
                else
                {
                    m_velocity.y -= m_gravity;
                }                
            }

            //if jump key is held
            if (Input.GetAxis("Vertical") > 0)
            {
                //Jump
                if (m_isOnGround && m_isFlying >= 0)
                {

                    m_velocity.y = m_jumpStrength;

                }
                //Flap
                else if (m_isFlying == 2 && Input.GetButtonDown("Vertical"))
                {
                    m_velocity.y = m_flapStrength;
                }
            }
            //Fly
            if (m_isFlying == 3)
            {
                m_velocity.y = m_flySpeed * Input.GetAxis("Vertical");
            }
            //Run
            m_velocity.x = m_horizontalSpeed * Input.GetAxis("Horizontal");

            //stop at ground
            m_isOnGround = m_bottomPoint.position.y <= m_groundY;
            if (m_isOnGround && m_velocity.y < 0.0f)
            {
                m_velocity.y = 0.0f;
            }

            //Apply
            this.gameObject.transform.position += (new Vector3(m_velocity.x, m_velocity.y, 0.0f) * Time.deltaTime);
        }
        #endregion

        #region Lock Position to Screen
        {
            //full length, not distance from center (20 = -10 to 10)
            //screen hight = 20 assuming no body messed with the camera
            float screenWidth = 20.0f / ((float)Screen.height / (float)Screen.width);

            //If you're off the various sides of the screen, get set to at said side of screen
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

        #region Health
        {
            m_healthBarSlider.value = ((float)m_health / (float)(m_maxHealth));
            if (m_health <= 0)
            {
                Debug.Log("Ouchy Wa Wa, I'm a dead! I've died " + (m_deathCount + 1) + " time(s) now!");
                m_deathCount++;
                m_health = m_maxHealth;
            }
        }
        #endregion
    }
}
