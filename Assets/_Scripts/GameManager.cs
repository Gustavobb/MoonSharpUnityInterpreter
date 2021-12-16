using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] Text debugText;
    [SerializeField] Toggle toggle;

    public void RealoadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SetCodeString(string code)
    {
        debugText.text = code;
    }

    public void ToggleCode()
    {
        debugText.gameObject.SetActive(toggle.isOn);
    }
}
