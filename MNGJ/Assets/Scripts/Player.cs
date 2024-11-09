using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    bool isHorizontalMove;
    float h;
    float v;
    Rigidbody2D rigid;

    void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        GetMovementValue();
    }

    void FixedUpdate(){
        Move();
    }

        //player movement 입력값 받기
    void GetMovementValue(){
        h= Input.GetAxisRaw("Horizontal");
        v= Input.GetAxisRaw("Vertical");

        //십자방향으로만 움직이기 위해 값 저장
        bool hDown = Input.GetButtonDown("Horizontal");
        bool hUp = Input.GetButtonUp("Horizontal");
        bool vDown = Input.GetButtonDown("Vertical");
        bool vUp = Input.GetButtonUp("Vertical");

        //십자이동을 위한 플래그
        if(hDown||vUp)
            isHorizontalMove=true;
        else if(vDown||hUp)
            isHorizontalMove=false;
    }

    //이동이 구현되는 함수
    void Move(){
        Vector2 movement;
        if(isHorizontalMove)
            movement=new Vector2(h,0);
        else    
            movement=new Vector2(0,v);

        rigid.velocity=movement*speed;
    }

}
