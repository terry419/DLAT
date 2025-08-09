using UnityEngine;
using System.Collections.Generic;

// DataManager�� �������� �� ���� �����ϴ� �̱������� ����
public class DataManager : MonoBehaviour
{
    // �� �̱��� �ν��Ͻ� ���� �� �ܺ� �б� ���� ������Ƽ
    public static DataManager Instance { get; private set; }

    [Header("�����Ϳ��� �Ҵ��� ī�� ������ ���")]
    public CardDataSO[] allCardData;               // ���� �ε�: Inspector�� ���� �Ҵ�
    [Header("�����Ϳ��� �Ҵ��� ���� ������ ���")]
    public ArtifactDataSO[] allArtifactData;

    // �� ��ųʸ� �ʵ�� ����: ī��/���� ���� ��Ȯ��
    private readonly Dictionary<string, CardDataSO> cardDict = new Dictionary<string, CardDataSO>();
    private readonly Dictionary<string, ArtifactDataSO> artifactDict = new Dictionary<string, ArtifactDataSO>();

    void Awake()
    {
        // �� �̱��� �ʱ�ȭ ����
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // �� ������ �ε� & ����
        LoadAll();
    }

    // ��ü ������ �ε� �� �׸� ����
    public void LoadAll()
    {
        cardDict.Clear();
        artifactDict.Clear();

        // ī�� ������ ��ȸ
        foreach (var cd in allCardData ?? System.Array.Empty<CardDataSO>())  // �� null-���� ó��
        {
            ValidateCardData(cd);                                            // �׸� ��ȿ�� �˻�
            cardDict[cd.cardID] = cd;                                        // ��ųʸ��� ĳ��
        }

        // ���� ������ ��ȸ
        foreach (var ad in allArtifactData ?? System.Array.Empty<ArtifactDataSO>())
        {
            ValidateArtifactData(ad);
            artifactDict[ad.artifactID] = ad;
        }
    }

    // ī�� ������ ��ȸ: ������ ��� �� null ��ȯ
    public CardDataSO GetCard(string id)
    {
        if (!cardDict.TryGetValue(id, out var cd))
            Debug.LogWarning($"[DataManager] �������� �ʴ� ī�� ID ��ȸ: {id}"); // �� ��ȸ ���� ���
        return cd;
    }

    // ���� ������ ��ȸ
    public ArtifactDataSO GetArtifact(string id)
    {
        if (!artifactDict.TryGetValue(id, out var ad))
            Debug.LogWarning($"[DataManager] �������� �ʴ� ���� ID ��ȸ: {id}");
        return ad;
    }

    // ���� ī�� ������ ��ȿ�� �˻�
    public void ValidateCardData(CardDataSO cd)
    {
        if (cd == null)
        {
            Debug.LogError("[DataManager] ī�� �����Ͱ� �Ҵ���� ����(null)");
            return;
        }
        if (string.IsNullOrEmpty(cd.cardID))
            Debug.LogError($"[DataManager] ī�� ID ����: {cd.name}");
        if (string.IsNullOrEmpty(cd.cardName))
            Debug.LogError($"[DataManager] ī��� ����: {cd.cardID}");
        if (cd.damageMultiplier < 0)
            Debug.LogError($"[DataManager] ���� ����� ����: {cd.cardID} ({cd.damageMultiplier})");
    }

    // ���� ���� ������ ��ȿ�� �˻�
    public void ValidateArtifactData(ArtifactDataSO ad)
    {
        if (ad == null)
        {
            Debug.LogError("[DataManager] ���� �����Ͱ� �Ҵ���� ����(null)");
            return;
        }
        if (string.IsNullOrEmpty(ad.artifactID))
            Debug.LogError($"[DataManager] ���� ID ����: {ad.name}");
        if (ad.effectValue < 0)
            Debug.LogError($"[DataManager] ���� ���� ȿ����: {ad.artifactID} ({ad.effectValue})");
    }
}
