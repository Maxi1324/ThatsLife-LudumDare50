using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

[Serializable]
public struct CH
{
    public Vector3 ER;

    public NavMeshData DA;
}

public class DONavMeshStuff : MonoBehaviour
{
    [SerializeField]
    public Transform center;

    [SerializeField]
    private List<CH> ch;

    [SerializeField]
    private List<NavMeshDataInstance> inst = new List<NavMeshDataInstance>();

    public void OnEnable()
    {
        delAll();
        loadNM();
    }

    public void OnDisable()
    {
        foreach (var instance in inst)
        {
            instance.Remove();
        }
    }

    public void loadNM()
    {
        foreach (var chunk in ch)
        {
            inst.Add(
                NavMesh.AddNavMeshData(
                    chunk.DA,
                    center.transform.position,
                    Quaternion.Euler(chunk.ER)));
        }
    }

    public void delAll()
    {
        NavMesh.RemoveAllNavMeshData();
    }
}

