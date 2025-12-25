using UnityEngine;

public class Coin : Item
{
    protected override void CollisionPlayer(GameObject obj)
    {
        // 코인 획득 처리
        GameManager.Instance.AddScore(10);
        PoolManager.Instance.Release(itemTag, gameObject);
    }
}