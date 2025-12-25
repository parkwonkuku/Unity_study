using UnityEngine;
using System.Collections;

public class ObstacleSpawner : ItemSpawner
{
    private WaitForSeconds _spawnDelay = new WaitForSeconds(0.1f);

    protected override void Update()
    {
        base.Update();
        if (timer >= currentInterval)
        {
            _spawnIndex = Spawn();
            timer = 0f;
            StartCoroutine(resetLineOccupancyNextFrame());
        }
    }

    IEnumerator resetLineOccupancyNextFrame()
    {
        yield return _spawnDelay;
        ResetLineOccupancy(_spawnIndex);
        setInterval();
    }
}
