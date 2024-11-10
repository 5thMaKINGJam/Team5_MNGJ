using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartUI : MonoBehaviour
{
    public GameObject ControlUI;
    public GameObject SettingUI;
    
    public void OnButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "TurnOff":
                Debug.Log("게임 종료");
                Application.Quit();
                break;

            case "SelectStage":
                Debug.Log("스테이지 선택 화면으로");
                SceneManager.LoadScene("SelectStage");
                break;

            case "Control":
                Debug.Log("게임 방법");
                ControlUI.SetActive(true);
                break;
            
            case "Setting":
                Debug.Log("설정");
                SettingUI.SetActive(true);
                break;

            case "Back":
                Debug.Log("다시 메인으로");
                ControlUI.SetActive(false);
                SettingUI.SetActive(false);
                break;
        }
    }
}
