using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public sealed class World // Used for queues in resolving conflicts later
{
    // Singleton to ensure one instance is made at a time
    private static readonly World instance = new World();
    private static WorldStates world;

    private static Queue<GameObject> patients;

    static World()
    {
        world = new WorldStates();
        patients = new Queue<GameObject>();
    }

    private World()
    {

    }

    public void AddPatient(GameObject p)
    {
        patients.Enqueue(p);
    }

    public GameObject RemovePatient()
    {
        if (patients.Count == 0)
        {
            return null;
        }
        return patients.Dequeue();
    }

    public static World Instance
    {
        get { return instance; }
    }

    public WorldStates GetWorld()
    {
        return world;
    }
}
