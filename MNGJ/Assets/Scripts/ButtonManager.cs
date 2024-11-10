using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public void OnButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "BackToStart":
                SceneManager.LoadScene("Start");
                break;
                
            case "Restart":
                Debug.Log("게임 오버");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            case "GoSpring":
                Debug.Log("봄 스테이지로");
                SceneManager.LoadScene("Map_1");
                break;


            case "GoSummer":
                Debug.Log("여름 스테이지로");
                SceneManager.LoadScene("Map_2");
                break;

            case "GoFall":
                Debug.Log("가을 스테이지로");
                SceneManager.LoadScene("Map_3");
                break;

            case "GoWinter":
                Debug.Log("겨울 스테이지로");
                SceneManager.LoadScene("Map_4");
                break;
            case "GoEnding":
                Debug.Log("엔딩 스테이지로");
                SceneManager.LoadScene("Ending");
                break;
            default:
                Debug.Log("알 수 없는 버튼 클릭");
                break;
        }
    }
}