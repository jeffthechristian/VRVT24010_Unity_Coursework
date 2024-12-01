using UnityEngine;
using System.Collections.Generic;

public class BoneSpawner : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectsToSpawn; 
    [SerializeField] private List<Transform> spawnPoints; 

    void Start()
    {
        SpawnObjects();
    }

    void SpawnObjects()
    {
        if (objectsToSpawn.Count > spawnPoints.Count)
        {
            Debug.LogError("Not enough spawn points for all objects!");
            return;
        }

        List<Transform> availableSpawnPoints = new List<Transform>(spawnPoints);

        for (int i = 0; i < availableSpawnPoints.Count; i++)
        {
            int randomIndex = Random.Range(i, availableSpawnPoints.Count);
            Transform temp = availableSpawnPoints[i];
            availableSpawnPoints[i] = availableSpawnPoints[randomIndex];
            availableSpawnPoints[randomIndex] = temp;
        }

        for (int i = 0; i < objectsToSpawn.Count; i++)
        {
            GameObject objectToSpawn = objectsToSpawn[i];
            Transform spawnPoint = availableSpawnPoints[i];

            Instantiate(objectToSpawn, spawnPoint.position, spawnPoint.rotation);
        }
    }
}
