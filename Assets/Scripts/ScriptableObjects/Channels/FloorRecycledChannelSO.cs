using UnityEngine;

[CreateAssetMenu(fileName = "FloorRecycledChannelSO", menuName = "ScriptableObjects/FloorRecycledChannelSO")]
public class FloorRecycledChannelSO : ScriptableObject
{
    public System.Action<GameObject> OnFloorRecycled;

    public void Raise(GameObject recycledFloor)
    {
        OnFloorRecycled?.Invoke(recycledFloor);
    }
}
