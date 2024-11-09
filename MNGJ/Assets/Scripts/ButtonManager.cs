using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "Start":
                Debug.Log("게임 시작!");
                // 게임 시작 로직
                break;

            case "Restart":
                Debug.Log("게임 오버");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            case "SelectStage":
                Debug.Log("스테이지 선택 화면으로");
                //선택화면으로 이동하기
                break;

            case "NextStage":
                Debug.Log("다음 스테이지로");
                //선택화면으로 이동하기
                break;

            default:
                Debug.Log("알 수 없는 버튼 클릭");
                break;
        }
    }
}