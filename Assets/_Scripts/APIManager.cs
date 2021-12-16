using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class APIManager
{
    public delegate void OnSuccess(string response);

    public static IEnumerator Get(string url, OnSuccess onSuccess)
    {
        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
            Debug.Log(www.error);
        else
            onSuccess(www.downloadHandler.text);
    }
}