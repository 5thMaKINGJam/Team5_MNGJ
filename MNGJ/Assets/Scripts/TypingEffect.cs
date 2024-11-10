using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI targetText;
    private string text = @"본디 사람과 어울리기 좋아하던 
    안산의 호랑이, 산군은 험상궂은 생김새 때문에 
    사람과 놀지 못해 슬퍼했다.

    그러자 산군의 벗인 산신은 
    진짜처럼 살아숨쉬는 사람들이 있는 
    신묘한 영력을 지닌 사군자 그림을 선물하였고, 
    산군은 이에 기뻐하며 그림 속 사람들과 어울렸다.

    그러나 오랜 세월이 지나 산신이 명을 다하자, 
    산신의 힘이 그림 속으로 흩어지며 
    사군자 그림은 영력을 잃었다.
    그림 속 사람들은 산군을 오래간 보지 못해 
    그림 속을 떠돌아다니는 원귀가 되었다.

    이에 산군은 그림의 영력을 되찾고 
    원혼을 달래기 위해 조선에서 제일 가는 도사, 
    바로 당신에게 도움을 청하는데..";

    private float typingSpeed = 0.1f;   // 타이핑 속도
    private float soundCooldown = 0.8f; // 효과음 재생 간격
    private float soundTimer = 0f;
    bool isEnd=false;
    void Start()
    {
        text = targetText.text.ToString();
        targetText.text = "";
        SoundManager.instance.PlaySFX(SoundManager.ESfx.TYPING_EFFECT);
        StartCoroutine(TypingCoroutine());
    }
    

    IEnumerator TypingCoroutine()
    {
        if(Input.anyKeyDown){
            break;
        }
        int count = 0;

        while (count < text.Length)
        {
            // 한 글자씩 출력
            targetText.text += text[count];
            count++;

            // 효과음 재생
            soundTimer += Time.deltaTime;
            if (soundTimer >= soundCooldown)
            {
                SoundManager.instance.PlaySFX(SoundManager.ESfx.TYPING_EFFECT);
                soundTimer = 0f; // 타이머 초기화
                Debug.Log(soundTimer);
            }
            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
