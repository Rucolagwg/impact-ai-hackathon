using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Text;

public class GeminiChatBot2 : MonoBehaviour
{
    [TextArea]
    public string userInputField;
    //public TMP_Text chatOutput;
    private const string API_KEY = "AIzaSyCqb3HHXZ3qgtPXsRA2tx2FYKQAZZ-oeHM"; // 🔐 Gemini API 키 입력
    //private const string API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent?key=" + API_KEY;
    //private const string API_URL = "https://generativelanguage.googleapis.com/v1/models/gemini-pro:generateContent?key=" + API_KEY;
    private const string API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + API_KEY;


    [ContextMenu("SendMessage")]
    public void OnSendButtonClicked()
    {
        string userInput = userInputField;
        if (!string.IsNullOrEmpty(userInput))
        {
            print("제미나이에게 메세지 보내는 중");
            StartCoroutine(SendToGemini(userInput));
        }
    }

    IEnumerator SendToGemini(string inputText)
    {
        // JSON 요청 포맷
        string requestBody = "{\"contents\": [{\"parts\": [{\"text\": \"" + EscapeJson(inputText) + "\"}]}]}";

        using (UnityWebRequest request = new UnityWebRequest(API_URL, "POST"))
        {
            byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string responseText = request.downloadHandler.text;
                string reply = ExtractReply(responseText);
                //chatOutput.text = $"AI: {reply}";
                
                print("Gemini Orgin Response : " + responseText);
                print("Gemini Response : " + reply);
            }
            else
            {
                //chatOutput.text = $"❌ 오류 발생: {request.error}";
                print("Gemini Error : " +request.error);
                print("Gemini Error : " + request.responseCode + " - " + request.error);
                print("Gemini Response Text : " + request.downloadHandler.text);
            }
        }
    }

    // Gemini 응답에서 텍스트만 추출
    private string ExtractReply(string json)
    {
        // 매우 간단한 파싱 (Unity에서는 JsonUtility 한계 → 실제 프로젝트에선 JSON 파서 사용 권장)
        int start = json.IndexOf("\"text\":") + 8;
        int end = json.IndexOf("\"", start);
        if (start > 0 && end > start)
            return json.Substring(start, end - start);
        else
            return "답변을 이해할 수 없습니다.";
    }

    private string EscapeJson(string text)
    {
        return text.Replace("\"", "\\\"").Replace("\n", "\\n");
    }
}