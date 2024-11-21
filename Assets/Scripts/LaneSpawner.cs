using Unity.VisualScripting;
using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    private int totalLanes = 0;
    public int lineWidth;
    public GameObject grassPrefab1;
    public GameObject grassPrefab2;
    public GameObject roadPrefab;
    public Transform spawnPoint;
    private float laneOffset = 1f;

    public GameObject[] cars;

    private bool grassVersion = true;
    private string lastLaneType = "";
    private int consecutiveCount = 0;

    private float lastSpawnZ;

    void Start()
    {
        lastSpawnZ = spawnPoint.position.z;

        for (int i = 0; i < 40; i++)
        {
            GenerateLane();
        }
    }

    public void GenerateLane()
    {
        totalLanes++;
        GameObject lane;
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y, lastSpawnZ + laneOffset);
        // Determine lane type based on consecutive count

        if (totalLanes < 6)
        {
            lane = GetNextGrassPrefab();
        }
        else
        {
            if (consecutiveCount >= 4)
            {
                // Force a change in lane type
                lane = lastLaneType == "Grass" ? roadPrefab : GetNextGrassPrefab();
                ResetConsecutiveCount();
            }
            else
            {
                // Randomly choose a lane
                if (Random.value > 0.5f)
                {
                    lane = GetNextGrassPrefab();
                }
                else
                {
                    lane = roadPrefab;

                    GameObject car = cars[Random.Range(0, cars.Length)];
                    GameObject latestCar = Instantiate(car, spawnPosition, Quaternion.Euler(0, 90, 0));
                    latestCar.transform.position = new Vector3(latestCar.transform.position.x, -1.5f, latestCar.transform.position.z);
                }
            }
        }

        GameObject latestLine = Instantiate(lane, spawnPosition, Quaternion.identity);
        latestLine.AddComponent<DestroyWhenPassed>();
        latestLine.transform.localScale = new Vector3(lineWidth, 20, 1);
        // Update the Z position for the next lane
        lastSpawnZ += laneOffset;

        // Update tracking variables
        UpdateConsecutiveCount(lane == roadPrefab ? "Road" : "Grass");
    }

    GameObject GetNextGrassPrefab()
    {
        GameObject grass = grassVersion ? grassPrefab1 : grassPrefab2;
        grassVersion = !grassVersion; // Alternate grass version
        return grass;
    }

    void ResetConsecutiveCount()
    {
        consecutiveCount = 0;
    }

    void UpdateConsecutiveCount(string currentType)
    {
        if (currentType == lastLaneType)
        {
            consecutiveCount++;
        }
        else
        {
            lastLaneType = currentType;
            ResetConsecutiveCount();
        }
    }
}
