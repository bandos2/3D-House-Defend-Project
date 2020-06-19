using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnableObject
{
    public GameObject Object;
    public int id;
    public string name;
    public Vector3 PlaceToSpawnObject;
    public float LifeTime;
}
