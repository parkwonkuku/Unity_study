using UnityEngine;

public class FloorManager : MonoBehaviour
{
    public Floor floor;
    public ObstacleSpawner obstacleSpawner;
    public CoinSpawner coinSpawner;

    private float startSpeed;

    void Awake()
    {
        if (floor == null)
            floor = GetComponentInChildren<Floor>();
        
        startSpeed = floor.moveSpeed;

        //if (obstacleSpawner != null && floor != null)
            //obstacleSpawner.reset(floor.floors[1]);

        //if (coinSpawner != null && floor != null)
            //coinSpawner.reset(floor.floors[1]);
    }

    void Update()
    {
        float ratio = startSpeed / floor.moveSpeed;
        
        if (obstacleSpawner != null) obstacleSpawner.SetSpeedRatio(ratio);
        if (coinSpawner != null) coinSpawner.SetSpeedRatio(ratio);
    }

    void OnEnable()
    {
        //floor.OnFloorRecycled += HandleFloorRecycled;
    }

    void OnDisable()
    {
        //floor.OnFloorRecycled -= HandleFloorRecycled;
    }

    void HandleFloorRecycled(GameObject recycledFloor)
    {
        //obstacleSpawner.reset(recycledFloor);
        //coinSpawner.reset(recycledFloor);
    }
}
