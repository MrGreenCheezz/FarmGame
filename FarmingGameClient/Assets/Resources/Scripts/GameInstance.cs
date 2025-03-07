using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;



public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;
    [SerializeField]
    public string JwtToken;
    public string URL = "http://localhost:5145/";
    public bool IsLogedIn = false;
    public bool IsInGame = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       if(instance != null)
        {
            Destroy(this);
            Debug.Log("Another game instance already created, destroying!");
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;

    }


    public IEnumerator Login(string email, string password, MainMenuScript mainMenu)
    {
        // Экранируем специальные символы в параметрах
        string encodedEmail = UnityWebRequest.EscapeURL(email);
        string encodedPassword = UnityWebRequest.EscapeURL(password);

        // Формируем URL с query параметрами
        string urlWithParams = $"{URL}auth/login?email={encodedEmail}&password={encodedPassword}";

        using (UnityWebRequest request = new UnityWebRequest(urlWithParams, "POST"))
        {
            // Настраиваем запрос как в curl примере
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("accept", "*/*");

            // Важно: явно указываем пустое тело запроса
            request.uploadHandler = new UploadHandlerRaw(new byte[0]);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {request.error}");
                Debug.LogError($"Response Code: {request.responseCode}");
                Debug.LogError($"Response Body: {request.downloadHandler.text}");
                mainMenu.SetErrorString(request.downloadHandler.text);
            }
            else
            {
                

                // Парсим токен (адаптируйте под ваш формат ответа)
                if (TryParseToken(request.downloadHandler.text, out string token))
                {
                    JwtToken = token;
                    IsLogedIn = true;
                    Debug.Log("Login successful! Token: " + JwtToken);
                    if (string.IsNullOrEmpty(JwtToken))
                    {
                        mainMenu.SetErrorString("Token is not valid");
                    }
                    else
                    {
                        StartGame();
                    }                 
                }
                else
                {
                    Debug.LogError("Invalid response format!");
                    mainMenu.SetErrorString("Token is not valid");
                }
            }
        }
    }

    private bool TryParseToken(string json, out string token)
    {
        var res = JsonUtility.FromJson<TokenBearer>(json);
        
        print(res.token);
        token = res.token;
        IsLogedIn = true;
        return true;
    }

    public IEnumerator RegisterUser(string username, string email, string password, MainMenuScript mainMenu)
    {
        string encodedUsername = UnityWebRequest.EscapeURL(username);
        string encodedEmail = UnityWebRequest.EscapeURL(email);
        string encodedPassword = UnityWebRequest.EscapeURL(password);

        string urlWithParams = $"{URL}auth/register?username={encodedUsername}&password={encodedPassword}&email={encodedEmail}";


        using (UnityWebRequest request = new UnityWebRequest(urlWithParams, "POST"))
        {

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("accept", "*/*");

            request.uploadHandler = new UploadHandlerRaw(new byte[0]);

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError($"Error: {request.error}");
                Debug.LogError($"Response Code: {request.responseCode}");
                Debug.LogError($"Response Body: {request.downloadHandler.text}");
                mainMenu.SetErrorString(request.downloadHandler.text);
            }
            else
            {
                Debug.Log("Response: " + request.downloadHandler.text);
                mainMenu.SetSuccesString(request.downloadHandler.text);
            }
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainGameScene");
        IsInGame = true;
    }

}
[Serializable]
public class TokenBearer
{
    public string token;
}