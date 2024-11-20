using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    public int lineWidth;
    public GameObject grassPrefab1;
    public GameObject grassPrefab2;
    public GameObject roadPrefab;
    public Transform spawnPoint;
    private float laneOffset = 1f;

    private bool grassVersion = true;
    private string lastLaneType = "";
    private int consecutiveCount = 0;

    private float lastSpawnZ;

    void Start()
    {
        lastSpawnZ = spawnPoint.position.z;

        for (int i = 0; i < 50; i++)
        {
            GenerateLane();
        }
    }

    public void GenerateLane()
    {
        GameObject lane;

        // Determine lane type based on consecutive count
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
            }
        }

        // Instantiate the lane
        Vector3 spawnPosition = new Vector3(spawnPoint.position.x, spawnPoint.position.y, lastSpawnZ + laneOffset);
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
