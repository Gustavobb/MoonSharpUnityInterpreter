using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Cysharp.Threading.Tasks;

public class APIManager
{
    public static async UniTask<string> GetLuaCode(string url)
    {
        return await Get<string>(url);
    }

    public static async UniTask Post(string url, object data)
    {
        var request = new UnityWebRequest(url, "POST");
        request.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(JsonUtility.ToJson(data)));

        request.SetRequestHeader("Content-Type", "application/json");
        await request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
    }

    public static async UniTask<T> Get<T>(string url)
    {
        UnityWebRequest uwr = UnityWebRequest.Get(url);
        var response = await uwr.SendWebRequest();

        if (uwr.result != UnityWebRequest.Result.Success)
            Debug.Log(uwr.error);

        if (typeof(T) == typeof(string))
            return (T)(object) response.downloadHandler.text;

        return JsonUtility.FromJson<T>(response.downloadHandler.text);
    }
}