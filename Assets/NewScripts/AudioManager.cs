using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                //寻找该类实例，
                instance = Object.FindObjectOfType(typeof(AudioManager)) as AudioManager;
                if (instance == null)
                {
                    GameObject go = new GameObject("AudioManager");
                    DontDestroyOnLoad(go);
                    instance = go.AddComponent<AudioManager>();
                    AudioManager.instance.audioSource = go.AddComponent<AudioSource>();
                }
            }
            return instance;
        }
    }

    public enum ChangeSceneOrLoad
    {
        Load, ChangeScene
    }
    private AudioSource audioSource;
    public AudioSource AudioSource
    {
        get { return audioSource; }
        set { audioSource = value; }
    }
    public ChangeSceneOrLoad csol = ChangeSceneOrLoad.ChangeScene;

    public bool yinLiangBianXiao = false;
    public bool yinLiangBianDa = false;

    private float currentYinLiangBianXiaoTime;
    public float maxYinLiangBianXiaoTime = 2f;

    private float currentYinLiangBianDaTime;
    public float maxYinLiangBianDaTime = 2f;

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Debug.Log("AudioManager==Null");
            instance = this;
        }
        else if (instance != null)
        {
            Debug.Log("AudioManager!=Null");
            Destroy(gameObject);
        }
        if (GetComponent<AudioSource>() == null)
        {
            gameObject.AddComponent<AudioSource>();
        }
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        ChangePlayOnAwake();
    }
    private void Update()
    {

        if (yinLiangBianXiao)
            YinLiangBianXiao();
        if (yinLiangBianDa)
            YinLiangBianDa();
        //Debug.Log(audioSource.clip.name);
    }


    //音乐管理
    //改变音乐（不播放）
    public void ChangeBGM(string musicName)
    {
        audioSource.clip = Resources.Load<AudioClip>(musicName);
    }
    public void PlayBGM()
    {
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }
    private void ChangePlayOnAwake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            audioSource.playOnAwake = true;
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.playOnAwake = false;
            audioSource.loop = true;
        }
    }
    public bool GetMusicPlayState()
    {
        if (audioSource.isPlaying)
            return true;
        else
            return false;
    }

    public void YinLiangBianXiao()
    {
        currentYinLiangBianXiaoTime += Time.deltaTime;
        audioSource.volume = Mathf.Lerp(audioSource.volume, 0, 0.021f);
        if (currentYinLiangBianXiaoTime >= maxYinLiangBianXiaoTime)
        {
            YinLiangZero();
            currentYinLiangBianXiaoTime = 0;
            yinLiangBianXiao = false;
            return;
        }
        //Debug.Log(audioSource.volume+"音量变小");
    }

    public void YinLiangBianDa()
    {
        currentYinLiangBianDaTime += Time.deltaTime;
        audioSource.volume = Mathf.Lerp(audioSource.volume, 1, 0.021f);
        if (currentYinLiangBianDaTime >= maxYinLiangBianDaTime)
        {
            currentYinLiangBianDaTime = 0;
            SetVolume(1f);
            yinLiangBianDa = false;
            return;
        }
        //Debug.Log(audioSource.volume + "音量变大");
    }

    public void YinLiangZero()
    {
        audioSource.volume = 0;
    }

    public void SetVolume(float x)
    {
        audioSource.volume = x;
    }

    public string GetCurrentMusic()
    {
        return audioSource.clip.name;
    }
    //音乐切换协程
    public void ChangeScenePointPlayBGM(string bgmName)
    {
        StartCoroutine(ChangeScenePointPlayBGMIEnu(bgmName));
    }

    public void NormalPointPlayBGM(string rTL, string lTR, Vector2 offset)
    {
        StartCoroutine(NormalPointPlayBGMIEnu(rTL, lTR, offset));
    }

    IEnumerator ChangeScenePointPlayBGMIEnu(string bgmName)
    {
        //GameManager.Instance.YinLiangBianXiao();
        yinLiangBianXiao = true;
        yield return new WaitForSeconds(2.01f);
        ChangeBGM(bgmName);
        PlayBGM();
        YinLiangZero();
        yinLiangBianDa = true;
        //Debug.Log("切换点播放音乐");
    }

    IEnumerator NormalPointPlayBGMIEnu(string rTL, string lTR, Vector2 offset)
    {
        if (offset.x > 0)//左向右
        {

            if (audioSource.clip == null || audioSource.clip.name != lTR)
            {
                yinLiangBianXiao = true;
                yield return new WaitForSeconds(2.01f);
                ChangeBGM(lTR);
                PlayBGM();
                YinLiangZero();
                yinLiangBianDa = true;
            }
        }
        else
        {
            if (audioSource.clip == null || audioSource.clip.name != rTL)
            {
                yinLiangBianXiao = true;
                yield return new WaitForSeconds(2.01f);
                ChangeBGM(rTL);
                PlayBGM();
                YinLiangZero();
                yinLiangBianDa = true;
            }
        }
    }

}
