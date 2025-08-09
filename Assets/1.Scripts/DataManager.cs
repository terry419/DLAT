using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    // 카드ID를 키로 CardDataSO 참조를 캐싱
    private Dictionary<string, CardDataSO> cardDataMap = new Dictionary<string, CardDataSO>();

    void Awake()
    {
        // SO 에셋 로드
        LoadAllCardData();
        // 데이터 검증
        ValidateCardData();
    }

    private void LoadAllCardData()
    {
        // Resources 폴더에 저장한 모든 CardDataSO 에셋 로드
        CardDataSO[] cards = Resources.LoadAll<CardDataSO>("CardData");
        foreach (var card in cards)
        {
            cardDataMap[card.cardID] = card;
        }
    }

    public CardDataSO GetCardData(string id)
    {
        // 존재하지 않을 경우 null 반환
        cardDataMap.TryGetValue(id, out var data);
        return data;
    }

    public void ValidateCardData()
    {
        // 각 항목 누락 여부 체크 후 로그 출력
        foreach (var kvp in cardDataMap)
        {
            var c = kvp.Value;
            if (string.IsNullOrEmpty(c.cardID) || string.IsNullOrEmpty(c.cardName))
            {
                Debug.LogError($"[DataManager] 카드 데이터 유효성 오류: {kvp.Key} 항목 누락됨");
            }
        }
    }
}
