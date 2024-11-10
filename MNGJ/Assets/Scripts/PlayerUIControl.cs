using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUIControl : MonoBehaviour
{
    public Player player;
    public GameObject gameOverUI;
    public GameObject ClearUI;
    int Hp;
    bool achieveClearItem;
    string currentSceneName;

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

    //게임 클리어 유아이 보여주며 GameManager를 고침
    void ShowClearUI(){
        currentSceneName = SceneManager.GetActiveScene().name;
        
        if(currentSceneName == "Map_1")
            GameManager.Instance.isClear[1] = true;
        else if(currentSceneName == "Map_2")
            GameManager.Instance.isClear[2] = true;
        else if(currentSceneName == "Map_3")
            GameManager.Instance.isClear[3] = true;

        ClearUI.SetActive(true);
    }
}
