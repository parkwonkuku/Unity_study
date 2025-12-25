using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    [Header("이동 설정")]
    public float laneDistance = 3.5f; // 트랙 사이의 거리
    public float sideMoveSpeed = 10f;

    private int currentTrackIndex = 1; // 현재 트랙 인덱스 (0: 왼쪽, 1: 중앙, 2: 오른쪽)
    private float targetX = 0f;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(targetX, rb.position.y, rb.position.z);
        Vector3 smoothPosition = Vector3.MoveTowards(rb.position, newPosition, sideMoveSpeed * Time.fixedDeltaTime);
        rb.MovePosition(smoothPosition);
    }

    void OnMove(InputValue value)
    {
        float input = value.Get<Vector2>().x;
        
        if (input > 0.5f)
            MoveLane(1); // 오른쪽으로 이동
        else if (input < -0.5f)
            MoveLane(-1); // 왼쪽으로 이동
    }

    void MoveLane(int direction)
    {
        // 인덱스 범위 제한 (0~2 사이)
        int nextIndex = currentTrackIndex + direction;
        if (nextIndex < 0 || nextIndex > 2) return;

        currentTrackIndex = nextIndex;
        targetX = (currentTrackIndex - 1) * laneDistance;
    }
}
