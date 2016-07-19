using UnityEngine;

/// <summary>
/// sound를 재생하는 manager, mainScene에서 초기화 됩니다.
/// </summary>
public class csSoundManager : MonoBehaviour {

    public AudioClip buttonSound;

    static bool isPlaying;
    static csSoundManager _instance = null;
    public static csSoundManager Instance()
    {
        return _instance;
    }

    // Use this for initialization
    void Start()
    {
        if (_instance == null)
            _instance = this;

        DontDestroyOnLoad(this);
    }

    void Awake()
    {
        if (_instance == null)
            _instance = this;
    }

    /// <summary>
    /// Bgm을 play합니다
    /// </summary>
    /// <param name="bgm"> play할 bgm입니다,</param>
    public void PlayBgm(AudioClip bgm)
    {
       GetComponent<AudioSource>().Stop();
       GetComponent<AudioSource>().PlayOneShot(bgm);
       isPlaying = true;
    }

    /// <summary>
    /// button sound를 출력합니다.
    /// </summary>
    public void PlayButtonSound()
    {
        GetComponent<AudioSource>().PlayOneShot(buttonSound);
    }

    /// <summary>
    /// effect sound를 출력합니다.
    /// </summary>
    /// <param name="clip">출력할 effect sound 입니다.</param>
    public void PlaySfx(AudioClip clip)
    {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }

    /// <summary>
    /// 현재 재생중인 bgm을 중지시킨다.
    /// </summary>
    public void PauseBgm()
    {
        GetComponent<AudioSource>().Pause();
        isPlaying = false;
    }

    /// <summary>
    /// 중지된 bgm을 다시 재생합니다.
    /// </summary>
    public void ResumeBgm()
    {
        GetComponent<AudioSource>().Play();
        isPlaying = true;
    }

    /// <summary>
    /// 현재 재생중인 audio를 중지시킵니다.
    /// </summary>
    public void Stop()
    {
        GetComponent<AudioSource>().Stop();
        isPlaying = false;
    }

    /// <summary>
    /// bgm의 재생유무를 반환합니다.
    /// </summary>
    /// <returns></returns>
    public bool IsPlaying()
    {
        return isPlaying;
    }
}
