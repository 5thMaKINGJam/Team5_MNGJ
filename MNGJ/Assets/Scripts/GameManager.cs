using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
      // 싱글톤 인스턴스
    public static GameManager Instance { get; private set; }

    public bool isFirst=true; //프롤로그 나올까 말까
    public bool [] isClear=new bool[4]; //클리어 했나용??

    private void Awake()
    {
        // 싱글톤 인스턴스 설정
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        isClear[0]=true;
        
    }

}
