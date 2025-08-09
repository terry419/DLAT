// InputManager.cs
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    // 이벤트 인스턴스화 (널 체크 불필요)
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnSubmit = new UnityEvent();  // “확인/선택” 이벤트
    public UnityEvent OnCancel = new UnityEvent();  // “취소/뒤로” 이벤트

    [Header("입력 버퍼 (초)")]
    public float inputBuffer = 0.1f;
    [Header("반복 입력 간격 (초)")]
    public float repeatRate = 0.12f;

    private float lastMoveTime;
    private float lastSubmitTime;
    private float lastCancelTime;

    void Update()
    {
        float now = Time.time;

        // 이동 입력 (가변 입력을 즉시 반영하되 버퍼링 적용)
        Vector2 move = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
        if (move != Vector2.zero && now - lastMoveTime >= inputBuffer)
        {
            OnMove.Invoke(move.normalized);
            lastMoveTime = now;
        }

        // 확인/공격 입력: Enter 키 OR 매핑된 “Submit” 버튼
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))  // 변경: 키보드 A 대신 GetButtonDown("Submit") 사용
            && now - lastSubmitTime >= repeatRate)
        {
            OnSubmit.Invoke();
            lastSubmitTime = now;
        }

        // 취소/뒤로 입력: ESC 키 OR 매핑된 “Cancel” 버튼
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))  // 변경: 키보드 B 대신 GetButtonDown("Cancel") 사용
            && now - lastCancelTime >= repeatRate)
        {
            OnCancel.Invoke();
            lastCancelTime = now;
        }
    }
}
