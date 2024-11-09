using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credit : MonoBehaviour
{
    public RectTransform creditText; // 크레딧 텍스트의 RectTransform
    public float scrollSpeed = 50f;  // 스크롤 속도
    public float resetPositionY = -500f; // 시작 위치
    public float endPositionY = 1000f;   // 끝 위치

    void Start()
    {
        // 크레딧 텍스트의 초기 위치 설정
        creditText.anchoredPosition = new Vector2(creditText.anchoredPosition.x, resetPositionY);
    }

    void Update()
    {
        // 텍스트 위치를 위로 이동
        creditText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;

        // 끝 위치에 도달하면 다시 초기 위치로 설정
        if (creditText.anchoredPosition.y >= endPositionY)
        {
            creditText.anchoredPosition = new Vector2(creditText.anchoredPosition.x, resetPositionY);
        }
    }
}
