using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPlanetMovement : MonoBehaviour
{
    public Transform Center;
    public Rigidbody rb;
    public LayerMask mask;

    public Transform dings;

    public float gravity = 10;

    private Vector2 pos;
    public float t;

    public float r = 3;

    // Start is called before the first frame update
    void Start()
    {
        if (Center == null)
        {
            Center = GameObject.Find("Planet").GetComponent<Transform>();
        }
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        RaycastHit hit;
        Vector3 v = Vector3.up;
        if (Physics.Raycast(transform.position,transform.forward,out hit, Mathf.Infinity, mask))
        {
            v = -hit.normal;
        }*/

     

        Vector3 relativePos = Center.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;

        /*rb.AddForce(transform.forward * gravity);
         move(transform.forward);

         transform.Translate(transform.up * fSpeed);
         */

        transform.position = Vector3.Lerp(transform.position, Center.transform.position,t);
        Debug.DrawRay(transform.position, transform.up * 100);
    }

    public float ConvertToRadians(float angle)
    {
        return (float)Math.PI / 180 * angle;
    }
}