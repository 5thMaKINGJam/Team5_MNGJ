using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrologueManager : MonoBehaviour
{
    public GameObject prologue;
// Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("First"))
        {
            StartCoroutine(FirstTime());
        }
    }

    IEnumerator FirstTime()
    {
        // 프롤로그 UI 활성화
        prologue.SetActive(true);
        Debug.Log("Press any key to continue...");

        // 아무 키나 누를 때까지 대기
        yield return new WaitUntil(() => Input.anyKeyDown);

        PlayerPrefs.SetInt("First", 1);
        prologue.SetActive(false);
    }

}
