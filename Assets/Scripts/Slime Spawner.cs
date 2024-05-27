using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeSpawner : MonoBehaviour
{
    public GameObject slimePrefab; // Assign the slime prefab in the inspector
    public float respawnTime = 5f; // Time in seconds before respawning a slime

    private List<GameObject> spawnedSlimes = new List<GameObject>();
    private int totalSlimeCount = 5; // Initial number of slimes to spawn

    Detection detection;
    public GameObject target;

    private void Awake()
    {
        detection = slimePrefab.GetComponent<Detection>();
    }
    void Start()
    {
        detection.player = target.transform;
        SpawnSlimes();
    }

    void Update()
    {
        // Check if any slimes have been killed
        for (int i = 0; i < spawnedSlimes.Count; i++)
        {
            if (spawnedSlimes[i] == null)
            {
                // Slime has been killed, start a coroutine to respawn it later
                StartCoroutine(RespawnSlime(i));
                spawnedSlimes.RemoveAt(i);
            }
        }
    }

    void SpawnSlimes()
    {
        print("spawn");
        for (int i = 0; i < totalSlimeCount; i++)
        {
            // Spawn slimes at random positions within a certain radius
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
            GameObject slime = Instantiate(slimePrefab, spawnPosition, Quaternion.identity);
            spawnedSlimes.Add(slime);
        }
    }

    IEnumerator RespawnSlime(int index)
    {
        yield return new WaitForSeconds(respawnTime);
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-10f, 10f), 0f, Random.Range(-10f, 10f));
        GameObject slime = Instantiate(slimePrefab, spawnPosition, Quaternion.identity);
        spawnedSlimes.Insert(index, slime);
    }
}