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

    [Header("총알 발사")]
    public GameObject bulletPrefab;
    public Transform firePoint;

    [Header("이동 속도 조정")]
    public float baseMoveSpeed = 5f;

    private Coroutine autoAttackRoutine;
    private Coroutine cardTriggerRoutine;

    void Awake()
    {
        // 컴포넌트와 매니저 참조 (싱글톤 사용)
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        cardManager = CardManager.Instance;

        InputManager.Instance.OnMove.AddListener(OnMove);
    }

    void Start()
    {
        // 자동 공격 코루틴 시작 (첫 호출도 interval만큼 지연)
        StartAutoAttackLoop(stats.finalAttackSpeed);

        // 카드 발동 코루틴 시작 (10초마다)
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

    // 자동 공격 코루틴: interval만큼 지연 후 반복 발사
    private void StartAutoAttackLoop(float attackSpeed)
    {
        if (autoAttackRoutine != null)
            StopCoroutine(autoAttackRoutine);
        autoAttackRoutine = StartCoroutine(AutoAttackLoop(attackSpeed));
    }

    private IEnumerator AutoAttackLoop(float attackSpeed)
    {
        float interval = attackSpeed > 0f ? 1f / attackSpeed : 1f;
        yield return new WaitForSeconds(interval);  // 첫 호출 지연
        while (true)
        {
            if (bulletPrefab != null && firePoint != null)
                Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            yield return new WaitForSeconds(interval);
        }
    }

    // 카드 발동 코루틴: 첫 호출 10초 지연 후 10초마다 효과 실행
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
