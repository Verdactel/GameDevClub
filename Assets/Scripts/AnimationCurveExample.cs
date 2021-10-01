using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurve : MonoBehaviour
{

    public AnimationCurve lerpCurve;
    public Vector3 lerpOffset;
    public float lerpTime = 3f;
    private float _timer = 0f;

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer > lerpTime)
        {
            _timer = lerpTime;
        }

        float lerpRatio = _timer / lerpTime;

    }
}
