using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSlime : MonoBehaviour
{
    public GameObject slimePrefab; // Assign the slime prefab in the Inspector
    public float respawnTime = 10f; // Time in seconds to respawn slimes
    public int maxSlimes = 5; // Maximum number of slimes to spawn

    private List<GameObject> slimes = new List<GameObject>(); // List to keep track of spawned slimes

    public GameObject target;

    void Start()
    {
        SpawnSlimes();
    }

    void Update()
    {
        print(slimes.Count);
        // Check if any slimes have been killed
        for (int i = 0; i < slimes.Count; i++)
        {
            if (slimes[i] == null)
            {
                // Start a coroutine to respawn the slime after respawnTime seconds
                StartCoroutine(RespawnSlime(i, respawnTime));
                slimes.RemoveAt(i); // Remove the null entry from the list
                i--; // Decrement i to account for the removed entry
            }
        }
    }

    void SpawnSlimes()
    {
        for (int i = 0; i < maxSlimes; i++)
        {
            Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)); // Spawn slimes within a 10x10 area around the spawner
            GameObject slime = Instantiate(slimePrefab, spawnPosition, Quaternion.identity);
            slimes.Add(slime);

            Detection detectionScript = slime.GetComponentInChildren<Detection>();
            if (detectionScript != null)
            {
                detectionScript.player = target.transform;
            }
        }
    }

    IEnumerator RespawnSlime(int index, float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 spawnPosition = transform.position + new Vector3(Random.Range(-5f, 5f), 0f, Random.Range(-5f, 5f)); // Spawn slime within a 10x10 area around the spawner
        GameObject slime = Instantiate(slimePrefab, spawnPosition, Quaternion.identity);
        slimes.Insert(index, slime); // Insert the new slime at the same index as the one that was killed
        Detection detectionScript = slime.GetComponentInChildren<Detection>();
        if (detectionScript != null)
        {
            detectionScript.player = target.transform;
        }
    }
}
