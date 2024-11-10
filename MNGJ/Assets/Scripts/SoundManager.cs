using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;
    [SerializeField] private AudioMixer audioMixer;

    private bool[] isMute = new bool[2];
    private float[] audioVolumes = new float[2];
       // BGM 종류들
    public enum EBgm
    {
        MAIN_BGM,
        SPRING_BGM,
        SUMMER_BGM,
        FALL_BGM,
        WINTER_BGM,
        DETECTED_BGM,
        GAMEOVER_BGM
    }

    // SFX 종류들
    public enum ESfx
    {
        CLICK_EFFECT,
        ITEM_EFFECT,
        TREASURE_EFFECT,
        CLEAR_EFFECT,
        DETECTED_EFFECT,
        DAMAGE_EFFECT,
        GAMEOVER_EFFECT,
        TYPING_EFFECT
    }

    public enum EAudioMixerType{BGM,SFX}

    //audio clip 담을 수 있는 배열
    [SerializeField] AudioClip[] bgms;
    [SerializeField] AudioClip[] sfxs;

    //플레이하는 AudioSource
    [SerializeField] AudioSource audioBgm;
    [SerializeField] AudioSource audioSfx;

    //싱글톤
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnEnable()
    {
        // 씬이 로드될 때 호출되는 이벤트 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 씬 로드 이벤트 등록 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬이 로드될 때 호출되는 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        PlayBgmByScene();
    }
    
    
    //씬에 따라서 노래 틀기
    public void PlayBgmByScene()
    {
            // 현재 재생 중인 BGM과 재생하려는 BGM을 비교하기 위한 변수
        EBgm newBgm = EBgm.MAIN_BGM;
        string currentSceneName = SceneManager.GetActiveScene().name;

        // 씬 이름에 따라 BGM 할당
        switch (currentSceneName)
        {
            case "Start":
            case "SelectStage":
                newBgm = EBgm.MAIN_BGM; // 메인 메뉴 BGM
                break;
            case "Map_1":
                newBgm = EBgm.SPRING_BGM; // 봄 씬 BGM
                break;
            case "Map_2":
                newBgm = EBgm.SUMMER_BGM; // 여름 씬 BGM
                break;
            case "Map_3":
                newBgm = EBgm.FALL_BGM; // 가을 씬 BGM
                break;
            case "Map_4":
                newBgm = EBgm.WINTER_BGM; // 겨울 씬 BGM
                break;
            default:
                newBgm = EBgm.MAIN_BGM; // 기본 BGM
                break;
        }

         if (audioBgm.clip == bgms[(int)newBgm])
        {
            Debug.Log("현재 BGM이 이미 재생 중입니다. 변경하지 않습니다.");
            return;
        }

        PlayBGM(newBgm);
    }


   // EBgm 열거형을 매개변수로 받아 해당하는 배경 음악 클립을 재생
    public void PlayBGM(EBgm bgmIdx)
    {
      	//enum int형으로 형변환 가능
        audioBgm.clip = bgms[(int)bgmIdx];
        audioBgm.Play();
    }

    // 현재 재생 중인 배경 음악 정지
    public void StopBGM()
    {
        audioBgm.Stop();
    }

    // ESfx 열거형을 매개변수로 받아 해당하는 효과음 클립을 재생
    public void PlaySFX(ESfx esfx)
    {
        audioSfx.PlayOneShot(sfxs[(int)esfx]);
    }


    //슬라이더를 통한 볼륨 조절
    public void SetAudioVolume(EAudioMixerType audioMixerType,float volume)
    {
                // 최소값을 0.0001로 설정하여 무한대 오류 방지
        if (volume <= 0)
        {
            volume = 0.0001f;
        }
        // 오디오 믹서의 값은 -80 ~ 0까지이기 때문에 0.0001 ~ 1의 Log10 * 20을 한다.
        audioMixer.SetFloat(audioMixerType.ToString(), Mathf.Log10(volume) * 20);
    }
    
    public void OnBgmVolumeChanged(float volume)
{
    SetAudioVolume(EAudioMixerType.BGM, volume);
}

    public void OnSfxVolumeChanged(float volume)
    {
        SetAudioVolume(EAudioMixerType.SFX, volume);
    }
}
