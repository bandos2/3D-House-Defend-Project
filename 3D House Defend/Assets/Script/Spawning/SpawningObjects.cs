using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawningObjects : MonoBehaviour
{
    //[SerializeField]
    //public GameObject[] SpawningGameObjects;

    public SpawnableObject [] ObjectsToSpawn; //Array Of possible spawning objects

    public bool SpawnedGameObjectsHAVELimit;
    [Range(1f,30f)]
    public int SpawnedGameObjectsLimit; 

    public int CurrentlySpawnedObjects = 0;

    //public GameObject[] SpawnedObjects; //already spawned objects (need more work on it but is it really helpful?)


    public bool IsObjectsHaveALifeTime;
    public bool SetLifeTimeManualyForAllObjects;
    [Range(1f, 30f)]
    public float SpawnedGameObjectsLifeTime;

    [SerializeField]
    public Transform PlaceToSpawnObjects; //

    Vector3 PlaceToSpawn;

    [SerializeField]
    public bool SpawnOverTime;

    [SerializeField]
    public float SpawnEveryXSeconds;

    [SerializeField]
    public bool RandomizeSpawningPositions;

    [Range(0f, 10f)]
    public float RandomizingRangeX;
    [Range(0f, 10f)]
    public float RandomizingRangeY;
    [Range(0f, 10f)]
    public float RandomizingRangeZ;

    public Vector3 SpawnPoint;
    Vector3 SpawnPointStartPosition;
    bool SpawningCoorutineIsRunning = false;

    public List<GameObject> SpawnedObjects;

    //  public string choice;

    void Awake()
    {
        //SpawnedObjects = new GameObject[SpawnedGameObjectsLimit];
        foreach (SpawnableObject SpO in ObjectsToSpawn) //Set Spawnable objects name
        {
            SpO.name = SpO.Object.name;
        }

        for (var i = 0; i < ObjectsToSpawn.Length; i++)//Set Spawnable objects id
        {
            ObjectsToSpawn[i].id = i + 1;
        }

        if (PlaceToSpawnObjects != null)  //sets spawning point to the choosen transform or sets it to attached game object
        { SpawnPoint = PlaceToSpawnObjects.transform.position; }
        else
        {
            SpawnPoint = transform.position;
        }

        SpawnPointStartPosition = SpawnPoint;
    }

    public void Spawn(string ObjectName, bool WithLifeTime) //for external calls with name
    {
        for (var i = 0; i < ObjectsToSpawn.Length; i++)
        {
            if(ObjectsToSpawn[i].name == ObjectName)
            {
                if (WithLifeTime)
                {
                    var Spawned = Instantiate(ObjectsToSpawn[i].Object, SpawnPoint, Quaternion.identity);
                    StartCoroutine(DestroyObjectAfterLifeTimeEnds(Spawned, ObjectsToSpawn[i].LifeTime));               
                }
                else
                {
                    Instantiate(ObjectsToSpawn[i].Object, SpawnPoint, Quaternion.identity);
                }
            }
        }
    }

    public void SetSpawnPoint(Transform NewSpawnPoint)
    {
        PlaceToSpawnObjects = NewSpawnPoint;
    }
    public void SpawnAllObjects()
    {
        if (RandomizeSpawningPositions)
        {
            SpawnPoint =  RandomizeVectorInGivenRange(SpawnPoint);
        }
        //for (var i = 0; i < SpawningGameObjects.Length; i++)
        //{
        //    Instantiate(SpawningGameObjects[i], SpawnPoint, Quaternion.identity);
        //}
        for (var i = 0; i < ObjectsToSpawn.Length; i++)
        {
            Instantiate(ObjectsToSpawn[i].Object, SpawnPoint, Quaternion.identity);
        }
        SpawnPoint = new Vector3(SpawnPointStartPosition.x,
            SpawnPointStartPosition.y,
            SpawnPointStartPosition.z); //return spawn point position to the start point (to avoid it from flying avay)
    }

    public void SpawnObjectWithID1()
    {
        if (RandomizeSpawningPositions)
        {
            SpawnPoint = RandomizeVectorInGivenRange(SpawnPoint);
        }
        GameObject var = Instantiate(ObjectsToSpawn[0].Object, SpawnPoint, Quaternion.identity); 
        StartCoroutine(DestroyObjectAfterLifeTimeEnds(var)); //way to destroy every instantieted object after "Life time" ends

        SpawnPoint = new Vector3(SpawnPointStartPosition.x,
          SpawnPointStartPosition.y,
          SpawnPointStartPosition.z); //return spawn point position to the start point (to avoid it from flying avay)
    }

    public void SpawnObjectWithID2()
    {
        if (RandomizeSpawningPositions)
        {
            SpawnPoint = RandomizeVectorInGivenRange(SpawnPoint);
        }
        Instantiate(ObjectsToSpawn[1].Object, SpawnPoint, Quaternion.identity);
        SpawnPoint = new Vector3(SpawnPointStartPosition.x,
          SpawnPointStartPosition.y,
          SpawnPointStartPosition.z); //return spawn point position to the start point (to avoid it from flying avay)
    }

    public Vector3 RandomizeVectorInGivenRange(Vector3 Vector)
    {
        Vector = new Vector3(Vector.x + Random.Range(-RandomizingRangeX, RandomizingRangeX),
               Vector.y + Random.Range(-RandomizingRangeY, RandomizingRangeY),
               Vector.z + Random.Range(-RandomizingRangeZ, RandomizingRangeZ));
        return Vector;
    }

    public Vector3 ReturnSpawnPointToStartPosition(Vector3 Vector)
    {
        Vector = new Vector3(SpawnPointStartPosition.x,
         SpawnPointStartPosition.y,
         SpawnPointStartPosition.z); //return spawn point position to the start point (to avoid it from flying avay)
        return Vector;
    }

    public void Update()
    {
        foreach (SpawnableObject SpO in ObjectsToSpawn) //Set Spawnable objects name
        {
            SpO.name = SpO.Object.name;
        }
        if (PlaceToSpawnObjects != null) // cheking if new place to spawn was attached in editor or run time
        {
            SpawnPoint = PlaceToSpawn = PlaceToSpawnObjects.transform.position;           
        }
        if (SpawnOverTime && SpawningCoorutineIsRunning == false && CurrentlySpawnedObjects < SpawnedGameObjectsLimit ||
            SpawnedGameObjectsHAVELimit == false && SpawningCoorutineIsRunning == false)
            StartCoroutine(SpawnObjectsOverTime());

        CurrentlySpawnedObjects = SpawnedObjects.Count;
        //need to add funtion to always push wariables of "SpawnedOjects" to the top of the array?
    }

    IEnumerator SpawnObjectsOverTime() // way to get pause betwen spawns (able to control from editor)
    {
        SpawningCoorutineIsRunning = true;
        if (SpawnOverTime)
        {
            if (RandomizeSpawningPositions)
            {
                SpawnPoint = RandomizeVectorInGivenRange(SpawnPoint);
            }
            //for (var i = 0; i < SpawningGameObjects.Length; i++)
            //{
            //    Instantiate(SpawningGameObjects[i], SpawnPoint, Quaternion.identity);
            //}  
            //for (var i = 0; i < ObjectsToSpawn.Length; i++)
            //{
            //  Instantiate(ObjectsToSpawn[i].Object, SpawnPoint, Quaternion.identity);    
            //}
            var Control = ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Length-1)];
            var Spawned = Instantiate(Control.Object, SpawnPoint, Quaternion.identity);
            var LifeTimeOfSpawnedObject = Control.LifeTime;
            SpawnedObjects.Add(Spawned);


            if (IsObjectsHaveALifeTime && SpawnedGameObjectsHAVELimit)
            {
                //SpawnedObjects[CurrentlySpawnedObjects] = Spawned;  //way to stroe all spawned objects 
                //StartCoroutine(DestroyObjectAfterLifeTimeEnds(SpawnedObjects[CurrentlySpawnedObjects], LifeTimeOfSpawnedObject));// and destroy them after "Life time" ends
                StartCoroutine(DestroyObjectAfterLifeTimeEnds(Spawned, LifeTimeOfSpawnedObject)); 
                //CurrentlySpawnedObjects++;
            }
            else if (SpawnedGameObjectsHAVELimit)
            {
               // SpawnedObjects[CurrentlySpawnedObjects] = Spawned;  //way to stroe all spawned objects              
                //CurrentlySpawnedObjects++;
            }
            else if (IsObjectsHaveALifeTime)
            {
                    StartCoroutine(DestroyObjectAfterLifeTimeEnds(Spawned, LifeTimeOfSpawnedObject));
            }


        }
        yield return new WaitForSeconds(SpawnEveryXSeconds);
        SpawningCoorutineIsRunning = false;
        ReturnSpawnPointToStartPosition(SpawnPoint);
        //SpawnPoint = new Vector3(SpawnPointStartPosition.x,
        //   SpawnPointStartPosition.y,
        //   SpawnPointStartPosition.z); //return spawn point position to the start point (to avoid it from flying avay)
    }

    IEnumerator DestroyObjectAfterLifeTimeEnds(GameObject spawnedObject) //destroy after manually seted life time ends
    {
        if (IsObjectsHaveALifeTime)
        {
            yield return new WaitForSeconds(SpawnedGameObjectsLifeTime);
            SpawnedObjects.Remove(spawnedObject);
            Destroy(spawnedObject);
            if (SpawnedGameObjectsHAVELimit)
            {
                CurrentlySpawnedObjects--;
            }
        }
        else
            yield return null;
    }

    IEnumerator DestroyObjectAfterLifeTimeEnds(GameObject spawnedObject, float LifeTime)  //destroy after spawned Object's life time ends
    {
        if (IsObjectsHaveALifeTime)
        {
            if(SetLifeTimeManualyForAllObjects)
            {
                yield return new WaitForSeconds(SpawnedGameObjectsLifeTime);
            }
            else
            {
                yield return new WaitForSeconds(LifeTime);
            }
            SpawnedObjects.Remove(spawnedObject);
            Destroy(spawnedObject);
            if (SpawnedGameObjectsHAVELimit)
            {
                CurrentlySpawnedObjects--;
            }
        }
        else
            yield return null;
    }
}
