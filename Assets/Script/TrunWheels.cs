using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrunWheels : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (WorldGen.over) return;
        transform.Rotate(0, 0,500 * Time.deltaTime);
    }
}
