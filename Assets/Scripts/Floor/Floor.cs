using UnityEngine;
using System;
using System.Collections.Generic;

public class Floor : MonoBehaviour
{
    [Header("트랙 설정")]
    public List<GameObject> floors = new List<GameObject>();

    [Header("속도 및 가속 설정")]
    public float moveSpeed = 5f;        // 현재/시작 속도
    public float acceleration = 0.1f;    // 초당 증가할 속도 값
    public float maxSpeed = 20f;         // 도달할 수 있는 최대 속도

    private float _accumulatedMovement = 0f; // 누적 이동 거리
    private int currentFloorIndex = 0; // 다음에 이동시킬 바닥 인덱스
    private float floorLength; // 바닥의 Z축 길이

    public event Action<GameObject, int> OnFloorRecycled;
    
    void Awake()
    {
        if (floors.Count == 0)
        {
            foreach (Transform track in transform)
                floors.Add(track.gameObject);
        }
        floorLength = GetTotalZLength(floors[0]);
    }

    void Update()
    {
        if (moveSpeed < maxSpeed)
        {
            moveSpeed += acceleration * Time.deltaTime;
            if (moveSpeed > maxSpeed)
                moveSpeed = maxSpeed;
        }

        float moveDelta = moveSpeed * Time.deltaTime;
        _accumulatedMovement += moveDelta;

        foreach(GameObject floor in floors)
        {
            floor.transform.position -= new Vector3(0, 0, moveDelta);
        }

        if (_accumulatedMovement >= floorLength)
        {
            GameObject recycledFloor = floors[currentFloorIndex];
            recycledFloor.transform.position += new Vector3(0, 0, floorLength * floors.Count);
            OnFloorRecycled?.Invoke(recycledFloor, currentFloorIndex);
            
            _accumulatedMovement -= floorLength;
            currentFloorIndex = (currentFloorIndex + 1) % floors.Count;
        }
    }

    float GetTotalZLength(GameObject obj)
    {
        Renderer[] renderers = obj.GetComponentsInChildren<Renderer>();
        if (renderers.Length == 0) return 0f;

        // 첫 번째 렌더러의 영역으로 초기화
        Bounds bounds = renderers[0].bounds;
        for (int i = 1; i < renderers.Length; i++)
        {
            bounds.Encapsulate(renderers[i].bounds);
        }

        return bounds.size.z; // 합쳐진 최종 박스의 Z축 길이
    }
}
