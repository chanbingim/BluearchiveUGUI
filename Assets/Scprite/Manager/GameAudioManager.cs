using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAudioManager : MonoBehaviour
{
    public delegate void CharacterVoiceEnd();

    [SerializeField] private CustomDictionary<AudioClip> m_AudioClipList;
    [SerializeField] private AudioSource audioSource_BGM;
    [SerializeField] private AudioSource audioSource_Character;
    public CharacterVoiceEnd CharacterVoiceEndDele;


    static GameAudioManager instance;

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public static GameAudioManager GetInstance
    {
        get
        {
            if (null == instance)
            {
                return null;
            }
            return instance;
        }
    }
    private void Start()
    {
        PlayBGM(SceneManager.GetActiveScene().name);
    }

    public void PlayBGM(string mapname)
    {
        var MainBGM = m_AudioClipList.Find(mapname);

        if(MainBGM)
        {
            if(audioSource_BGM.isPlaying)
                audioSource_BGM.Stop();

            audioSource_BGM.clip = MainBGM;
            audioSource_BGM.Play();
        }
    }
    public float playCharacterVoice(string path)
    {
        Debug.Log(path);
        AudioClip characterVoice = Resources.Load<AudioClip>(path);
        audioSource_Character.clip = characterVoice;
        audioSource_Character.Play();
        StartCoroutine(DisableOnAudioEnd());
        return characterVoice.length;
    }
    private IEnumerator DisableOnAudioEnd()
    {
        // 오디오 재생이 끝날 때까지 대기
        while (audioSource_Character.isPlaying)
        {
            yield return null; // 다음 프레임까지 대기
        }

        CharacterVoiceEndDele.Invoke();
    }
}
