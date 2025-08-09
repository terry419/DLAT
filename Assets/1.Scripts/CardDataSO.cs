using UnityEngine;

[CreateAssetMenu(fileName = "CardData_", menuName = "GameData/CardData")]
public class CardDataSO : ScriptableObject
{
    [Header("�ĺ� ����")]
    public string cardID;      // ex: "warrior_basic_001"
    public string cardName;    // UI�� ǥ�õ� ī���

    [Header("ī�� �Ӽ�")]
    public CardType type;      // Physical or Magical
    /*
    Physical = 0,
    Magical = 1
    */

    public CardRarity rarity;  // Common, Rare, Epic, Legendary

    /*
    Common    = 0,
    Rare      = 1,
    Epic      = 2,
    Legendary = 3
    */

    [Header("��ġ")]
    [Header("��ġ")]
    public float damageMultiplier;            // ����� ���� ����
    public float attackSpeedMultiplier;       // ���ݼӵ� ���� ����
    public float moveSpeedMultiplier;         // �̵��ӵ� ���� ����
    public float healthMultiplier;            // ü�� ���� ����
    public float critRateMultiplier;          // ũ��Ƽ�� Ȯ�� ���� (optional)
    public float critDamageMultiplier;        // ũ��Ƽ�� ���� ���� ����
    public string effectDescription;

        [Header("�ߵ� ����")]
    public TriggerType triggerType;  // IV. ī�� �ý��� ���� ����

    /*
    Interval    = 0,
    OnHit       = 1,
    OnCrit      = 2,
    OnSkillUse  = 3,
    OnLowHealth = 4
    */


}
