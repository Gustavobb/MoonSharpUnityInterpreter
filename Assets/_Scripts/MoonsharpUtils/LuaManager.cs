using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoonSharp.Interpreter;
using System.IO;
using System;

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
    void Start()
    {
        GetCode();
    }

    void Update()
    {
        if (canRun) Perform("Update");
    }
    #endregion

    #region Core Functions
    void GetCode()
    {
        string scriptCode = "None";

        if (useApi)
        {  
            StartCoroutine(APIManager.Get(apiPath, SetupApi));
            return;
        } 

        for (int i = 0; i < scripts.Count; i++)
        {
            scriptCode = FileManager.ReadTextFile(scriptsPath + scripts[i] + ".lua");
            scriptsList.Add(SetupScript(scriptCode));
        }

        Setup(scriptCode);
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
    void Setup(string scriptCode)
    {
        Perform("Setup");
        gameManager.SetCodeString(scriptCode);
        Perform("Start");
        canRun = true;
    }

    void SetupApi(string scriptCode)
    {
        scriptsList.Add(SetupScript(scriptCode));
        Setup(scriptCode);
    }
    #endregion

    #region MoonSharp Functions
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
