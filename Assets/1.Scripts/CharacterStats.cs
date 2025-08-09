using UnityEngine;

[System.Serializable]
public class BaseStats
{
    public float baseDamage;          // 기본 공격력
    public float baseAttackSpeed;     // 기본 공격속도
    public float baseMoveSpeed;       // 기본 이동속도
    public float baseHealth;          // 기본 체력
    public float baseCritRate;        // 기본 크리티컬 확률 (예: 0.10 = 10%)
    public float baseCritDamage;      // 기본 크리티컬 배율 (예: 1.5 = 150%)
}

public class CharacterStats : MonoBehaviour
{
    [Header("기본 능력치")]
    public BaseStats stats;

    [Header("보정 비율")]
    public float buffDamageRatio;     // 증폭제_공격력_비율 예: 0.2 (20%)
    public float buffAttackSpeedRatio;
    public float buffMoveSpeedRatio;
    public float buffHealthRatio;
    public float buffCritRateRatio;
    public float buffCritDamageRatio;

    [Header("최종 능력치 (읽기 전용)")]
    public float finalDamage;
    public float finalAttackSpeed;
    public float finalMoveSpeed;
    public float finalHealth;
    public float finalCritRate;
    public float finalCritDamage;

    void Start()
    {
        CalculateFinalStats();
    }

    // 최종 능력치 계산
    public void CalculateFinalStats()
    {
        // 기본능력치 × (1 + 증폭제 + 카드 + 유물 비율)
        finalDamage = stats.baseDamage * (1 + buffDamageRatio);
        finalAttackSpeed = stats.baseAttackSpeed * (1 + buffAttackSpeedRatio);
        finalMoveSpeed = stats.baseMoveSpeed * (1 + buffMoveSpeedRatio);
        finalHealth = stats.baseHealth * (1 + buffHealthRatio);
        finalCritRate = stats.baseCritRate * (1 + buffCritRateRatio);
        finalCritDamage = stats.baseCritDamage * (1 + buffCritDamageRatio);

        // 음수 체크
        if (finalDamage < 0 || finalAttackSpeed < 0 || finalMoveSpeed < 0 ||
            finalHealth < 0 || finalCritRate < 0 || finalCritDamage < 0)
        {
            Debug.LogError($"[CharacterStats] 음수 스탯 발생! Damage:{finalDamage}, AtkSpd:{finalAttackSpeed}, MoveSpd:{finalMoveSpeed}, Health:{finalHealth}, CritRate:{finalCritRate}, CritDmg:{finalCritDamage}");
            // 5초 후 게임 종료
            Invoke(nameof(QuitGame), 5f);
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
