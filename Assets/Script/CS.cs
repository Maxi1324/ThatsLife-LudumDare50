using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CS : MonoBehaviour
{
    public Transform Trans;
    public float Dur = 0f;
    public float str = 0.7f;
    public float df = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (Trans == null)
        {
            Trans = GetComponent<Transform>();
        }
    }

    void OnEnable()
    {
        originalPos = Trans.localPosition;
    }

    void Update()
    {
        if (Dur > 0)
        {
            Trans.localPosition = originalPos + Random.insideUnitSphere * str;

            Dur -= Time.deltaTime * df;
        }
        else
        {
            Dur = 0f;
            Trans.localPosition = originalPos;
        }
    }
}
