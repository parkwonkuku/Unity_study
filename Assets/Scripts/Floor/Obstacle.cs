using UnityEngine;

public class Obstacle : Item
{
    protected override void CollisionPlayer(GameObject obj)
    {
        GameManager.Instance.GameOver();
    }
}
