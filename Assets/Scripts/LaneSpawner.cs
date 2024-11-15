using UnityEngine;

public class LaneSpawner : MonoBehaviour
{
    public GameObject grassPrefab1;
    public GameObject grassPrefab2;
    public GameObject roadPrefab;
    public Transform spawnPoint;
    private float laneOffset = 1f;

    private bool GrassVersion = true;
    private string lastLaneType = "";
    private int consecutiveCount = 0;

    private float lastSpawnZ;

    void Start()
    {
        lastSpawnZ = spawnPoint.position.z;

        for (int i = 0; i < 20; i++)
        {
            GenerateLane();
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            GenerateLane();
        }
    }

    void GenerateLane()
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
        Instantiate(lane, spawnPosition, Quaternion.identity);

        // Update the Z position for the next lane
        lastSpawnZ += laneOffset;

        // Update tracking variables
        UpdateConsecutiveCount(lane == roadPrefab ? "Road" : "Grass");
    }

    GameObject GetNextGrassPrefab()
    {
        GameObject grass = GrassVersion ? grassPrefab1 : grassPrefab2;
        GrassVersion = !GrassVersion; // Alternate grass version
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
