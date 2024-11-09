using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSight : MonoBehaviour
{
    [SerializeField] private float maxFollowTime = 4.0f;
    
    public bool followPlayer;
    private float curFollowTime = 0.0f;
    private MonsterMove monsterMove;
    bool soundFlag=true;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(soundFlag){
                MakeSound();
                soundFlag=false;
            }
            Debug.Log("시야각에 플레이어 들어옴");
            followPlayer = true;
            monsterMove.FollowPlayer();
        }
    }

    private void Start()
    {
        monsterMove = GetComponentInParent<MonsterMove>();
    }

    void Update()
    {
        if (followPlayer)
        {
            curFollowTime += Time.deltaTime;

            if (curFollowTime >= maxFollowTime)
            {
                followPlayer = false;
                monsterMove.ReturnToOrigin();
                curFollowTime = 0;
                Debug.Log("플레이어 추격 종료, 원래 위치로 돌아감");
                soundFlag=true;
                SoundManager.instance.StopBGM();
                SoundManager.instance.PlayBgmByScene();
            }
        }
    }

    void MakeSound(){
        SoundManager.instance.StopBGM();
            SoundManager.instance.PlayBGM(SoundManager.EBgm.DETECTED_BGM);
            SoundManager.instance.PlaySFX(SoundManager.ESfx.DETECTED_EFFECT);
    }
}
