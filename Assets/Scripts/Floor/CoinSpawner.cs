using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class CoinSpawner : ItemSpawner
{
    private WaitForSeconds _spawnDelay = new WaitForSeconds(0.2f);
    private bool _isSpawning = false; // 중복 실행 방지용 플래그

    protected override void Update()
    {
        base.Update();
        if (timer >= currentInterval && !_isSpawning)
        {
            StartCoroutine(SpawnCoinsInSequence(5));
        }
    }

    IEnumerator SpawnCoinsInSequence(int count)
    {
        _isSpawning = true;

        int spawnIndex = GetIndex();

        for (int i = 0; i < count; i++)
        {
            _spawnIndex = Spawn(spawnIndex);
            yield return _spawnDelay;
        }
        timer = 0f;
        _isSpawning = false;
        ResetLineOccupancy(_spawnIndex);
        setInterval();
    }
}
