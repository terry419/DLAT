using UnityEngine;

[System.Serializable]
public class BaseStats
{
    public float baseDamage;          // �⺻ ���ݷ�
    public float baseAttackSpeed;     // �⺻ ���ݼӵ�
    public float baseMoveSpeed;       // �⺻ �̵��ӵ�
    public float baseHealth;          // �⺻ ü��
    public float baseCritRate;        // �⺻ ũ��Ƽ�� Ȯ�� (��: 0.10 = 10%)
    public float baseCritDamage;      // �⺻ ũ��Ƽ�� ���� (��: 1.5 = 150%)
}

public class CharacterStats : MonoBehaviour
{
    [Header("�⺻ �ɷ�ġ")]
    public BaseStats stats;

    [Header("���� ����")]
    public float buffDamageRatio;     // ������_���ݷ�_���� ��: 0.2 (20%)
    public float buffAttackSpeedRatio;
    public float buffMoveSpeedRatio;
    public float buffHealthRatio;
    public float buffCritRateRatio;
    public float buffCritDamageRatio;

    [Header("���� �ɷ�ġ (�б� ����)")]
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

    // ���� �ɷ�ġ ���
    public void CalculateFinalStats()
    {
        // �⺻�ɷ�ġ �� (1 + ������ + ī�� + ���� ����)
        finalDamage = stats.baseDamage * (1 + buffDamageRatio);
        finalAttackSpeed = stats.baseAttackSpeed * (1 + buffAttackSpeedRatio);
        finalMoveSpeed = stats.baseMoveSpeed * (1 + buffMoveSpeedRatio);
        finalHealth = stats.baseHealth * (1 + buffHealthRatio);
        finalCritRate = stats.baseCritRate * (1 + buffCritRateRatio);
        finalCritDamage = stats.baseCritDamage * (1 + buffCritDamageRatio);

        // ���� üũ
        if (finalDamage < 0 || finalAttackSpeed < 0 || finalMoveSpeed < 0 ||
            finalHealth < 0 || finalCritRate < 0 || finalCritDamage < 0)
        {
            Debug.LogError($"[CharacterStats] ���� ���� �߻�! Damage:{finalDamage}, AtkSpd:{finalAttackSpeed}, MoveSpd:{finalMoveSpeed}, Health:{finalHealth}, CritRate:{finalCritRate}, CritDmg:{finalCritDamage}");
            // 5�� �� ���� ����
            Invoke(nameof(QuitGame), 5f);
        }
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
