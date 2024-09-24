using UnityEngine;
using System.Collections;
using System.IO;

public class Spawner : MonoBehaviour {
    public float secondsToSpawn;
    public GameObject prefabToSpawn;

    public static int itemsSpawned = 0;

    private string outputFile = @"C:\Users\tasos\Downloads\unity_benchmark.txt";
    private void Start() {
        StartCoroutine(SpawnPrefabEverySecond());
    }

    private void Update() {
        var currentFps = (int)(1f / Time.unscaledDeltaTime);

        if (itemsSpawned % 50 == 0) {
            var barrelsToFps = $"({itemsSpawned}, {currentFps})\n";
            
            File.AppendAllText(outputFile, barrelsToFps);
        }
    }

    private IEnumerator SpawnPrefabEverySecond() {
        while (true) {
            Instantiate(prefabToSpawn, transform.position, transform.rotation);

            itemsSpawned++;
            yield return new WaitForSeconds(secondsToSpawn);
        }
    }
}