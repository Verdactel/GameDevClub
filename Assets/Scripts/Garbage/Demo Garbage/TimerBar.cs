using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    public Slider m_slider;

    public void SetMaxTime(float time)
    {
        m_slider.maxValue = time;
        m_slider.value = time;
    }

    public void SetTime(float time)
    {
        m_slider.value = time;
    }
}
