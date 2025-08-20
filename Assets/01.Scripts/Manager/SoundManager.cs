using System.Collections.Generic;
using UnityEngine;
//const string으로 이름을 저장해두기  열거대신에 해보기 
//이걸 키값으로 사운드소스 를 가지는 오브젝트  딕셔너리 
//이걸 꺼내 쓰기 
//호출할때마다 로드
public class SoundManager
{
    private static SoundManager instance;
    private static Dictionary<string, AudioClip> soundDictionary = new Dictionary<string, AudioClip>();

    private AudioSource BGMObj;

    private float bgmVolume = 1f;
    private float effectVolume = 1f;
    private float masterVolume = 1f;
    // 나중에 UI단에서 조정할 볼륨 값  
    public float BGMVolume
    {
        get { return bgmVolume * masterVolume; }
        set
        {
            if (value > 1)
            {
                bgmVolume = 1;
            }
            else if (value < 0)
            {
                bgmVolume = 0;
            }
            else
            {
                bgmVolume = value;
            }
        }
    }
    public float EffectVolume
    {
        get { return effectVolume * masterVolume; }
        set
        {
            if (value > 1)
            {
                effectVolume = 1;
            }
            else if (value < 0)
            {
                effectVolume = 0;
            }
            else
            {
                effectVolume = value;
            }
        }
    }
    public float MasterVolume
    {
        get { return masterVolume; }
        set
        {
            if (value > 1)
            {
                masterVolume = 1;
            }
            else if (value < 0)
            {
                masterVolume = 0;
            }
            else
            {
                masterVolume = value;
            }
        }
    }
    public static SoundManager Instance //프로퍼티 
    {
        get
        {
            if (instance == null)
            {
                instance = new SoundManager(); //주기함수 쓸 수 없으므로 생성자로 초기화
                GameObject sourceObj = new GameObject("BGMObj");
                instance.BGMObj = sourceObj.AddComponent<AudioSource>();
                MonoBehaviour.DontDestroyOnLoad(sourceObj);
            }
            return instance;
        }
    }

    public void OnBGM(AudioClip trackClip) //처음에 bgm 틀어줄 객체가 Awake나 Start에서 호출할 함수? 
    {
        BGMObj.clip = trackClip;
        BGMObj.loop = true;
        BGMObj.Play();
    }

    public void ChangeBGM(AudioClip trackClip)
    {
        BGMObj.clip = trackClip;
        BGMObj.loop = true;
        BGMObj.Play();
    }

    public void StopBGM()
    {
        BGMObj.Stop();
    }

    public void OnSFX(AudioClip SoundClip)
    {
        if (SoundClip == null)
        {
            return;
        }
        GameObject obj = PoolManager.Pop(ResourceStringHelper.AudioPrefab);
        AudioSource sourceComponent = obj.GetComponent<AudioSource>();
        sourceComponent.PlayOneShot(SoundClip);
    }
}