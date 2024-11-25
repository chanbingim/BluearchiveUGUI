using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UIElements;

public class DataManager : MonoBehaviour
{
    private static DataManager instance = null;
    [SerializeField] private CustomDictionary<ScriptableObject> CharacterDataList;
    static GameLogicmanager GameInstance;

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
    public static DataManager GetInstance
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

    [ContextMenu("To Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
    public void SavePlayerDataToJson<T>(string FileName, T SaveData)
    {
        // ToJson을 사용하면 JSON형태로 포멧팅된 문자열이 생성된다  
        string jsonData = JsonUtility.ToJson(SaveData, true);
        // 데이터를 저장할 경로 지정
        string path = Path.Combine(Application.dataPath, FileName);
        // 파일 생성 및 저장
        File.WriteAllText(path, jsonData);
    }

    [ContextMenu("From Json Data")] // 컴포넌트 메뉴에 아래 함수를 호출하는 To Json Data 라는 명령어가 생성됨
    public bool LoadPlayerDataToJson<T>(string FileName, System.Action<T> onLoadCallback)
    {
        // 데이터를 불러올 경로 지정
        string path = Path.Combine(Application.dataPath, FileName);

        if(File.Exists(path))
        {
            // 파일의 텍스트를 string으로 저장
            string jsonData = File.ReadAllText(path);
            // 파일 생성 및 저장
            T LoadPlayerData = JsonUtility.FromJson<T>(jsonData);
            onLoadCallback.Invoke(LoadPlayerData);
            return true;
        }

        return false;
    }

    public ScriptableObject GetCharacterData(string Name)
    {
        return CharacterDataList.Find(Name);
    }

}
