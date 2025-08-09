using UnityEngine;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    // �̺�Ʈ�� �Է��� �и��ϸ� ���߿� �� ��Ʈ�ѷ��� ����(subscribe)�� �� �ֽ��ϴ�.
    public UnityEvent OnSubmit;   // Enter �Ǵ� A ��ư
    public UnityEvent OnCancel;   // ESC �Ǵ� B ��ư
    public UnityEvent<Vector2> OnMove; // WASD/ȭ��ǥ �Է� ����

    void Update()
    {
        // 1. �̵� �Է�: WASD �Ǵ� ȭ��ǥ Ű
        float h = Input.GetAxisRaw("Horizontal"); // A/D or ��/��
        float v = Input.GetAxisRaw("Vertical");   // W/S or ��/��
        Vector2 moveVector = new Vector2(h, v).normalized;
        if (moveVector != Vector2.zero)
            OnMove?.Invoke(moveVector); // �����ڿ� �̵� ���� ����

        // 2. Ȯ��/����: Enter Ű �Ǵ� �ܼ� A ��ư
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Submit"))
            OnSubmit?.Invoke();

        // 3. ���/�ڷ�: ESC Ű �Ǵ� �ܼ� B ��ư
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Cancel"))
            OnCancel?.Invoke();
    }
}
