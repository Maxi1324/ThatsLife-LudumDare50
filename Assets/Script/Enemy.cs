using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent Agent;
    public GameObject AM;
    public GameObject Player;
    public GameObject Center;
    void Start()
    {
        
    }

    void Update()
    {
        Agent.SetDestination(Player.transform.position);

        Vector3 relativePos = Center.transform.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.localRotation = rotation;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player" && collision.transform.GetComponent<Player>().hasBoost)
        {
            Destroy(gameObject);
        }
    }
}
