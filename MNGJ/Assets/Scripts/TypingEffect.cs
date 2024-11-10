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

    private float typingSpeed = 0.1f;
    private float soundCooldown = 0.8f;
    private float soundTimer = 0f;
    private bool isTyping = true;

    void Start()
    {
        text = targetText.text.ToString();
        targetText.text = "";
        SoundManager.instance.PlaySFX(SoundManager.ESfx.TYPING_EFFECT);
        StartCoroutine(TypingCoroutine());
    }

    IEnumerator TypingCoroutine()
    {
        int count = 0;

        while (count < text.Length && isTyping)
        {
            if (Input.anyKeyDown)
            {
                isTyping = false;
                SoundManager.instance.StopSFX();  // 인수 없이 호출
                targetText.text = text;  // 전체 텍스트 출력
                yield break;
            }

            targetText.text += text[count];
            count++;

            soundTimer += Time.deltaTime;
            if (soundTimer >= soundCooldown)
            {
                SoundManager.instance.PlaySFX(SoundManager.ESfx.TYPING_EFFECT);
                soundTimer = 0f;
            }

            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
