using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField]
    private InGameUI inGameUI;

    //이동을 위한 변수들
    public float speed;
    bool isHorizontalMove;
    float h;
    float v;
    Rigidbody2D rigid;

    // Player Sprites
    Animator anim;

    //HP 관련 변수
    public int Hp; //최대 3칸

    //상호작용 관련 변수
    public int InvincibilityTime; //무적 시간
    bool isInvulnerable = false; //플래그
    public bool achieveClearItem =false; //클리어 아이템 얻었니?
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        rigid=GetComponent<Rigidbody2D>();
        spriteRenderer=GetComponent<SpriteRenderer>();
        anim=GetComponent<Animator>();
        inGameUI.DrawHearts(Hp);
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
        if(hDown)
            isHorizontalMove=true;
        else if(vDown)
            isHorizontalMove=false;
        else if (hUp||vUp)
            isHorizontalMove = h!=0;
            
        //애니메이션 변수
        if(anim.GetInteger("hAxisRaw")!=h){
            anim.SetBool("isChange",true);
            anim.SetInteger("hAxisRaw",(int)h);
        }
        else if(anim.GetInteger("vAxisRaw")!=v){
            anim.SetBool("isChange",true);
            anim.SetInteger("vAxisRaw",(int)v);
        }
        else{
            anim.SetBool("isChange",false);
        }
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
            SoundManager.instance.PlaySFX(SoundManager.ESfx.ITEM_EFFECT);
            inGameUI.DrawHearts(Hp);
            Debug.Log("체력 회복해용");
        }
    }

    //체력 감소
    void LoseHp(){
        Hp--;
        inGameUI.DrawHearts(Hp);
        // Start the damage delay Coroutine
       StartCoroutine(DamageSound());       
    }

     // 죽었을 때 효과음이 겹치지 않도록
   private IEnumerator DamageSound()
   {
        SoundManager.instance.PlaySFX(SoundManager.ESfx.DAMAGE_EFFECT);
        // Wait for the specified amount of time
        yield return new WaitForSeconds(1f);
        if (Hp==0){
            SoundManager.instance.StopBGM();
            SoundManager.instance.PlayBGM(SoundManager.EBgm.GAMEOVER_BGM);
            SoundManager.instance.PlaySFX(SoundManager.ESfx.GAMEOVER_EFFECT);
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
    void OnTriggerEnter2D(Collider2D other)
    {
        // 원귀나 총알에 닿으면 체력 감소
        if (other.gameObject.CompareTag("Monster") || other.gameObject.CompareTag("Bullet"))
        {
            if (!isInvulnerable)
            {
                OnDamage();
            }
        }
    }

        void OnTriggerStay2D(Collider2D other)
        {
            //Hp 포션
            if (other.gameObject.CompareTag("Potion")){
            if (Input.GetKeyDown(KeyCode.Space)){
                GetHp();
                other.gameObject.SetActive(false);
            }
        }
        
        //보물? 클리어 아이템
        else if(other.gameObject.CompareTag("ClearItem")){
            if (Input.GetKeyDown(KeyCode.Space)){
               StartCoroutine(ClearSound());
               achieveClearItem=true;
            }
        }
    }

         //보상 얻을 때 효과음 겹치지 않도록
    private IEnumerator ClearSound()
   {
        SoundManager.instance.PlaySFX(SoundManager.ESfx.TREASURE_EFFECT);
        // Wait for the specified amount of time
        yield return new WaitForSeconds(4f);
        SoundManager.instance.PlaySFX(SoundManager.ESfx.CLEAR_EFFECT);
        speed=0; //움직이지 못하도록
        }
}
