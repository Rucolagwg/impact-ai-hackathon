using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Text;
using SimpleJSON;

public class GeminiChatBot : MonoBehaviour
{
    [TextArea]
    public string userInputField;
    //public TMP_Text chatOutput;

    private const string API_KEY = "YOUR_API_KEY";
    private const string API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + API_KEY;

    [ContextMenu("SendMessage")]
    public void OnSendButtonClicked()
    {
        string userInput = userInputField;
        if (!string.IsNullOrEmpty(userInput))
        {
            StartCoroutine(SendToGemini(userInput));
        }
    }

    IEnumerator SendToGemini(string userText)
    {
        // 내부 요청은 JSON 형식이지만 사용자에겐 안 보임
        string body = "{\"contents\": [{\"role\": \"user\", \"parts\": [{\"text\": \"" + EscapeJson(userText) + "\"}]}]}";

        using (UnityWebRequest req = new UnityWebRequest(API_URL, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(body);
            req.uploadHandler = new UploadHandlerRaw(bodyRaw);
            req.downloadHandler = new DownloadHandlerBuffer();
            req.SetRequestHeader("Content-Type", "application/json");

            yield return req.SendWebRequest();

            if (req.result == UnityWebRequest.Result.Success)
            {
                string rawJson = req.downloadHandler.text;
                string reply = ExtractReplyFromJson(rawJson);
                //chatOutput.text = "AI: " + reply;
                print("AI: " + reply);
            }
            else
            {
                //chatOutput.text = "❌ 오류: " + req.error;
                print("❌ 오류: " + req.error);
            }
        }
    }

    string ExtractReplyFromJson(string json)
    {
        var parsed = JSON.Parse(json);
        try
        {
            return parsed["candidates"][0]["content"]["parts"][0]["text"];
        }
        catch
        {
            return "⚠️ Gemini 응답을 해석할 수 없습니다.";
        }
    }

    string EscapeJson(string text)
    {
        return text.Replace("\"", "\\\"").Replace("\n", "\\n");
    }
}