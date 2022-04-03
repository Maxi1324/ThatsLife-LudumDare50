using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject orb;
    public Transform spawn;
    
    private GameObject AktOrb;
    private float Timer;

    private bool erstesmal = true;
    private bool bereitsN = false;

    private void Update()
    {
        Timer += Time.deltaTime;

        if(AktOrb == null && !bereitsN)
        {
            Timer = 0;
            bereitsN = true;
        }
        if ((AktOrb == null && Timer > 5) || erstesmal)
        {
            bereitsN = false;
            erstesmal = false;
            Spawn();
        }
    }

    public void Spawn()
    {
        AktOrb = Instantiate(orb, spawn.position, spawn.rotation);
    }
}
