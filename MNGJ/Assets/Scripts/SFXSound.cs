using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSound : MonoBehaviour
{

      // 버튼 클릭 시 호출될 메서드
    public void OnButtonClick()
    {
        SoundManager.instance.PlaySFX(SoundManager.ESfx.CLICK_EFFECT);
        Debug.Log("버튼 클릭 효과음 재생!");
    }
}
