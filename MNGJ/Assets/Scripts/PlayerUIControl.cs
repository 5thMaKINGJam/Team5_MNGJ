using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUIControl : MonoBehaviour
{
    public Player player;
    public GameObject gameOverUI;
    public GameObject ClearUI;
    public GameObject HpUI0;
    public GameObject HpUI1;
    public GameObject HpUI2;
    int Hp;
    bool achieveClearItem;
    
    // Start is called before the first frame update
    void Start()
    {
        gameOverUI.SetActive(false);
        ClearUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Hp=player.Hp;
        achieveClearItem=player.achieveClearItem;

        ShowHp();

        if(Hp==0){
            ShowGameOverUI();
        }
        if(achieveClearItem){
            ShowClearUI();
        }
    }

    //게임 오버 유아이 보여주기
    void ShowGameOverUI(){
        gameOverUI.SetActive(true);
    }

    //게임 클리어 유아이 보여주기
    void ShowClearUI(){
        ClearUI.SetActive(true);
    }
    
    //Hp에 따라서 모양 달라짐
    void ShowHp(){
        switch(Hp){
            case 3 :
                HpUI0.SetActive(true);
                HpUI1.SetActive(true);
                HpUI2.SetActive(true);
                break;
            case 2 :
                HpUI0.SetActive(true);
                HpUI1.SetActive(true);
                HpUI2.SetActive(false);
                break;
            case 1 :
                HpUI0.SetActive(true);
                HpUI1.SetActive(false);
                HpUI2.SetActive(false);
                break;
            case 0 :
                HpUI0.SetActive(false);
                HpUI1.SetActive(false);
                HpUI2.SetActive(false);
                break;
        }
    }
}
