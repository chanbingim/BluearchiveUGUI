using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameAudioManager : MonoBehaviour
{
    #region Varable
    public delegate void CharacterVoiceEnd();
    [SerializeField] private CustomDictionary<AudioClip> m_AudioClipList;
    [SerializeField] private AudioSource audioSource_BGM;
    [SerializeField] private AudioSource audioSource_Character;
    public CharacterVoiceEnd CharacterVoiceEndDele;
    #endregion
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
        if(audioSource_BGM)
        {
            audioSource_BGM.volume = 0.5f;
        }

        AudioClip characterVoice = Resources.Load<AudioClip>(path);
        audioSource_Character.clip = characterVoice;
        audioSource_Character.Play();
        StartCoroutine(DisableOnAudioEnd());
        return characterVoice.length;
    }
    private IEnumerator DisableOnAudioEnd()
    {
        // ����� ����� ���� ������ ���
        while (audioSource_Character.isPlaying)
        {
            yield return null; // ���� �����ӱ��� ���
        }

        if (audioSource_BGM)
        {
            audioSource_BGM.volume = 1.0f;
        }

        CharacterVoiceEndDele.Invoke();
    }
}
