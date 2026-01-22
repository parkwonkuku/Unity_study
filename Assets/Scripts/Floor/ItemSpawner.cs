using UnityEngine;

public abstract class ItemSpawner : MonoBehaviour
{
    [Header("스폰 설정")]
    public Transform[] spawnPoints;
    public string itemTag = "";
    public float minInterval = 1f;
    public float maxInterval = 3f;

    [Header("이벤트 채널 설정")]
    [SerializeField] private FloorRecycledChannelSO floorRecycledChannel;

    [SerializeField]
    protected float currentInterval;
    [SerializeField]
    protected float lastSpeedRatio = 1f;

    protected float timer = 0f;
    protected int _spawnIndex;
    protected GameObject parentObject;

    protected static bool[] isLineOccupied = new bool[3];

    protected virtual void Awake()
    {
        currentInterval = Random.Range(minInterval, maxInterval);
        if (floorRecycledChannel != null)
        {
            floorRecycledChannel.OnFloorRecycled += OnFloorRecycled;
            Debug.Log("ItemSpawner subscribed to OnFloorRecycled");
        }
    }

    protected virtual void Update()
    {
        timer += Time.deltaTime;
    }

    public virtual void Disable()
    {
        if (floorRecycledChannel != null)
        {
            floorRecycledChannel.OnFloorRecycled -= OnFloorRecycled;
        }
    }

    public void OnFloorRecycled(GameObject parent)
    {
        parentObject = parent;
    }

    public virtual int Spawn(int index = -1)
    {
        // 1. 사용할 라인 결정
        int spawnIndex = index;

        if (spawnIndex == -1)
            spawnIndex = GetIndex();

        // 2. 라인 점유 표시
        isLineOccupied[spawnIndex] = true;
        
        // 3. 실제 생성 로직
        GameObject itemObj = PoolManager.Instance.Get(itemTag);
        if (itemObj != null)
        {
            itemObj.transform.position = spawnPoints[spawnIndex].position;
            if (parentObject != null)
                itemObj.transform.SetParent(parentObject.transform);
            itemObj.SetActive(true);
        }

        return spawnIndex;
    }

    public static void ResetLineOccupancy(int index)
    {
        isLineOccupied[index] = false;
    }

    protected int GetIndex()
    {
        int index;

        do
        {
            index = Random.Range(0, spawnPoints.Length);
        } while (isLineOccupied[index]);

        return index;
    }

    public void SetSpeedRatio(float ratio) => lastSpeedRatio = ratio;

    protected void setInterval() => currentInterval = Random.Range(minInterval, maxInterval) * lastSpeedRatio;
}
