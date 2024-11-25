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
    // �����͸� �ε��ϴ� �Լ�
    void LoadPlayerData()
    {
        // ���⼭ �����͸� �ε��ϴ� �ڵ带 �ۼ��մϴ�.
        DataManager.GetInstance.LoadPlayerDataToJson<FPlayerInfomation>("playerData.json", (loadedData) =>
        {
            GameLogicmanager.GetInstance.SetPlayerData(loadedData);
        });
    }
}
