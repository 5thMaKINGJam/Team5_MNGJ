using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour
{
    public Button [] Stages;

    // Start is called before the first frame update
    void Update()
    {
        UpdateStageButtons();
    }

     // 스테이지 버튼 활성화 업데이트
    void UpdateStageButtons()
    {
        // GameManager의 isClear 배열과 버튼 배열 비교
        for (int i = 0; i < Stages.Length; i++)
        {
            // isClear가 true이면 버튼 활성화, false이면 비활성화
            if (i < GameManager.Instance.isClear.Length)
            {
                Stages[i].interactable = GameManager.Instance.isClear[i];
            }
        }
    }
}
