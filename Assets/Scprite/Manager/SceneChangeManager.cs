using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static LodingScene;

public class SceneChangeManager : MonoBehaviour
{
    private static SceneChangeManager instance = null;
    public string NextSceneName;

    public static CustomDictionary<Action> sceneInitActions = new CustomDictionary<Action>();

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
    public static SceneChangeManager GetInstance
    {
        get
        {
            if (null == instance)
            {
                GameObject obj = new GameObject("SceneChangeManager");
                instance = obj.AddComponent<SceneChangeManager>();
                DontDestroyOnLoad(obj);
            }
            return instance;
        }
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1)
    {
        GameAudioManager.GetInstance.PlayBGM(arg0.name);
    }

    private void Update()
    {
        if(Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            string CurrentScene = SceneManager.GetActiveScene().name;
            if (CurrentScene == "StartScenes")
                ChangeNextScene("MainLobby");
        }
    }

    public void ChangeNextScene(string _NextScenename)
    {
        SceneManager.LoadScene("Loding");
        NextSceneName = _NextScenename;
    }
}
