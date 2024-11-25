using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;

[System.Serializable]
public struct FPlayerInfomation
{
    public string PlayerName;
    public int PlayerLevel;
    public int PlayerCurrentExperience;
    public int MaxExperience;
}

public class GameLogicmanager : MonoBehaviour
{
    private static GameLogicmanager instance = null;

    [SerializeField]
    FPlayerInfomation m_PlayerInfo;

    private void Awake()
    {
       if(null == instance)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
       else
        {
            Destroy(this.gameObject);
        }
    }
    public static GameLogicmanager GetInstance
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
        LoadPlayerData();
    }
    public FPlayerInfomation GetPlayerData()
    {
        return m_PlayerInfo;
    }
    public void SetPlayerData(FPlayerInfomation _Playerinfo)
    {
        m_PlayerInfo = _Playerinfo;
    }
    // 데이터를 로드하는 함수
    void LoadPlayerData()
    {
        // 여기서 데이터를 로드하는 코드를 작성합니다.
        DataManager.GetInstance.LoadPlayerDataToJson<FPlayerInfomation>("playerData.json", (loadedData) =>
        {
            GameLogicmanager.GetInstance.SetPlayerData(loadedData);
        });
    }
}
