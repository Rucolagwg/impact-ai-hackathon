using System.Collections.Generic;
using UnityEngine;

public class NewsManager : MonoBehaviour
{
    public List<NewsItem> newsItems;

    public List<NewsItem> gameNewsList;

    // ?????? ????????
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

            Debug.Log($"?? ???? ????: {newsItems.Count}");
        }
        else
        {
            Debug.LogError("news.json ?????? ???? ?? ????????.");
        }
    }

    public List<NewsItem> GetRandomNewsItems(int count = 13)
    {
        // ???? ????: ???????? null?????? ?????? ???????? ?????? ????
        if (newsItems == null || newsItems.Count <= count)
        {
            Debug.LogWarning("???? ?????? ?????????? ???? ????.");
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


        print($"{count} ???? ?????? ???? ???????? ???? (???? ??????)");

        // ?????? 12?? ??????
        return shuffled.GetRange(0, count);
    }

    [ContextMenu("Update Dummy Summary")]
    public void UpdateDummyNewsToSummary()
    {
        // ?????? ??????
        NewsItem ni = gameNewsList[0];

        UIManager.Instance.UpdateSummary(ni.title, ni.choices[0], ni.reasons[0]);

    }

    [ContextMenu("Update Dummy Upper")]
    public void UpdateDummyUpper()
    {
        // ?????? ??????
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
