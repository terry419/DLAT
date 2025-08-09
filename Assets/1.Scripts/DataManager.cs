using UnityEngine;
using System.Collections.Generic;

// DataManager는 전역에서 한 번만 존재하는 싱글톤으로 관리
public class DataManager : MonoBehaviour
{
    // ① 싱글톤 인스턴스 선언 및 외부 읽기 전용 프로퍼티
    public static DataManager Instance { get; private set; }

    [Header("에디터에서 할당할 카드 데이터 목록")]
    public CardDataSO[] allCardData;               // 동기 로드: Inspector에 에셋 할당
    [Header("에디터에서 할당할 유물 데이터 목록")]
    public ArtifactDataSO[] allArtifactData;

    // ② 딕셔너리 필드명 개선: 카드/유물 구분 명확히
    private readonly Dictionary<string, CardDataSO> cardDict = new Dictionary<string, CardDataSO>();
    private readonly Dictionary<string, ArtifactDataSO> artifactDict = new Dictionary<string, ArtifactDataSO>();

    void Awake()
    {
        // ③ 싱글톤 초기화 로직
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // ④ 데이터 로드 & 검증
        LoadAll();
    }

    // 전체 데이터 로드 및 항목별 검증
    public void LoadAll()
    {
        cardDict.Clear();
        artifactDict.Clear();

        // 카드 데이터 순회
        foreach (var cd in allCardData ?? System.Array.Empty<CardDataSO>())  // ⑤ null-안전 처리
        {
            ValidateCardData(cd);                                            // 항목별 유효성 검사
            cardDict[cd.cardID] = cd;                                        // 딕셔너리에 캐싱
        }

        // 유물 데이터 순회
        foreach (var ad in allArtifactData ?? System.Array.Empty<ArtifactDataSO>())
        {
            ValidateArtifactData(ad);
            artifactDict[ad.artifactID] = ad;
        }
    }

    // 카드 데이터 조회: 없으면 경고 후 null 반환
    public CardDataSO GetCard(string id)
    {
        if (!cardDict.TryGetValue(id, out var cd))
            Debug.LogWarning($"[DataManager] 존재하지 않는 카드 ID 조회: {id}"); // ⑥ 조회 실패 경고
        return cd;
    }

    // 유물 데이터 조회
    public ArtifactDataSO GetArtifact(string id)
    {
        if (!artifactDict.TryGetValue(id, out var ad))
            Debug.LogWarning($"[DataManager] 존재하지 않는 유물 ID 조회: {id}");
        return ad;
    }

    // 개별 카드 데이터 유효성 검사
    public void ValidateCardData(CardDataSO cd)
    {
        if (cd == null)
        {
            Debug.LogError("[DataManager] 카드 데이터가 할당되지 않음(null)");
            return;
        }
        if (string.IsNullOrEmpty(cd.cardID))
            Debug.LogError($"[DataManager] 카드 ID 누락: {cd.name}");
        if (string.IsNullOrEmpty(cd.cardName))
            Debug.LogError($"[DataManager] 카드명 누락: {cd.cardID}");
        if (cd.damageMultiplier < 0)
            Debug.LogError($"[DataManager] 음수 대미지 비율: {cd.cardID} ({cd.damageMultiplier})");
    }

    // 개별 유물 데이터 유효성 검사
    public void ValidateArtifactData(ArtifactDataSO ad)
    {
        if (ad == null)
        {
            Debug.LogError("[DataManager] 유물 데이터가 할당되지 않음(null)");
            return;
        }
        if (string.IsNullOrEmpty(ad.artifactID))
            Debug.LogError($"[DataManager] 유물 ID 누락: {ad.name}");
        if (ad.effectValue < 0)
            Debug.LogError($"[DataManager] 음수 유물 효과값: {ad.artifactID} ({ad.effectValue})");
    }
}
