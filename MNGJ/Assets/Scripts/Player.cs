using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    //이동을 위한 변수들
    public float speed;
    bool isHorizontalMove;
    float h;
    float v;
    Rigidbody2D rigid;

    //HP 관련 변수
    public int Hp; //최대 3칸

    //상호작용 관련 변수
    Collider2D collider;
    public int InvincibilityTime; //무적 시간
    bool isInvulnerable = false; //플래그
    public bool achieveClearItem =false; //클리어 아이템 얻었니?
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();        
        collider=GetComponent<Collider2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        GetMovementValue();
    }

    void FixedUpdate()
    {
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

    //체력 회복
    void GetHp(){
        if(Hp<3){
            Hp++;
            Debug.Log("체력 회복해용");
        }
    }

    //체력 감소
    void LoseHp(){
        Hp--;
        if(Hp==0){
            Debug.Log("죽었습니다");
            speed=0; //움직이지 못하도록
        }
            
    }
    
    //무적시간
     public void OnDamage()
   {
       // Set the invulnerable flag to true
       isInvulnerable = true;
       spriteRenderer.color=Color.yellow; //무적시간일 떄 노란색
       LoseHp();
       // Start the damage delay Coroutine
       StartCoroutine(DamageDelay());
   }

     //딜레이 후 무적시간 끝내는 코루틴
   private IEnumerator DamageDelay()
   {
       // Wait for the specified amount of time
       yield return new WaitForSeconds(InvincibilityTime);

       // Set the invulnerable flag to false
       isInvulnerable = false;
       spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
       Debug.Log("무적시간 끝");
   }

        //상호작용 감지 함수
    void OnTriggerStay2D(Collider2D other)
    {
        // 원귀와 충돌 상태에서 계속 체력 감소
        if (other.gameObject.CompareTag("Monster"))
        {
            if (!isInvulnerable)
            {
                OnDamage();
            }
        }

        //Hp 포션
        if(other.gameObject.CompareTag("Potion")){
            if (Input.GetKeyDown(KeyCode.Space)){
                GetHp();
                other.gameObject.SetActive(false);
            }
        }
        
        //보물? 클리어 아이템
        else if(other.gameObject.CompareTag("ClearItem")){
            if (Input.GetKeyDown(KeyCode.Space)){
                Debug.Log("클리어~~!");
                achieveClearItem=true;
                speed=0;
                other.gameObject.SetActive(false);
            }
        }
    }

}
