using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using System.IO;
using System;
using Cysharp.Threading.Tasks;

public class LuaManager : MonoBehaviour
{
    #region Variables
    [SerializeField] bool useApi = false;
    [SerializeField] string apiPath;
    [SerializeField] string scriptsPath;
    [SerializeField] GameManager gameManager;
    [SerializeField] List<string> scripts = new List<string>();
    List<Script> scriptsList = new List<Script>();
    delegate object DefineUserData();
    Script currentScript;
    bool canRun = false;
    #endregion

    #region Unity Functions
    async void Start()
    {
        await Setup();
        Perform("Start");
        canRun = true;
    }

    void Update()
    {
        if (canRun) Perform("Update");
    }
    #endregion

    #region MoonSharp Functions
    async UniTask<int> Setup()
    {
        string scriptCode = "None";

        if (useApi)
        {  
            scriptCode = await APIManager.GetLuaCode(apiPath);
            scriptsList.Add(SetupScript(scriptCode));
        } 

        else for (int i = 0; i < scripts.Count; i++)
        {
            scriptCode = FileManager.ReadTextFile(scriptsPath + scripts[i] + ".lua");
            scriptsList.Add(SetupScript(scriptCode));
        }

        Perform("Setup");
        gameManager.SetCodeString(scriptCode);
        return 0;
    }

    void RegisterFunctions()
    {
        currentScript.Globals["Require"] = (Func<string, string, int>) Require;
        currentScript.Globals["Print"] = (Func<object, int>) Print;
    }

    Script SetupScript(string scriptText)
    {
        currentScript = new Script();
        RegisterFunctions();
        currentScript.DoString(scriptText);

        return currentScript;
    }

    void Perform(string name)
    {
        for (int i = 0; i < scriptsList.Count; i++)
        {
            currentScript = new Script();
            currentScript = scriptsList[i];

            object global = currentScript.Globals[name];
            if (global != null)
                currentScript.Call(global);
        }
    }
    #endregion

    #region Utils Functions
    int Require(string name, string nameSpace)
    {
        Type type = Type.GetType(nameSpace + "." + name + ", " + nameSpace);
        UserData.RegisterType(type);
        currentScript.Globals[name] = UserData.Create(type);

        return 0;
    }

    int Print(object text)
    {
        Debug.Log(text);
        return 0;
    }
    #endregion
}
