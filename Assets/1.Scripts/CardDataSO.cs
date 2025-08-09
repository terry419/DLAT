using UnityEngine;

[CreateAssetMenu(fileName = "CardData_", menuName = "GameData/CardData")]
public class CardDataSO : ScriptableObject
{
    [Header("식별 정보")]
    public string cardID;      // ex: "warrior_basic_001"
    public string cardName;    // UI에 표시될 카드명

    [Header("카드 속성")]
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

    [Header("수치")]
    [Header("수치")]
    public float damageMultiplier;            // 대미지 비율 보정
    public float attackSpeedMultiplier;       // 공격속도 비율 보정
    public float moveSpeedMultiplier;         // 이동속도 비율 보정
    public float healthMultiplier;            // 체력 비율 보정
    public float critRateMultiplier;          // 크리티컬 확률 보정 (optional)
    public float critDamageMultiplier;        // 크리티컬 배율 비율 보정
    public string effectDescription;

        [Header("발동 조건")]
    public TriggerType triggerType;  // IV. 카드 시스템 정의 참조

    /*
    Interval    = 0,
    OnHit       = 1,
    OnCrit      = 2,
    OnSkillUse  = 3,
    OnLowHealth = 4
    */


}
