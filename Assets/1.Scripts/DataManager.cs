using UnityEngine;
using System.Collections.Generic;

public class DataManager : MonoBehaviour
{
    // ī��ID�� Ű�� CardDataSO ������ ĳ��
    private Dictionary<string, CardDataSO> cardDataMap = new Dictionary<string, CardDataSO>();

    void Awake()
    {
        // SO ���� �ε�
        LoadAllCardData();
        // ������ ����
        ValidateCardData();
    }

    private void LoadAllCardData()
    {
        // Resources ������ ������ ��� CardDataSO ���� �ε�
        CardDataSO[] cards = Resources.LoadAll<CardDataSO>("CardData");
        foreach (var card in cards)
        {
            cardDataMap[card.cardID] = card;
        }
    }

    public CardDataSO GetCardData(string id)
    {
        // �������� ���� ��� null ��ȯ
        cardDataMap.TryGetValue(id, out var data);
        return data;
    }

    public void ValidateCardData()
    {
        // �� �׸� ���� ���� üũ �� �α� ���
        foreach (var kvp in cardDataMap)
        {
            var c = kvp.Value;
            if (string.IsNullOrEmpty(c.cardID) || string.IsNullOrEmpty(c.cardName))
            {
                Debug.LogError($"[DataManager] ī�� ������ ��ȿ�� ����: {kvp.Key} �׸� ������");
            }
        }
    }
}
