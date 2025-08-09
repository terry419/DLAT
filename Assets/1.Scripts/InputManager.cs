// InputManager.cs
using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    // �̺�Ʈ �ν��Ͻ�ȭ (�� üũ ���ʿ�)
    public UnityEvent<Vector2> OnMove = new UnityEvent<Vector2>();
    public UnityEvent OnSubmit = new UnityEvent();  // ��Ȯ��/���á� �̺�Ʈ
    public UnityEvent OnCancel = new UnityEvent();  // �����/�ڷΡ� �̺�Ʈ

    [Header("�Է� ���� (��)")]
    public float inputBuffer = 0.1f;
    [Header("�ݺ� �Է� ���� (��)")]
    public float repeatRate = 0.12f;

    private float lastMoveTime;
    private float lastSubmitTime;
    private float lastCancelTime;

    void Update()
    {
        float now = Time.time;

        // �̵� �Է� (���� �Է��� ��� �ݿ��ϵ� ���۸� ����)
        Vector2 move = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );
        if (move != Vector2.zero && now - lastMoveTime >= inputBuffer)
        {
            OnMove.Invoke(move.normalized);
            lastMoveTime = now;
        }

        // Ȯ��/���� �Է�: Enter Ű OR ���ε� ��Submit�� ��ư
        if ((Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))  // ����: Ű���� A ��� GetButtonDown("Submit") ���
            && now - lastSubmitTime >= repeatRate)
        {
            OnSubmit.Invoke();
            lastSubmitTime = now;
        }

        // ���/�ڷ� �Է�: ESC Ű OR ���ε� ��Cancel�� ��ư
        if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))  // ����: Ű���� B ��� GetButtonDown("Cancel") ���
            && now - lastCancelTime >= repeatRate)
        {
            OnCancel.Invoke();
            lastCancelTime = now;
        }
    }
}
