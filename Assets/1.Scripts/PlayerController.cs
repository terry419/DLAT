// PlayerController.cs
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 moveInput;
    private CharacterStats stats;
    private CardManager cardManager;

    [Header("�Ѿ� �߻�")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("�̵� �ӵ� ����")]
    public float baseMoveSpeed = 5f;

    private Coroutine autoAttackRoutine;
    private Coroutine cardTriggerRoutine;

    void Awake()
    {
        // ������Ʈ�� �Ŵ��� ���� (�̱��� ���)
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        cardManager = CardManager.Instance;

        InputManager.Instance.OnMove.AddListener(OnMove);
    }

    void Start()
    {
        // �ڵ� ���� �ڷ�ƾ ���� (ù ȣ�⵵ interval��ŭ ����)
        StartAutoAttackLoop(stats.finalAttackSpeed);

        // ī�� �ߵ� �ڷ�ƾ ���� (10�ʸ���)
        StartCardTriggerLoop(10f);
    }

    void FixedUpdate()
    {
        float speed = stats.finalMoveSpeed * baseMoveSpeed;
        rb.velocity = moveInput * speed;
    }

    private void OnMove(Vector2 input)
    {
        moveInput = input;
    }

    // �ڵ� ���� �ڷ�ƾ: interval��ŭ ���� �� �ݺ� �߻�
    private void StartAutoAttackLoop(float attackSpeed)
    {
        if (autoAttackRoutine != null)
            StopCoroutine(autoAttackRoutine);
        autoAttackRoutine = StartCoroutine(AutoAttackLoop(attackSpeed));
    }

    private IEnumerator AutoAttackLoop(float attackSpeed)
    {
        float interval = attackSpeed > 0f ? 1f / attackSpeed : 1f;
        yield return new WaitForSeconds(interval);  // ù ȣ�� ����
        while (true)
        {
            if (bulletPrefab != null && firePoint != null)
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(interval);
        }
    }

    // ī�� �ߵ� �ڷ�ƾ: ù ȣ�� 10�� ���� �� 10�ʸ��� ȿ�� ����
    private void StartCardTriggerLoop(float interval)
    {
        if (cardTriggerRoutine != null)
            StopCoroutine(cardTriggerRoutine);
        cardTriggerRoutine = StartCoroutine(CardTriggerLoop(interval));
    }

    private IEnumerator CardTriggerLoop(float interval)
    {
        yield return new WaitForSeconds(interval);
        while (true)
        {
            List<CardDataSO> equipped = cardManager.GetEquippedCards();
            if (equipped != null && equipped.Count > 0)
            {
                int idx = Random.Range(0, equipped.Count);
                equipped[idx].TriggerEffect();
            }
            yield return new WaitForSeconds(interval);
        }
    }
}
