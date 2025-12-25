using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField]
    protected string itemTag = "";

    protected void OnTriggerEnter(Collider collision)
    {
        GameObject obj = collision.gameObject;
        if (obj.CompareTag("Player"))
        {
            CollisionPlayer(obj);
        }
        if (obj.CompareTag("FloorEnd"))
        {
            PoolManager.Instance.Release(itemTag, gameObject);
        }
    }

    protected abstract void CollisionPlayer(GameObject obj);
}
