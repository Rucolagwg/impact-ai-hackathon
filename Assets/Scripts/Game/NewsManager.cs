using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    public List<NewsItem> newsItems;

    public List<NewsItem> gameNewsList;

    // 싱글톤 인스턴스
    public static NewsManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        LoadNewsData();
        gameNewsList = GetRandomNewsItems();

    }

    void LoadNewsData()
    {
        TextAsset jsonText = Resources.Load<TextAsset>("news"); // Resources/news.json
        if (jsonText != null)
        {
            NewsItemList itemList = JsonUtility.FromJson<NewsItemList>("{\"items\":" + jsonText.text + "}");
            newsItems = itemList.items;

            Debug.Log($"총 뉴스 개수: {newsItems.Count}");
        }
        else
        {
            Debug.LogError("news.json 파일을 찾을 수 없습니다.");
        }
    }

    public List<NewsItem> GetRandomNewsItems(int count = 12)
    {
        // 방어 코드: 리스트가 null이거나 항목이 부족하면 그대로 반환
        if (newsItems == null || newsItems.Count <= count)
        {
            Debug.LogWarning("뉴스 항목이 부족하거나 비어 있음.");
            return new List<NewsItem>(newsItems);
        }

        List<NewsItem> shuffled = new List<NewsItem>(newsItems);

        // Fisher-Yates Shuffle
        for (int i = 0; i < shuffled.Count; i++)
        {
            int randomIndex = Random.Range(i, shuffled.Count);
            NewsItem temp = shuffled[i];
            shuffled[i] = shuffled[randomIndex];
            shuffled[randomIndex] = temp;
        }


        print($"{count} 개의 무작위 뉴스 가져오기 성공 (뉴스 메니저)");

        // 앞에서 12개 자르기
        return shuffled.GetRange(0, count);
    }

    [ContextMenu("Update Dummy Summary")]
    public void UpdateDummyNewsToSummary()
    {
        // 테스트 삭제해
        NewsItem ni = gameNewsList[0];

        UIManager.Instance.UpdateSummary(ni.title, ni.choices[0], ni.reasons[0]);

    }

    [ContextMenu("Update Dummy Upper")]
    public void UpdateDummyUpper()
    {
        // 테스트 삭제해
        NewsItem ni = gameNewsList[0];

        UIManager.Instance.UpdateUpper(ni.title, ni.summary);

    }

}

[System.Serializable]
public class NewsItem
{
    public string title;
    public string link;
    public string date;
    public string summary;
    public List<string> choices;
    public List<string> results;
    public List<string> reasons;
    public List<float> returns;
}

[System.Serializable]
public class NewsItemList
{
    public List<NewsItem> items;
}
