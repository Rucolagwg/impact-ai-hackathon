using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using System.Text;
using SimpleJSON;

public class GeminiChatBot : MonoBehaviour
{

    // 이 메서드가 txt_leftInput에서 호출됩니다.
    public string ProcessUserInput(string userInput)
    {
        Debug.Log("GeminiChatBot 수신: " + userInput);

        // 여기에 실제 Gemini API 연동 또는 복잡한 처리 로직이 들어갑니다.
        // 지금은 간단한 응답을 생성합니다.
        string response = "Gemini가 처리한 내용: \"" + userInput + "\" 잘 받았습니다!";

        return response;
    }

    // Start와 Update는 특별한 로직이 없다면 비워둡니다.
    void Start()
    {

    }

    void Update()
    {

    }


    [TextArea]
    public string userInputField;
    //public TMP_Text chatOutput;
    public txt_left lefttxt;

    private const string API_KEY = "AIzaSyBP14P4VYmFl8dJADu1TJh3qa8rRKuT0e0"; // 입력
    private const string API_URL = "https://generativelanguage.googleapis.com/v1beta/models/gemini-2.0-flash:generateContent?key=" + API_KEY;

    [ContextMenu("SendMessage")]
    public void OnSendButtonClicked() // left Input에 글을 입력하고, 전송 버튼을 누르며 이 함수와 연결되게 한다.
    {
        string userInput = userInputField; // 요게 유저가 입력한 질문 내용
        if (!string.IsNullOrEmpty(userInput))
        {
            StartCoroutine(SendToGemini(userInput));
        }
    }

    public void OnSendEnter(string text) // left Input에 글을 입력하고, 전송 버튼을 누르며 이 함수와 연결되게 한다.
    {

        if (text != null)
        {
            StartCoroutine(SendToGemini(text));
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
                print("AI: " + reply); // string으로 반환 -> left에 출력하기
                lefttxt.SetText(reply);
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