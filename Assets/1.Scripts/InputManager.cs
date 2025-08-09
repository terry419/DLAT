using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    // 이벤트로 입력을 분리하면 나중에 각 컨트롤러가 구독(subscribe)할 수 있습니다.
    public UnityEvent OnSubmit;   // Enter 또는 A 버튼
    public UnityEvent OnCancel;   // ESC 또는 B 버튼
    public UnityEvent<Vector2> OnMove; // WASD/화살표 입력 벡터

    void Update()
    {
        // 1. 이동 입력: WASD 또는 화살표 키
        float h = Input.GetAxisRaw("Horizontal"); // A/D or ←/→
        float v = Input.GetAxisRaw("Vertical");   // W/S or ↑/↓
        Vector2 moveVector = new Vector2(h, v).normalized;
        if (moveVector != Vector2.zero)
            OnMove?.Invoke(moveVector); // 구독자에 이동 방향 전달

        // 2. 확인/선택: Enter 키 또는 콘솔 A 버튼
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))
            OnSubmit?.Invoke();

        // 3. 취소/뒤로: ESC 키 또는 콘솔 B 버튼
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
            OnCancel?.Invoke();
    }
}
